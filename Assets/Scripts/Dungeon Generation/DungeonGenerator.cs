using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;
using Core.Enemy;
using Core.Utils;

namespace Dungeon.DungeonGeneration
{
    public enum DungeonState
    {
        inactive,
        generatingMain,
        generatingBranches,
        cleanup,
        completed
    }

    /// <summary>
    /// The whole dungeon generation process inside here.
    /// </summary>
    public class DungeonGenerator : MonoBehaviour
    {
        [Header("Dungeon Tiles")]
        [SerializeField] GameObject[] startPrefabs;
        [SerializeField] GameObject[] tilePrefabs;
        [SerializeField] GameObject[] exitPrefabs;
        [SerializeField] GameObject[] blockedPrefabs;
        [SerializeField] GameObject[] doorPrefabs;
        [SerializeField] GameObject doorsParent;

        [Header("Debugging Options")]
        [SerializeField] bool useBoxColliders;
        [SerializeField] bool useLightsForDebugging;
        [SerializeField] bool restoreLightsAfterDebugging;

        [Header("Generation Limits")]
        [Range(2, 100)] [SerializeField] int mainLength = 10;
        [Range(0, 50)] [SerializeField] int brachLength = 5;
        [Range(0, 25)] [SerializeField] int numBrenches = 10;
        [Range(0, 100)] [SerializeField] int doorPercent = 25;
        [Range(0f, 1f)] [SerializeField] float constructionDelay;

        [Header("Available at Runtime")]
        public List<Tile> generatedTiles = new List<Tile>();
        public List<GameObject> collapsedWalls = new List<GameObject>();

        [HideInInspector] public static DungeonState dungeonState = DungeonState.inactive;
        private List<Connector> availableConnectors = new List<Connector>();
        private Color startLightColor = Color.white;
        private Transform tileFrom, tileTo, tileRoot;
        private Transform container;
        private GameObject goPlayer;
        private int attempts;
        private int maxAttempts = 100;

        #region Setup
        private void Start()
        {
            if (PersistentData.Instance.newGame)
            {
                LoadNewSeed();
                goPlayer = GameObject.FindWithTag("Camera");

            }
            else
            {
                LoadPreviousSeed();
                goPlayer = null;
            }

            StartCoroutine(DungeonBuild());
        }

        private void LoadPreviousSeed()
        {
            string stringSeed = PlayerPrefs.GetString("seed");
            PersistentData.Instance.seed = stringSeed;
            int intSeed = 0;
            foreach (char c in stringSeed)
            {
                intSeed += (int)c;
            }

            Random.InitState(intSeed);
        }

        private void LoadNewSeed()
        {
            int intSeed = 0;
            string stringSeed = PersistentData.Instance.seed;
            if (!stringSeed.Equals(""))
            {
                intSeed = 0;
                foreach (char c in stringSeed)
                {
                    intSeed += (int)c;
                }
#pragma warning disable
                Random.seed = intSeed;
#pragma warning restore

                PlayerPrefs.SetString("seed", intSeed.ToString());
            }
            else
            {
                intSeed = Random.Range(0, 69420);
                PlayerPrefs.SetString("seed", intSeed.ToString());
            }
        }
        #endregion

