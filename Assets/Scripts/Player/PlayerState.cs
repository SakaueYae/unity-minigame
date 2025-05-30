using GameScene.Audio;
using UniRx;
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

        void Move(bool isRight, Rigidbody2D rb2)
        {
            if (isRight)
            {
                if (rb2.linearVelocityX > maxVelocity) return;
                rb2.AddForce(new Vector2(force, 0));
            }
            else
            {
                if (rb2.linearVelocityX < maxVelocity * (-1)) return;
                rb2.AddForce(new Vector2(force * -1, 0));
            }
        }

        /// <summary>
        /// ï‡çsèÛë‘
        /// </summary>
        class WalkState : IPlayerState
        {
            public void OnEnter(Player player)
            {

            }

            public void OnUpdate(Player player)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    player.Move(true, player.GetComponent<Rigidbody2D>()); // Fixed: Use instance method
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    player.Move(false, player.GetComponent<Rigidbody2D>()); // Fixed: Use instance method
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.ChangeState(player._jumpState);
                }
            }


            public void OnExit(Player player)
            {

            }
        }

        /// <summary>
        /// ÉWÉÉÉìÉvèÛë‘
        /// </summary>
        class JumpState : IPlayerState
        {
            private Rigidbody2D _rb2;


            public void OnEnter(Player player)
            {
                _rb2 = player.GetComponent<Rigidbody2D>();
                _rb2.AddForce(new Vector2(0, player.jumpForce));
                SoundManager.Instance.PlaySE("Jump");
            }

            public void OnUpdate(Player player)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    player.Move(true, player.GetComponent<Rigidbody2D>());
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    player.Move(false, player.GetComponent<Rigidbody2D>());
                }
            }

            public void OnExit(Player player)
            {

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
