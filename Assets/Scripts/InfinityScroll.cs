using GameScene.Player;
using UniRx;
using UnityEngine;

namespace GamaScene.UI
{
    interface IInfinityScroll
    {

    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class InfinityScroll : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        Transform _transform;
        float width = 0.0f;
        bool isFirst = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = GetComponent<Transform>();
            width = _spriteRenderer.bounds.size.x;
            Debug.Log(width);

            Observable.EveryUpdate()
                .Where(_ => _spriteRenderer.isVisible)
                .Subscribe(_ => isFirst=false)
                .AddTo(this);

            Observable.EveryUpdate()
                .Where(_ => !_spriteRenderer.isVisible && !isFirst)
                .Subscribe(_ => MovePosition())
                .AddTo(this);
        }

        void MovePosition()
        {
            _transform.position += new Vector3(width * 2, 0, 0);
            isFirst = true;
        }
    }

}