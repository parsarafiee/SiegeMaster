using Units.Types;

namespace Commands
{
    public class AttackPressCommand : ICommand
    {
        public CommandUpdateType UpdateType { get; } =  CommandUpdateType.Refresh;

        private readonly Unit _unit;
        
        public AttackPressCommand(Unit unit)
        {
            _unit = unit;
        }
        
        public void Execute()
        {
            _unit.AbilityHandler.OnFirePress?.Invoke();
        }

        public void Undo()
        {
            
        }
    }
}