using Units.Types;

namespace Commands
{
    public class AbilityPressCommand : ICommand
    {
        public CommandUpdateType UpdateType { get; } = CommandUpdateType.Refresh;

        private readonly int _abilityNumber;
        private readonly Unit _unit;

        public AbilityPressCommand(Unit unit, int abilityNumber)
        {
            _abilityNumber = abilityNumber;
            _unit = unit;
        }

        public void Execute()
        {
            _unit.AbilityHandler.OnAbilityPress?.Invoke(_abilityNumber);
        }

        public void Undo()
        {
            
        }
    }
}