using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace GameScene.UI
{
    public interface IUIDisplayView
    {
        IObservable<DisplayStatus> OnChangeState();
    }

    public class StartCanvasController : MonoBehaviour,IUIDisplayView
    {
        [SerializeField]
        GameObject text;

        TextMeshProUGUI _text;
        AudioSource _audioSource;
        RectTransform _rectTransform;

        Subject<DisplayStatus> _onEndCountdown = new Subject<DisplayStatus>();
        public IObservable<DisplayStatus> OnChangeState() => _onEndCountdown;

        // Start is called once before the first execution of Update after the MonoBehaviour is created  
        void Start()
        {
            _text = text.GetComponent<TextMeshProUGUI>();
            _audioSource = GetComponent<AudioSource>();
            //_rectTransform = text.GetComponent<RectTransform>();
            //var sequence = DOTween.Sequence();

            //for (int i = 3; i > 0; i--)
            //{
            //    FadeIn((i).ToString(), sequence);
            //}

            //sequence.Play();
            CountdownEvent(_text).Forget();
        }

        // Update is called once per frame  
        void Update()
        {

        }


        async UniTask CountdownEvent(TextMeshProUGUI text)
        {
            int sec = 3;
            _audioSource.Play();
            Time.timeScale = 0;

            while (sec > 0)
            {
                text.text = sec.ToString();
                await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.UnscaledDeltaTime);
                sec--;
            }
            text.text = "Start!";
            await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.UnscaledDeltaTime);

            Time.timeScale = 1;
            _onEndCountdown.OnNext(DisplayStatus.Proceeding);

            gameObject.SetActive(false);
        }

        void FadeIn(string countText, Sequence sequence)
        {
            _text.text = countText;

            _text.transform.localScale = Vector3.zero;
            _text.transform.DOScale(1.5f, 0.3f).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _text.transform.DOScale(1.0f, 0.2f);
                });

            // フェードインアニメーション  
            _text.alpha = 0;
            sequence.Join(_text.DOFade(1, 0.3f)).OnComplete(() => _text.DOFade(0.0f, 0.5f));

        }
    }
}