        #region Dungeon Build
        private IEnumerator DungeonBuild()
        {
            GameObject goContainer = new GameObject("Main Path");
            container = goContainer.transform;
            container.SetParent(transform);

            tileRoot = CreateStartTile();
            DebugRoomLighting(tileRoot, Color.cyan);
            tileTo = tileRoot;
            dungeonState = DungeonState.generatingMain;

            while(generatedTiles.Count < mainLength)
            {
                yield return new WaitForSeconds(constructionDelay);
                tileFrom = tileTo;

                if(generatedTiles.Count == mainLength - 1)
                {
                    tileTo = CreateExitTile();
                    DebugRoomLighting(tileTo, Color.magenta);
                }
                else
                {
                    tileTo = CreateTile();
                    DebugRoomLighting(tileTo, Color.yellow);
                }

                ConnectTiles();
                CollisionCheck();
            }

            foreach(Connector connector in container.GetComponentsInChildren<Connector>())
            {
                if (!connector.isConnected)
                {
                    if (!availableConnectors.Contains(connector))
                    {
                        availableConnectors.Add(connector);
                    }
                }
            }

            dungeonState = DungeonState.generatingBranches;
            for (int b = 0;b < numBrenches; b++)
            {
                if (availableConnectors.Count > 0)
                {
                    goContainer = new GameObject("Branch " + (b + 1));
                    container = goContainer.transform;
                    container.SetParent(transform);
                    int availableIndex = Random.Range(0, availableConnectors.Count);
                    tileRoot = availableConnectors[availableIndex].transform.parent.parent;
                    availableConnectors.RemoveAt(availableIndex);
                    tileTo = tileRoot;

                    for (int i = 0; i < brachLength - 1; i++)
                    {
                        yield return new WaitForSeconds(constructionDelay);
                        tileFrom = tileTo;
                        tileTo = CreateTile();
                        DebugRoomLighting(tileTo, Color.green);
                        ConnectTiles();
                        CollisionCheck();
                        if (attempts >= maxAttempts)
                        {
                            break;
                        }
                    }
                }
                else
                    break;
            }

            dungeonState = DungeonState.cleanup;

            LightRestoriation();
            CleanUpBoxes();
            StartCoroutine(BlockPassages());
            SpawnDoors();

            foreach(RandomDecor decor in FindObjectsOfType<RandomDecor>())
            {
                decor.Decorate();
            }

            GetComponent<NavmeshBaker>().BakeNavMesh();

            dungeonState = DungeonState.completed;

            StaticBatchingUtility.Combine(gameObject);
        }

        private void SpawnDoors()
        {
            if(doorPercent > 0)
            {
                Connector[] allConnectors = transform.GetComponentsInChildren<Connector>();
                for(int i = 0;i < allConnectors.Length; i++)
                {
                    Connector myConnector = allConnectors[i];
                    if (myConnector.isConnected)
                    {
                        int roll = Random.Range(1, 101);
                        if(roll <= doorPercent)
                        {
                            Vector3 halfExtents = new Vector3(myConnector.size.x, 1f, myConnector.size.x);
                            Vector3 pos = myConnector.transform.position;
                            Vector3 offset = Vector3.up * 0.5f;
                            Collider[] hits = Physics.OverlapBox(pos + offset, halfExtents, Quaternion.identity, LayerMask.GetMask("Door"));

                            if(hits.Length == 0)
                            {
                                int doorIndex = Random.Range(0, doorPrefabs.Length);
                                GameObject goDoor;
                                if(doorIndex <= 1)
                                {
                                    goDoor = Instantiate(doorPrefabs[doorIndex], pos, myConnector.transform.rotation, doorsParent.transform) as GameObject;
                                }
                                else
                                {
                                    goDoor = Instantiate(doorPrefabs[doorIndex], pos, myConnector.transform.rotation, myConnector.transform) as GameObject;
                                }
                                
                                goDoor.name = doorPrefabs[doorIndex].name;
                            }
                        }
                    }
                }
            }
        }

        private Vector3 matrixPos, matrixHalfExtents;

        private void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.TRS(matrixPos, Quaternion.identity, matrixHalfExtents);
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }

        private IEnumerator BlockPassages()
        {
            foreach(Connector connector in transform.GetComponentsInChildren<Connector>())
            {
                if (!connector.isConnected)
                {
                    Vector3 wallPos = connector.transform.position;
                    int wallIndex = Random.Range(0, blockedPrefabs.Length);
                    GameObject goWall = Instantiate(blockedPrefabs[wallIndex], wallPos, connector.transform.rotation, connector.transform) as GameObject;
                    goWall.name = blockedPrefabs[wallIndex].name;

                    /*BoxCollider box = goWall.GetComponent<BoxCollider>();

                    Vector3 offset = (connector.transform.right * box.center.x) + (connector.transform.up * box.center.y) + (connector.transform.forward * box.center.z);
                    Vector3 halfExtents = box.bounds.extents * connector.size.x / 2;*/

                    Vector3 center = connector.transform.position + connector.transform.forward * 4f + Vector3.up * connector.size.y/2;
                    Vector3 halfExtents = Vector3.one * 3f;

                    matrixPos = center;
                    matrixHalfExtents = halfExtents;

                    //List<Collider> hits = Physics.OverlapBox(matrixPos, matrixHalfExtents, Quaternion.identity, LayerMask.GetMask("Tile")).ToList();

                    List<Collider> hits = Physics.OverlapBox(center, halfExtents, Quaternion.identity, LayerMask.GetMask("Tile")).ToList();

                    if (hits.Count != 0)
                    {
                        DestroyImmediate(goWall);

                        GameObject goWall2 = Instantiate(blockedPrefabs.Last(), wallPos, connector.transform.rotation, connector.transform) as GameObject;
                        goWall2.name = blockedPrefabs.Last().name;
                    }

                    yield return null;
                }
            }
        }

