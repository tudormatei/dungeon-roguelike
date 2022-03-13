using System.Collections;
using UnityEngine;
using Dungeon.UI;
using EZCameraShake;
using TMPro;
using Core.Player;
using Core.ItemManagement;
using Dungeon.Audio;
using Core.Enemy;

namespace Core.Combat
{
    public class Fighter : MonoBehaviour
    {
        [Header("Combat Settings")]
        [SerializeField] private Transform weaponsParent;
        private PlayerAttributes playerAttributes;
        public WeaponObject currentWeapon = null;

        public JewelryObject currentJewelry = null;
        private bool jewelryEffectActive;

        private Animator currentWeaponAnimator;
        private bool canAttack = true;

        private Controls controls;
        
        [Header("Damage Display")]
        [SerializeField] private Color normalDamage;
        [SerializeField] private Color critDamage;
        [SerializeField] private GameObject damagePrefab;

        #region Start Setup
        private void Awake()
        {
            controls = new Controls();
            playerAttributes = GetComponent<PlayerAttributes>();
        }

        private void Start()
        {
            controls.Gameplay.MouseClick.performed += ctx => Attack();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
        #endregion

        #region Attacking
        private void Attack()
        {
            if (PauseMenu.isPaused) { return; }
            if (Interract.panelOpen) { return; }
            if(currentWeapon == null) { return; }
            if (!canAttack) { return; }

            // TODO: Randomize. 
            GameObject.Find("Canvas").GetComponent<AudioManager>().TriggerSoundEffect(3);

            StartCoroutine(AttackCooldown(currentWeapon.attackSpeed));
            if(currentWeaponAnimator != null) 
            { 
                int animationIndex = Random.Range(1, currentWeapon.NumberOfAttackAnimations + 1); 
                currentWeaponAnimator.SetTrigger("attack" + animationIndex); 
            }

            bool range = currentWeapon.IsRanged;
            if (range)
            {
                ShootProjectile();
            }
            else
            {
                MeleeAttack();
            }
        }

        private void SetCurrentWeaponAnimator()
        {
            if(currentWeapon == null) { currentWeaponAnimator = null; return; }

            if (currentWeapon.weaponGO != null)
            {
                currentWeaponAnimator = currentWeapon.weaponGO.transform.GetChild(0).GetComponent<Animator>();
            }
        }

        private IEnumerator AttackCooldown(float attackSpeed)
        {
            canAttack = false;

            yield return new WaitForSecondsRealtime(attackSpeed);

            canAttack = true;
        }


        private void MeleeAttack()
        {
            CameraShaker.Instance.ShakeOnce(3f, 2f, .2f, 1f);
        }

        private void ShootProjectile()
        {
            float damage;
            bool crit;
            (damage, crit) = CalculateDamage();
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit))
            {
                
                currentWeapon.ShootProjectile(hit.point, damage, crit);
            }
            else
            {
                currentWeapon.ShootProjectile(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(50), damage, crit);
            }
            
        }
        #endregion

        #region Damage Calculation
        public (float, bool) CalculateDamage()
        {
            float damage = 0f;
            float critChance = 0f;

            damage = playerAttributes.defaultAttributes[Attributes.Damage.ToString()] + playerAttributes.GetAttributeModifiedValue(Attributes.Damage);

            critChance = playerAttributes.defaultAttributes[Attributes.Critical_Chance.ToString()] + playerAttributes.GetAttributeModifiedValue(Attributes.Critical_Chance);

            bool crit;
            int num = Random.Range(1, 101);
            if (num <= critChance)
            {
                damage *= currentWeapon.CritDamageMultiplier;
                crit = true;
            }
            else
            {
                crit = false;
            }

            return (damage, crit);
        }

        public void DisplayDamage(float damage, bool crit, Transform enemyPosition)
        {
            GameObject go = Instantiate(damagePrefab);
            TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();

            float scale = Vector3.Distance(transform.position, enemyPosition.position);
            scale /= 2;
            scale = Mathf.Clamp(scale, .75f, 3f);

            Vector3 randomPositionCircle = Random.insideUnitSphere;
            go.transform.position = enemyPosition.position + randomPositionCircle * Mathf.Clamp(Vector3.Distance(transform.position, enemyPosition.position), 2f, 5f)/4f;
            go.transform.localScale = Vector3.one * scale;

            go.GetComponentInChildren<SpriteLookAtPlayer>().StartRotation();

            text.color = crit ? critDamage : normalDamage;
            text.text = ((int)damage).ToString();

            go.GetComponent<Animator>().SetTrigger("start");
            StartCoroutine(LerpFloatingDamage(go.transform, 3f));

            Destroy(go, 3f);
        }

        private IEnumerator LerpFloatingDamage(Transform gameobjectTransform, float duration)
        {
            float time = 0;
            Vector3 startPosition = gameobjectTransform.position;
            Vector3 targetPosition = new Vector3(gameobjectTransform.position.x, gameobjectTransform.position.y * 1.5f, gameobjectTransform.position.z);

            while (time < duration)
            {
                gameobjectTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            gameobjectTransform.position = targetPosition;
        }
        #endregion

        #region Equipping & Unequipping
        public void EquipRing(JewelryObject jewelryObject)
        {
            if(currentJewelry == null)
            {
                currentJewelry = jewelryObject;
                jewelryEffectActive = true;
                CheckJewelryType(jewelryObject.jewelryType);
            }
        }

        public void UnequipRing()
        {
            if(currentJewelry != null)
            {
                jewelryEffectActive = false;
                currentJewelry = null;
            }
        }

        public void EquipWeapon(WeaponObject weapon)
        {
            if(currentWeapon == null)
            {
                currentWeapon = weapon;
                currentWeapon.Spawn(weaponsParent);
            }

            SetCurrentWeaponAnimator();
        }

        public void UnequipWeapon()
        {
            if(currentWeapon != null)
            {
                Destroy(currentWeapon.weaponGO);
            }
            
            currentWeapon = null;
            SetCurrentWeaponAnimator();
        }
        #endregion

        #region Jewelry Behaviour
        private void CheckJewelryType(JewelryType jewelryType)
        {
            switch (jewelryType)
            {
                case JewelryType.Fire_Amulet:
                    StartCoroutine(FireAmuletBehaviour());
                    break;
                case JewelryType.Fire_Ring:
                    break;
                default:
                    break;
            }
        }

        private IEnumerator FireAmuletBehaviour()
        {
            float currentTime = 0f;
            while (jewelryEffectActive)
            {
                if(currentTime >= currentJewelry.TimeBetweenAttacks)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, currentJewelry.Range);
                    foreach(Collider col in colliders)
                    {
                        if (col.CompareTag("Enemy"))
                        {
                            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
                            enemyHealth.DealDamage(currentJewelry.Damage);

                            if (currentJewelry.Effect != null)
                            {
                                GameObject go = Instantiate(currentJewelry.Effect, transform.position, Quaternion.identity);
                                go.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                                Destroy(go, 2f);
                            }
                        }
                    }

                    currentTime = 0f;
                }

                currentTime += Time.deltaTime;
                yield return null;
            }
        }
        #endregion
    }
}