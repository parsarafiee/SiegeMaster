using Units.Types;

namespace Commands
{
    public class SwitchCommand : ICommand
    {
        private readonly Unit _unit;

        public CommandUpdateType UpdateType => CommandUpdateType.Refresh;

        public SwitchCommand(Unit unit)
        {
            _unit = unit;
        }

        public void Execute()
        {
            _unit.AbilityHandler.ToggleBuildMode.Invoke();
        }

        public void Undo()
        {

        }
    }
}