        private void CollisionCheck()
        {
            BoxCollider box = tileTo.GetComponent<BoxCollider>();
            if(box == null)
            {
                box = tileTo.gameObject.AddComponent<BoxCollider>();
                box.isTrigger = true;
            }

            Vector3 offset = (tileTo.right * box.center.x) + (tileTo.up * box.center.y) + (tileTo.forward * box.center.z);
            Vector3 halfExtents = box.bounds.extents;

            List<Collider> hits = Physics.OverlapBox(tileTo.position + offset, halfExtents, Quaternion.identity, LayerMask.GetMask("Tile")).ToList();

            if (hits.Count > 0)
            {
                if(hits.Exists(x => x.transform != tileFrom && x.transform != tileTo))
                {
                    attempts++;
                    int toIndex = generatedTiles.FindIndex(x => x.tile == tileTo);
                    if(generatedTiles[toIndex].connector != null)
                    {
                        generatedTiles[toIndex].connector.isConnected = false;
                    }
                    generatedTiles.RemoveAt(toIndex);
                    DestroyImmediate(tileTo.gameObject);

                    if(attempts >= maxAttempts)
                    {
                        int fromIndex = generatedTiles.FindIndex(x => x.tile == tileFrom);
                        Tile myTileFrom = generatedTiles[fromIndex];
                        if(tileFrom != tileRoot)
                        {
                            if(myTileFrom.connector != null)
                            {
                                myTileFrom.connector.isConnected = false;
                            }
                            availableConnectors.RemoveAll(x => x.transform.parent.parent == tileFrom);
                            generatedTiles.RemoveAt(fromIndex);
                            DestroyImmediate(tileFrom.gameObject);

                            if (myTileFrom.origin != tileRoot)
                            {
                                tileFrom = myTileFrom.origin;
                            }
                            else if (container.name.Contains("Main"))
                            {
                                if(myTileFrom.origin != null)
                                {
                                    tileRoot = myTileFrom.origin;
                                    tileFrom = tileRoot;
                                }
                            }
                            else if (availableConnectors.Count > 0)
                            {
                                int availableIndex = Random.Range(0, availableConnectors.Count);
                                tileRoot = availableConnectors[availableIndex].transform.parent.parent;
                                availableConnectors.RemoveAt(availableIndex);
                                tileFrom = tileRoot;
                            }
                            else { return; }
                        }
                        else if (container.name.Contains("Main"))
                        {
                            if(myTileFrom.origin != null)
                            {
                                tileRoot = myTileFrom.origin;
                                tileFrom = tileRoot;
                            }
                        }
                        else if(availableConnectors.Count > 0)
                        {
                            int availableIndex = Random.Range(0, availableConnectors.Count);
                            tileRoot = availableConnectors[availableIndex].transform.parent.parent;
                            availableConnectors.RemoveAt(availableIndex);
                            tileFrom = tileRoot;
                        }
                        else { return; }
                    }

                    if (tileFrom != null)
                    {
                        if (generatedTiles.Count == mainLength - 1)
                        {
                            tileTo = CreateExitTile();
                            DebugRoomLighting(tileTo, Color.magenta);
                        }
                        else
                        {
                            tileTo = CreateTile();
                            Color retryColor = container.name.Contains("Branch") ? Color.green : Color.yellow;
                            DebugRoomLighting(tileTo, retryColor * 2f);
                        }

                        ConnectTiles();
                        CollisionCheck();
                    }
                }
                else
                {
                    attempts = 0;
                }
            }
        }

