using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameScene.Obstacles
{
    public interface IWaterObstacles
    {
        IObservable<GameObject> OnWaterCollision();
    }

    public class WaterCollision : MonoBehaviour, IWaterObstacles
    {
        Subject<GameObject> _onWaterCollision = new Subject<GameObject>();
        public IObservable<GameObject> OnWaterCollision() => _onWaterCollision;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.OnParticleCollisionAsObservable().Subscribe(obj =>
            {
                _onWaterCollision.OnNext(obj);
            }).AddTo(this);
        }
    }
}
