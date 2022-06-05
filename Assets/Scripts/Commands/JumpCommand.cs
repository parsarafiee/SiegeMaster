using Units.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commands
{
    public class JumpCommand : ICommand
    {
        private readonly IMovable _obj;

        public CommandUpdateType UpdateType => CommandUpdateType.Refresh;

        public JumpCommand(IMovable obj)
        {
            _obj = obj;
       
        }

        public void Execute()
        {
            _obj.Jump();
        }

        public void Undo()
        {

        }
    }
}
