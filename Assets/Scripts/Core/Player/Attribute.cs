using Core.ItemManagement;

namespace Core.Player
{
    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized]
        public PlayerAttributes parent;
        public Attributes type;
        public ModifiableInt value;

        public void SetParent(PlayerAttributes _parent)
        {
            parent = _parent;
            value = new ModifiableInt(AttributeModified);
        }

        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }

}
