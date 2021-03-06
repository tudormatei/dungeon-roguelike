using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dungeon.UI
{
    /// <summary>
    /// Register UI tab in main menu.
    /// </summary>
    public class RegisterTab : MonoBehaviour
    {
        private List<Selectable> m_orderedSelectables;
        private Controls controls;

        void OnEnable()
        {
            controls.Enable();
        }
        void OnDisable()
        {
            controls.Disable();
        }

        private void Awake()
        {
            m_orderedSelectables = new List<Selectable>();
            controls = new Controls();
            controls.Gameplay.TAB.performed += ctx => HandleHotkeySelect(false, true, false);
        }


        private void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HandleHotkeySelect(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift), true, false); // Navigate backward when holding shift, else navigate forward.
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                HandleHotkeySelect(false, false, true);
            }
            */
        }

        private void HandleHotkeySelect(bool _isNavigateBackward, bool _isWrapAround, bool _isEnterSelect)
        {
            SortSelectables();

            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            if (selectedObject != null && selectedObject.activeInHierarchy) // Ensure a selection exists and is not an inactive object.
            {
                Selectable currentSelection = selectedObject.GetComponent<Selectable>();
                if (currentSelection != null)
                {
                    if (_isEnterSelect)
                    {
                        if (currentSelection.GetComponent<InputField>() != null)
                        {
                            ApplyEnterSelect(FindNextSelectable(m_orderedSelectables.IndexOf(currentSelection), _isNavigateBackward, _isWrapAround));
                        }
                    }
                    else // Tab select.
                    {
                        Selectable nextSelection = FindNextSelectable(m_orderedSelectables.IndexOf(currentSelection), _isNavigateBackward, _isWrapAround);
                        print("trying...");
                        if (nextSelection != null)
                        {
                            print(nextSelection.gameObject.name);
                            nextSelection.Select();
                        }
                    }
                }
                else
                {
                    SelectFirstSelectable(_isEnterSelect);
                }
            }
            else
            {
                SelectFirstSelectable(_isEnterSelect);
            }
        }

        ///<summary> Selects an input field or button, activating the button if one is found. </summary>
        private void ApplyEnterSelect(Selectable _selectionToApply)
        {
            if (_selectionToApply != null)
            {
                if (_selectionToApply.GetComponent<InputField>() != null)
                {
                    _selectionToApply.Select();
                }
                else
                {
                    Button selectedButton = _selectionToApply.GetComponent<Button>();
                    if (selectedButton != null)
                    {
                        _selectionToApply.Select();
                        selectedButton.OnPointerClick(new PointerEventData(EventSystem.current));
                    }
                }
            }
        }

        private void SelectFirstSelectable(bool _isEnterSelect)
        {
            if (m_orderedSelectables.Count > 0)
            {
                Selectable firstSelectable = m_orderedSelectables[0];
                if (_isEnterSelect)
                {
                    ApplyEnterSelect(firstSelectable);
                }
                else
                {
                    firstSelectable.Select();
                }
            }
        }

        private Selectable FindNextSelectable(int _currentSelectableIndex, bool _isNavigateBackward, bool _isWrapAround)
        {
            Selectable nextSelection = null;

            int totalSelectables = m_orderedSelectables.Count;
            if (totalSelectables > 1)
            {
                if (_currentSelectableIndex == (totalSelectables - 1))
                {
                    nextSelection = (_isWrapAround) ? m_orderedSelectables[0] : null;
                }
                else
                {
                    nextSelection = m_orderedSelectables[_currentSelectableIndex + 2];
                }
            }

            return (nextSelection);
        }

        private void SortSelectables()
        {
            Selectable[] originalSelectables = Selectable.allSelectablesArray;
            int totalSelectables = originalSelectables.Length;
            m_orderedSelectables = new List<Selectable>(totalSelectables);
            for (int index = 0; index < totalSelectables; ++index)
            {
                Selectable selectable = originalSelectables[index];
                m_orderedSelectables.Insert(FindSortedIndexForSelectable(index, selectable), selectable);
            }
        }

        ///<summary> Recursively finds the sorted index by positional order within m_orderedSelectables (positional order is determined from left-to-right followed by top-to-bottom). </summary>
        private int FindSortedIndexForSelectable(int _selectableIndex, Selectable _selectableToSort)
        {
            int sortedIndex = _selectableIndex;
            if (_selectableIndex > 0)
            {
                int previousIndex = _selectableIndex - 1;
                Vector3 previousSelectablePosition = m_orderedSelectables[previousIndex].transform.position;
                Vector3 selectablePositionToSort = _selectableToSort.transform.position;

                if (previousSelectablePosition.y == selectablePositionToSort.y)
                {
                    if (previousSelectablePosition.x > selectablePositionToSort.x)
                    {
                        // Previous selectable is in front, try the previous index:
                        sortedIndex = FindSortedIndexForSelectable(previousIndex, _selectableToSort);
                    }
                }
                else if (previousSelectablePosition.y < selectablePositionToSort.y)
                {
                    // Previous selectable is in front, try the previous index:
                    sortedIndex = FindSortedIndexForSelectable(previousIndex, _selectableToSort);
                }
            }

            return (sortedIndex);
        }
    }

}
