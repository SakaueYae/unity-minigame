using System;
using UniRx;
using UnityEngine;

namespace GameScene.UI
{
    /// <summary>
    /// 今開いているキャンバス
    /// </summary>
    
    public enum DisplayStatus
    {
        Start,
        Proceeding,
        Pause,
        Clear,
        GameOver
    }

    public class GameProgressModel
    {
        public ReactiveProperty<DisplayStatus> CurrentDisplayStatus;

        public GameProgressModel(DisplayStatus displayStatus)
        {
            CurrentDisplayStatus = new ReactiveProperty<DisplayStatus>(displayStatus);
        }

        public void ChangeDisplayStatus(DisplayStatus nextStatus)
        {
            CurrentDisplayStatus.Value = nextStatus;
        }
    }
}
