using System;
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

    public class Player : MonoBehaviour,IKeyEvent,ICollisionEvent<Collision2D>
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


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb2 = GetComponent<Rigidbody2D>();

            this.OnCollisionEnter2DAsObservable().Subscribe(collision => _onColiision.OnNext(collision));
        }


        public void MoveX(XDirection dir)
        {
            if (dir==XDirection.Right)
            {
                if (_rb2.linearVelocityX > maxVelocity) return;
                _rb2.AddForce(new Vector2(force, 0));
            }
            else
            {
                if (_rb2.linearVelocityX < maxVelocity * (-1)) return;
                _rb2.AddForce(new Vector2(force * -1, 0));
            }
        }

        public void Jump()
        {
            _rb2.AddForce(new Vector2(0, jumpForce));
        }
    }

}