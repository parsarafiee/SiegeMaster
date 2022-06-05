using Units.Interfaces;
using UnityEngine;

namespace Commands
{
    public class LookCommand : ICommand
    {
        public CommandUpdateType UpdateType { get; } = CommandUpdateType.FixedRefresh;
        
        private readonly ICameraController _player;

        public LookCommand(ICameraController player)
        {
            _player = player;
        }
        public void Execute()
        {
            _player.Look();
        }

        public void Undo()
        {
            
        }
    }
}