using Units.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commands
{
    public class MoveCommand : ICommand
    {
        private readonly Vector3 _direction;
        private readonly IMovable _obj;

        public CommandUpdateType UpdateType => CommandUpdateType.FixedRefresh;

        public MoveCommand(IMovable obj, Vector3 direction)
        {
            _obj = obj;
            _direction = direction;
        }

        public void Execute()
        {
           _obj.Move(_direction);
        }

        public void Undo()
        {
           
        }
    }
}
