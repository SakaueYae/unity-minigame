using UnityEngine;
using Utils.UI;
using UnityEngine.UI;
using UniRx;

namespace GameScene.UI
{
    public class MenuButton : ButtonView<string>
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetComponent<Button>().OnClickAsObservable().Subscribe(_ => _onClick.OnNext("Menu")).AddTo(this);
        }
    }
}
