using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Utils.UI
{
    public interface IButtonClickEvent<T>
    {
        IObservable<T> OnClick();
    }

    public abstract class ButtonView<T> : MonoBehaviour, IButtonClickEvent<T>
    {
        protected Subject<T> _onClick = new Subject<T>();
        public IObservable<T> OnClick() => _onClick;
        //protected abstract void OnClickAction();
    }

    public class BaseButton : ButtonView<Unit>
    {
        void Start()
        {
            GetComponent<Button>().OnClickAsObservable().Subscribe(_ => _onClick.OnNext(_));
        }

        //protected override void OnClickAction()
        //{
        //    Debug.Log("íäè€ÉNÉâÉXÇæÇÊ");
        //}
    }
}
