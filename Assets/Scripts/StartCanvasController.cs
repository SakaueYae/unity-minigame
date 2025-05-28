using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject text;

    TextMeshProUGUI _text;
    AudioSource _audioSource;
    RectTransform _rectTransform;

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

        while (sec>0)
        {
            _text.text = sec.ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.UnscaledDeltaTime);
            sec--;
        }
        _text.text = "Start!";
        await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.UnscaledDeltaTime);
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
        sequence.Join(_text.DOFade(1, 0.3f)).OnComplete(()=>_text.DOFade(0.0f,0.5f));

    }
}
