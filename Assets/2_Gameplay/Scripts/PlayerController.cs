using Gameplay.JumpStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;
        [SerializeField] private int maxJumps = 2;

        private Character _character;
        private Coroutine _jumpCoroutine;
        private IJumpState _jumpState;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _jumpState = new JumpState(this, maxJumps);
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }

            if (jumpInput?.action != null)
                jumpInput.action.performed += HandleJumpInput;
        }

        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started -= HandleMoveInput;
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }

            if (jumpInput?.action != null)
                jumpInput.action.performed -= HandleJumpInput;
        }

        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            direction *= _jumpState.IsAirborne ? airborneSpeedMultiplier : 1f;
            _character.SetDirection(direction);
        }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            _jumpState.HandleJump();
        }

        public void RunJump()
        {
            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = StartCoroutine(_character.Jump());
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5f)
                {
                    _jumpState.OnLand();
                }
            }
        }
    }
}
