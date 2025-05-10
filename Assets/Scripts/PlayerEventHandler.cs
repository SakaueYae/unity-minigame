using System;
using UniRx;
using UnityEngine;

namespace GameScene.Player
{
    public class PlayerEventHandler:MonoBehaviour
    {
        [SerializeField]
        Player _player;

        IKeyEvent _playerKeyEvent;

        private Subject<Unit> _ketboardEvent = new Subject<Unit>();
        public IObservable<Unit> KeyboardEvent => _ketboardEvent;

        private void Start()
        {
            _playerKeyEvent = _player.GetComponent<IKeyEvent>();

            Observable.EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.RightArrow))
                .Subscribe(_ => _playerKeyEvent.MoveX(XDirection.Right))
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.LeftArrow))
                .Subscribe(_ => _playerKeyEvent.MoveX(XDirection.Left))
                .AddTo(this);
             Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ => _playerKeyEvent.Jump())
                .AddTo(this);

        }
    }
}