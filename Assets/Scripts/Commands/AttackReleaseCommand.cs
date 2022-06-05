using Units.Types;

namespace Commands
{
    public class AttackReleaseCommand : ICommand
    {
        public CommandUpdateType UpdateType { get; } =  CommandUpdateType.Refresh;

        private readonly Unit _unit;
        
        public AttackReleaseCommand(Unit unit)
        {
            _unit = unit;
        }
        
        public void Execute()
        {
            _unit.AbilityHandler.OnFireRelease?.Invoke();
        }

        public void Undo()
        {
            
        }
    }
}