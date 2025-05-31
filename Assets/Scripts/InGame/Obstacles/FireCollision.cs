using System;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

namespace GameScene.Obstacles {
    public interface IFireObstacles {
        IObservable<GameObject> OnFireCollision();
    }

    public class FireCollision : MonoBehaviour, IFireObstacles
    {
        Subject<GameObject> _onFireCollision = new Subject<GameObject>();
        public IObservable<GameObject> OnFireCollision() => _onFireCollision;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.OnParticleCollisionAsObservable().Subscribe(obj =>
            {
                _onFireCollision.OnNext(obj);
            }).AddTo(this);
        }
    }
}
