using System;
using GameScene.UI;
using UniRx;
using UnityEngine;

namespace GameScene.GameStatus
{
    public abstract class BaseGameProgressStatusChange : MonoBehaviour
    {
        [SerializeField] protected DisplayStatus _displayStatus;

        protected Subject<DisplayStatus> _onChangeStatus = new Subject<DisplayStatus>();
        public IObservable<DisplayStatus> OnChangeStatus() => _onChangeStatus;

        protected void ChangeStatus(DisplayStatus status)
        {
            _onChangeStatus.OnNext(status);
        }
    }
}
