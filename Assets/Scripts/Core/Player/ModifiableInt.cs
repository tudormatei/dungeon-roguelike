using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public delegate void ModifiedEvent();
    [System.Serializable]
    public class ModifiableInt
    {
        [SerializeField]
        public int baseValue;
        public int BaseValue { get { return BaseValue; } set { baseValue = value; UpdateModifiedValue(); } }

        [SerializeField]
        private int modifiedValue;
        public int ModifiedValue { get { return modifiedValue; } set { modifiedValue = value; } }

        public List<IModifires> modifiers = new List<IModifires>();

        public event ModifiedEvent ValueModified;
        public ModifiableInt(ModifiedEvent method = null)
        {
            modifiedValue = baseValue;
            if (method != null)
            {
                ValueModified += method;
            }
        }

        public void RegisterModEvent(ModifiedEvent method)
        {
            ValueModified += method;
        }

        public void UnregisterModEvent(ModifiedEvent method)
        {
            ValueModified -= method;
        }

        public void UpdateModifiedValue()
        {
            var valueToAdd = 0;
            for (int i = 0; i < modifiers.Count; i++)
            {
                modifiers[i].AddValue(ref valueToAdd);
            }
            ModifiedValue = baseValue + valueToAdd;
            if (ValueModified != null)
            {
                ValueModified.Invoke();
            }
        }

        public void AddModifier(IModifires _modifier)
        {
            modifiers.Add(_modifier);
            UpdateModifiedValue();
        }

        public void RemoveModifier(IModifires _modifier)
        {
            modifiers.Remove(_modifier);
            UpdateModifiedValue();
        }
    }
}
