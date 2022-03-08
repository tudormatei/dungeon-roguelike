namespace Dungeon.Saving
{
    /// <summary>
    /// Interface that has to be implemented on every class that will need
    /// a capture state or restore state.
    /// </summary>
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}
