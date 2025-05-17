using UnityEngine;

namespace Gameplay.JumpStates
{
    public interface IJumpState
    {
        void Enter();
        void HandleJump();
        void OnLand();
    }
    public class GroundedState : IJumpState
    {
        private readonly PlayerController _controller;

        public GroundedState(PlayerController controller)
        {
            _controller = controller;
        }

        public void Enter() { }

        public void HandleJump()
        {
            _controller.RunJump();
            _controller.SetJumpState(new FirstJumpState(_controller));
        }

        public void OnLand() { }
    }

    public class FirstJumpState : IJumpState
    {
        private readonly PlayerController _controller;

        public FirstJumpState(PlayerController controller)
        {
            _controller = controller;
        }

        public void Enter() { }

        public void HandleJump()
        {
            _controller.RunJump();
            _controller.SetJumpState(new DoubleJumpState(_controller));
        }

        public void OnLand()
        {
            _controller.SetJumpState(new GroundedState(_controller));
        }
    }

    public class DoubleJumpState : IJumpState
    {
        private readonly PlayerController _controller;

        public DoubleJumpState(PlayerController controller)
        {
            _controller = controller;
        }

        public void Enter() { }

        public void HandleJump()
        {
            return; // No action on double jump
        }

        public void OnLand()
        {
            _controller.SetJumpState(new GroundedState(_controller));
        }
    }
}
