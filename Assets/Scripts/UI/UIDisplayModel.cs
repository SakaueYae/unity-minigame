using System;
using UniRx;
using UnityEngine;

namespace GameScene.UI
{
    /// <summary>
    /// ���J���Ă���L�����o�X
    /// </summary>
    
    public enum DisplayStatus
    {
        Start,
        Pause,
        Clear,
        GameOver
    }

    public class Model
    {
        public ReactiveProperty<DisplayStatus> CurrentDisplayStatus;

        public Model(DisplayStatus displayStatus)
        {
            CurrentDisplayStatus = new ReactiveProperty<DisplayStatus>(displayStatus);
        }
    }
}
