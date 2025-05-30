using System;
using GameScene.Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameScene.Player
{
    public enum XDirection
    {
        Right,
        Left
    }

    interface IKeyEvent
    {
        //public IObserver<Unit> KeyEventObserver();
        public void MoveX(XDirection dir);
        public void Jump();
    }

    interface ICollisionEvent<T>
    {
        public IObservable<T> OnCollision();
    }

    public partial class Player : MonoBehaviour,ICollisionEvent<Collision2D>
    {
        [SerializeField]
        float force = 0.0f;
        [SerializeField]
        float jumpForce = 0.0f;
        [SerializeField]
        float maxVelocity = 0.0f;

        private Subject<Collision2D> _onColiision = new Subject<Collision2D>();
        public IObservable<Collision2D> OnCollision() => _onColiision;

        Rigidbody2D _rb2;
        Animator _animator;

        WalkState _walkState;
        JumpState _jumpState;
        BurstState _burstState;
        WetState _wetState;
        FallState _fallState;
        ClearState _clearState;

        IPlayerState _currentState;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb2 = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _walkState = new WalkState();
            _jumpState = new JumpState();
            _burstState = new BurstState();
            _wetState = new WetState();
            _fallState = new FallState();
            _clearState = new ClearState();
            _currentState = _walkState;
            _currentState.OnEnter(this);

            this.OnCollisionEnter2DAsObservable().Subscribe(collision =>
            {
                //_onColiision.OnNext(collision);
                ChangeState(_walkState);
            }).AddTo(this);
            this.OnCollisionEnter2DAsObservable()
                .Where(collision => collision.gameObject.TryGetComponent<IEnemy>(out var emeny))
                .Subscribe(collision =>
                {
                    ChangeState(_burstState);
                }).AddTo(this);
        }

        void Update()
        {
            _currentState.OnUpdate(this);
        }


        public void ToggleAnimation(PlayerStatus status)
        {
            Time.timeScale = 0;
            switch (status)
            {
                case PlayerStatus.Burst:
                    _animator.SetBool("isBurst", true);
                    break;
                case PlayerStatus.Wet:
                    break;
                case PlayerStatus.Clear:
                    break;
                default:
                    break;
            }
        }

        void ChangeState(IPlayerState newState)
        {
            _currentState.OnExit(this);
            _currentState = newState;
            _currentState.OnEnter(this);
        }
    }

}