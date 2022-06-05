using Units.Types;

namespace Commands
{
    public class AbilityReleaseCommand : ICommand
    {
        public CommandUpdateType UpdateType { get; } = CommandUpdateType.Refresh;
        
        private readonly Unit _unit;
        private readonly int _abilityNumber;

        public AbilityReleaseCommand(Unit unit, int abilityNumber)
        {
            _unit = unit;
            _abilityNumber = abilityNumber;
        }

        public void Execute()
        {
            _unit.AbilityHandler.OnAbilityRelease?.Invoke(_abilityNumber);
        }

        public void Undo()
        {
        }
    }
}