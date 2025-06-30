using System;
using UnityEngine;
using UniRx;
using GameScene.Camera;
using GameScene.GameStatus;
using System.Linq;

namespace GameScene.UI
{
    [Serializable]
    public class GameProgressPresenter : MonoBehaviour
    {
        [SerializeField]
        CameraController _camera;
        [SerializeField]
        GameObject[] _viewsObj;
        GameProgressModel _model;
        IGameProgressView[] _views;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _model = new GameProgressModel(DisplayStatus.Start);
            _views = _viewsObj.Select(obj => obj.GetComponent<IGameProgressView>()).ToArray();

            foreach (IGameProgressView view in _views)
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
                        _camera.Stop();
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
