using System;
using UnityEngine;
using UniRx;
using GameScene.Camera;

namespace GameScene.UI
{
    [Serializable]
    public class GameProgressPresenter : MonoBehaviour
    {
        [Header("IUIDisplayViewÇ™ïKê{")]
        [SerializeReference]
        StartCanvasController startCanvasView;

        [SerializeField]
        CameraController _camera;

        IUIDisplayView[] _views;
        GameProgressModel _model;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _model = new GameProgressModel(DisplayStatus.Start);
            _views = new IUIDisplayView[] { startCanvasView.GetComponent<IUIDisplayView>() };

            foreach (IUIDisplayView view in _views)
            {
                view.OnChangeState().Subscribe(status => _model.ChangeDisplayStatus(status)).AddTo(this);
            }

            _model.CurrentDisplayStatus.Subscribe(status =>
            {
                switch (status)
                {
                    case DisplayStatus.Start:
                        break;
                    case DisplayStatus.Proceeding:
                        _camera.Move();
                        break;
                    case DisplayStatus.Pause:
                        break;
                    case DisplayStatus.Clear:
                        break;
                    case DisplayStatus.GameOver:
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
