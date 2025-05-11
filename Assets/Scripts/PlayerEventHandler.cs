using System;
using GameScene.Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameScene.Player
{
    public class PlayerEventHandler:MonoBehaviour
    {
        [SerializeField]
        Player _player;

        IKeyEvent _playerKeyEvent;
        ICollisionEvent<Collision2D> _playerCollisionEvent;

        private Subject<Unit> _ketboardEvent = new Subject<Unit>();
        private bool isJumping = false;

        public IObservable<Unit> KeyboardEvent => _ketboardEvent;

        private void Start()
        {
            _playerKeyEvent = _player.GetComponent<IKeyEvent>();
            _playerCollisionEvent = _player.GetComponent<ICollisionEvent<Collision2D>>();

            Observable.EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.RightArrow))
                .Subscribe(_ => _playerKeyEvent.MoveX(XDirection.Right))
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.LeftArrow))
                .Subscribe(_ => _playerKeyEvent.MoveX(XDirection.Left))
                .AddTo(this);
             Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space)&&!isJumping)
                .Subscribe(_ => { _playerKeyEvent.Jump(); isJumping = true; })
                .AddTo(this);

            _playerCollisionEvent.OnCollision().Subscribe((collision) => HandleCollisionEvent(collision));
        }

        void HandleCollisionEvent(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IEnemy>() != null)
            {
                Debug.Log("game over");
                Debug.Log(collision.gameObject);
            }
            else
            {
                isJumping = false;
            }
        }
    }
}