using System;
using UnityEngine;
using UniRx;
using GameScene.Camera;

namespace GameScene.UI
{
    [Serializable]
    public class UIDisplayPrevew : MonoBehaviour
    {
        [Header("IUIDisplayViewÇ™ïKê{")]
        [SerializeReference]
        StartCanvasController startCanvasView;

        [SerializeField]
        CameraController _camera;

        IUIDisplayView[] _views;
        UIDisplayModel _model;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _model = new UIDisplayModel(DisplayStatus.Start);
            _views = new IUIDisplayView[] { startCanvasView.GetComponent<IUIDisplayView>() };

            foreach (IUIDisplayView view in _views)
            {
                view.OnChangeState().Subscribe(status => _model.ChangeDisplayStatus(status)).AddTo(this);
            }

            _model.CurrentDisplayStatus.Subscribe(status =>
            {
                _camera.Move();
            });
        }
    }
}
