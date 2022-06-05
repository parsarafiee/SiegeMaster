using System.Collections.Generic;

using Commands;
using General;
using Managers;
using Units.Types;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Inputs
{
    /// <summary>
    /// This class polls the player inputs by reading the value of the binding key associated to an action in
    /// the PlayerInput component of Unity. Depending on which action was done, this class will create the proper
    /// command. With this approach you can rebind key easily in the PlayerInput component without changing the code.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour, IUpdatable
    {

        #region Properties and Variables
        //Camera

        private Transform _mainCamera;
        
        //ComponentsCache
        private PlayerInput _playerInput;
        private PlayerUnit _unit;
        //ActionCache
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _lookAction;
        private InputAction _basicAttackAction;
        private InputAction _ability1Action;
        private InputAction _ability2Action;
        private InputAction _ability3Action;
        private InputAction _ability4Action;
        private InputAction _toggleBuildModeAction;
        
        private InputAction[] _abilityActions;

        //Action Name in PlayerInput Component
        private const string Look = "Look";
        private const string Movement = "Movement";
        private const string Jump = "Jump";
        private const string BasicAttack = "BasicAttack";
        private const string Ability1 = "Ability1";
        private const string Ability2 = "Ability2";
        private const string Ability3 = "Ability3";
        private const string Ability4 = "Ability4";
        private const string SwitchMode = "SwitchMode";

        #endregion
        
        #region IUpdatable (Init, Refresh...)
        public void Init()
        {
            //Get Camera Reference
            if (Camera.main != null) _mainCamera = Camera.main.transform;
            else Debug.Log("No Main Camera Found");

            //Cache Components
            _playerInput = GetComponent<PlayerInput>();
            _unit = GetComponent<PlayerUnit>();

            //Locking mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }

        public void PostInit()
        {
            //Cache InputActions
            _moveAction = _playerInput.actions[Movement];
            _jumpAction = _playerInput.actions[Jump];
            _lookAction = _playerInput.actions[Look];
            _basicAttackAction = _playerInput.actions[BasicAttack];
            _ability1Action = _playerInput.actions[Ability1];
            _ability2Action = _playerInput.actions[Ability2];
            _ability3Action = _playerInput.actions[Ability3];
            _ability4Action = _playerInput.actions[Ability4];
            _toggleBuildModeAction = _playerInput.actions[SwitchMode];
            
            
            //Add Ability to an Array (Useful to avoid typing the same code multiple time in the future.
            //Since polling for those is the same and the ability handler also has the proper ability in an array)
            _abilityActions = new [] {_ability1Action, _ability2Action, _ability3Action, _ability4Action};
        }

        public void Refresh()
        {
            PollFireInput();
            PollAbilityInput();
            PollJumpInput();
            PollSwitchInput();
        }

        public void FixedRefresh()
        {
            PollLookInput();
            PollMovementInput();
           
        }

        public void LateRefresh()
        {
        }

        #endregion

        #region Private Methods
        
        private void PollMovementInput()
        {
            if (!(_moveAction.ReadValue<Vector2>() is var movementInput) || movementInput == Vector2.zero) return;
            var direction = new Vector3(movementInput.x, 0, movementInput.y);
            direction = direction.x * _mainCamera.right.normalized +
                        direction.z * _mainCamera.forward.normalized;
            CommandManager.Instance.Add(new MoveCommand(_unit, direction));
        }

        private void PollLookInput()
        {
            if (!(_lookAction.ReadValue<Vector2>() is var mouseDelta) || mouseDelta == Vector2.zero) return;

            CommandManager.Instance.Add(new LookCommand(_unit));
        }

        private void PollFireInput()
        { 
            if (_basicAttackAction.WasPressedThisFrame())
            {
                CommandManager.Instance.Add(new AttackPressCommand(_unit));
            }
            else if (_basicAttackAction.WasReleasedThisFrame())
            {
                CommandManager.Instance.Add(new AttackReleaseCommand(_unit));
            }
        }
        private void PollJumpInput()
        {
            if (_jumpAction.WasPressedThisFrame())
            {
                CommandManager.Instance.Add(new JumpCommand(_unit));
            }
        }

        private void PollAbilityInput()
        {
            for (var i = 0; i < _abilityActions.Length; i++)
            {
                if (_abilityActions[i].WasPressedThisFrame())
                {
                    CommandManager.Instance.Add(new AbilityPressCommand(_unit, i));
                }
                else if (_abilityActions[i].WasReleasedThisFrame())
                {
                    CommandManager.Instance.Add(new AbilityReleaseCommand(_unit, i));
                }
            }
        }

        private void PollSwitchInput()
        {
            if (_toggleBuildModeAction.WasPressedThisFrame())
            {
                CommandManager.Instance.Add(new SwitchCommand(_unit));
            }
        }
        #endregion
        
    }
}