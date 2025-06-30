using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameScene.Obstacles
{
    public interface IWaterObstacle
    {
        IObservable<Unit> OnWaterCollision();
    }

    public class WaterParticleCollision : MonoBehaviour, IWaterObstacle
    {
        Subject<Unit> _onWaterCollision = new Subject<Unit>();
        public IObservable<Unit> OnWaterCollision() => _onWaterCollision;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.OnParticleTriggerAsObservable().Subscribe(_ =>
            {
                _onWaterCollision.OnNext(Unit.Default);
            }).AddTo(this);
        }
    }
}
