using Units.Types;

namespace Commands
{
    public interface ICommand
    {
        public abstract CommandUpdateType UpdateType { get; }
        public abstract void Execute();
        public abstract void Undo();
    }

    public enum CommandUpdateType
    {
        Refresh,
        FixedRefresh
    }
}
