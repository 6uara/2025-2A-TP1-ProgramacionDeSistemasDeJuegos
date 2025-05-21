using UnityEngine;

namespace Gameplay.JumpStates
{
    public interface IJumpState
    {
        void HandleJump();
        void OnLand();
        bool IsAirborne { get; }
    }

    public class JumpState : IJumpState
    {
        private readonly PlayerController _controller;
        private readonly int _maxJumps;
        private int _jumpsUsed = 0;

        public JumpState(PlayerController controller, int maxJumps)
        {
            _controller = controller;
            _maxJumps = maxJumps;
        }

        public bool IsAirborne => _jumpsUsed > 0;

        public void HandleJump()
        {
            if (_jumpsUsed >= _maxJumps)
                return;

            _controller.RunJump();
            _jumpsUsed++;
        }

        public void OnLand()
        {
            _jumpsUsed = 0;
        }
    }
}
