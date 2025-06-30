using GameScene.GameStatus;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.UI
{
    public class GameProgressButtonView : BaseGameProgressStatusChange
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameObject.GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => ChangeStatus(_displayStatus))
                .AddTo(this);
        }
    }
}
