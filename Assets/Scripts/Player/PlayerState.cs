using UnityEngine;

namespace GameScene.Player
{
    public enum PlayerStatus
    {
        Jump,
        Walk,
        Burst,
        Wet,
        Fall,
        Clear,
    }

    public partial class Player
    {
        public interface IPlayerState
        {
            void OnEnter(Player player);
            void OnUpdate(Player player);
            void OnExit(Player player);
        }

        /// <summary>
        /// ï‡çsèÛë‘
        /// </summary>
        class WalkState : IPlayerState
        {
            private Rigidbody2D _rb2;
            private float _force;
            private float _maxVelocity;

            public WalkState(float force, float maxVelocity)
            {
                _force = force;
                _maxVelocity = maxVelocity;
            }

            public void OnEnter(Player player)
            {
                player.ToggleAnimation(PlayerStatus.Walk);
                _rb2 = player.GetComponent<Rigidbody2D>();
            }

            public void OnUpdate(Player player)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Move(true);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Move(false);
                }
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    player.ChangeState(player._jumpState);
                }
            }


            public void OnExit(Player player)
            {
                // Handle exit logic if needed
            }

            void Move(bool isRight)
            {
                if (isRight)
                {
                    if (_rb2.linearVelocityX > _maxVelocity) return;
                    _rb2.AddForce(new Vector2(_force, 0));
                }
                else
                {
                    if (_rb2.linearVelocityX < _maxVelocity * (-1)) return;
                    _rb2.AddForce(new Vector2(_force * -1, 0));
                }
            }
        }

        /// <summary>
        /// ÉWÉÉÉìÉvèÛë‘
        /// </summary>
        class JumpState : IPlayerState
        {
            private Rigidbody2D _rb2;
            private float jumpForce;

            public JumpState(float jumpForce)
            {
                this.jumpForce = jumpForce;
            }

            public void OnEnter(Player player)
            {
                _rb2 = player.GetComponent<Rigidbody2D>();
                player.ToggleAnimation(PlayerStatus.Jump);
                _rb2.AddForce(new Vector2(0, jumpForce));
            }

            public void OnUpdate(Player player)
            {

            }

            public void OnExit(Player player)
            {
                // Handle exit logic if needed
            }
        }

        class BurstState : IPlayerState
        {
            public void OnEnter(Player player)
            {
                player.ToggleAnimation(PlayerStatus.Burst);
            }
            public void OnUpdate(Player player)
            {
                // Handle burst logic if needed
            }
            public void OnExit(Player player)
            {
                // Handle exit logic if needed
            }
        }

        class WetState : IPlayerState
        {
            public void OnEnter(Player player)
            {
                player.ToggleAnimation(PlayerStatus.Wet);
            }
            public void OnUpdate(Player player)
            {
                // Handle wet logic if needed
            }
            public void OnExit(Player player)
            {
                // Handle exit logic if needed
            }
        }

        class FallState : IPlayerState
        {
            public void OnEnter(Player player)
            {
                player.ToggleAnimation(PlayerStatus.Fall);
            }
            public void OnUpdate(Player player)
            {
                // Handle fall logic if needed
            }
            public void OnExit(Player player)
            {
                // Handle exit logic if needed
            }
        }

        class ClearState : IPlayerState
        {
            public void OnEnter(Player player)
            {
                player.ToggleAnimation(PlayerStatus.Clear);
            }
            public void OnUpdate(Player player)
            {
                // Handle clear logic if needed
            }
            public void OnExit(Player player)
            {
                // Handle exit logic if needed
            }
        }
    }
}
