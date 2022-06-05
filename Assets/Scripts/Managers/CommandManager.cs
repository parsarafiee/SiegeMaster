using System;
using System.Collections.Generic;
using Commands;
using General;

namespace Managers
{
    public class CommandManager : WrapperManager
    {
        #region Properties and Variables

        private readonly Queue<ICommand> _refreshTypeToExecute;
        private readonly Queue<ICommand> _fixedRefreshTypeToExecute;
        private readonly Queue<ICommand> _refreshTypeToAdd;
        private readonly Queue<ICommand> _fixedRefreshTypeToAdd;
        private readonly Queue<ICommand> _storedCommands;

        #endregion

        #region Singleton

        private static CommandManager _instance;

        public static CommandManager Instance => _instance ??= new CommandManager();

        private CommandManager()
        {
            _refreshTypeToExecute = new Queue<ICommand>();
            _fixedRefreshTypeToExecute = new Queue<ICommand>();
            _storedCommands = new Queue<ICommand>();
            _refreshTypeToAdd = new Queue<ICommand>();
            _fixedRefreshTypeToAdd = new Queue<ICommand>();
        }

        #endregion

        #region Public Methods

        public override void Init()
        {
        }

        public override void PostInit()
        {
        }

        public override void Refresh()
        {
            AddRefreshTypeToExecuteQueue();
            ExecuteRefreshCommands();
        }


        public override void FixedRefresh()
        {
            AddFixedRefreshTypeToExecuteQueue();
            ExecuteFixedRefreshCommands();
        }

        public override void LateRefresh()
        {
            
        }

        public override void Clean()
        {
            _refreshTypeToExecute.Clear();
            _fixedRefreshTypeToExecute.Clear();
            _refreshTypeToAdd.Clear();
        }

        public void Add(ICommand obj)
        {
            switch (obj.UpdateType)
            {
                case CommandUpdateType.Refresh:
                    _refreshTypeToAdd.Enqueue(obj);
                    break;
                case CommandUpdateType.FixedRefresh:
                    _fixedRefreshTypeToAdd.Enqueue(obj);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Private Methods

        private void ExecuteRefreshCommands()
        {
            foreach (var commands in _refreshTypeToExecute)
            {
                commands.Execute();
                _storedCommands.Enqueue(commands);
            }

            _refreshTypeToExecute.Clear();
        }

        private void AddRefreshTypeToExecuteQueue()
        {
            foreach (var command in _refreshTypeToAdd)
            {
                _refreshTypeToExecute.Enqueue(command);
            }

            _refreshTypeToAdd.Clear();
        }

        private void ExecuteFixedRefreshCommands()
        {
            foreach (var commands in _fixedRefreshTypeToExecute)
            {
                commands.Execute();
                _storedCommands.Enqueue(commands);
            }

            _fixedRefreshTypeToExecute.Clear();
        }

        private void AddFixedRefreshTypeToExecuteQueue()
        {
            foreach (var command in _fixedRefreshTypeToAdd)
            {
                _fixedRefreshTypeToExecute.Enqueue(command);
            }

            _fixedRefreshTypeToAdd.Clear();
        }

        #endregion
    }
}