        private void LightRestoriation()
        {
            if(useLightsForDebugging && restoreLightsAfterDebugging && Application.isEditor)
            {
                Light[] lights = transform.GetComponentsInChildren<Light>();
                foreach(Light light in lights)
                {
                    light.color = startLightColor;
                }
            }
        }

        private void CleanUpBoxes()
        {
            if (!useBoxColliders)
            {
                foreach(Tile myTile in generatedTiles)
                {
                    BoxCollider box = myTile.tile.GetComponent<BoxCollider>();
                    MeshRenderer mesh = myTile.tile.GetComponent<MeshRenderer>();
                    MeshFilter meshF = myTile.tile.GetComponent<MeshFilter>();

                    if(meshF != null)
                    {
                        Destroy(meshF);
                    }

                    if(mesh != null)
                    {
                        Destroy(mesh);
                    }

                    if(box != null)
                    {
                        Destroy(box);
                    }
                }
            }
        }

        private void DebugRoomLighting(Transform tile, Color lightColor)
        {
            if (useLightsForDebugging && Application.isEditor)
            {
                Light[] lights = tile.GetComponentsInChildren<Light>();
                if(lights.Length > 0)
                {
                    if (startLightColor == Color.white)
                    {
                        startLightColor = lights[0].color;
                    }

                    foreach(Light light in lights)
                    {
                        light.color = lightColor;
                    }
                }   
            }
        }

        private void ConnectTiles()
        {
            Transform connectFrom = GetRandomConnector(tileFrom);
            if (connectFrom == null) return;

            Transform connectTo = GetRandomConnector(tileTo);
            if (connectTo == null) return;

            connectTo.SetParent(connectFrom);
            tileTo.SetParent(connectTo);
            connectTo.localPosition = Vector3.zero;
            connectTo.localRotation = Quaternion.identity;
            connectTo.Rotate(0, 180f, 0);
            tileTo.SetParent(container);
            connectTo.SetParent(tileTo.Find("Connectors"));

            generatedTiles.Last().connector = connectFrom.GetComponent<Connector>();
        }

        private Transform GetRandomConnector(Transform tile)
        {
            if (tile == null) return null;

            List<Connector> connectorList = tile.GetComponentsInChildren<Connector>().ToList().FindAll(x => x.isConnected == false);
            if(connectorList.Count > 0)
            {
                int connectorIndex = Random.Range(0, connectorList.Count);
                connectorList[connectorIndex].isConnected = true;

                if(tile == tileFrom)
                {
                    BoxCollider box = tile.GetComponent<BoxCollider>();
                    if(box == null)
                    {
                        box = tile.gameObject.AddComponent<BoxCollider>();
                        box.isTrigger = true;
                    }
                }

                return connectorList[connectorIndex].transform;
            }

            return null;
        }

        private Transform CreateTile()
        {
            int index = Random.Range(0, tilePrefabs.Length);
            GameObject goTile = Instantiate(tilePrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;

            goTile.name = tilePrefabs[index].name;

            Transform origin = generatedTiles[generatedTiles.FindIndex(x => x.tile == tileFrom)].tile;
            generatedTiles.Add(new Tile(goTile.transform, origin));

            return goTile.transform;
        }

        private Transform CreateExitTile()
        {
            int index = Random.Range(0, exitPrefabs.Length);
            GameObject goTile = Instantiate(exitPrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;

            goTile.name = "Exit Room";

            Transform origin = generatedTiles[generatedTiles.FindIndex(x => x.tile == tileFrom)].tile;
            generatedTiles.Add(new Tile(goTile.transform, origin));

            return goTile.transform;
        }

        private Transform CreateStartTile()
        {
            int index = Random.Range(0, startPrefabs.Length);
            GameObject goTile = Instantiate(startPrefabs[index], Vector3.zero, Quaternion.identity) as GameObject;

            goTile.name = "Start Room";

            /*float yRot = Random.Range(0, 4) * 90f;
            goTile.transform.Rotate(0, yRot, 0);*/

            if(goPlayer != null) 
                goPlayer.transform.LookAt(goTile.GetComponentInChildren<Connector>().transform.position);

            generatedTiles.Add(new Tile(goTile.transform, null));

            return goTile.transform;
        }
    }
    #endregion
}
