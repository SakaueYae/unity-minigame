using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameScene.Obstacles {
    public class WaterObjectCollision : MonoBehaviour, IWaterObstacle
    {
        Subject<Unit> _onWaterCollision = new Subject<Unit>();
        public IObservable<Unit> OnWaterCollision() => _onWaterCollision;

        void Start()
        {
            this.OnCollisionEnter2DAsObservable().Subscribe(_ => _onWaterCollision.OnNext(Unit.Default)).AddTo(this);
        }
    }
}
