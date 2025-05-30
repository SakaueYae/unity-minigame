using UnityEngine;

namespace GameScene.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        float speed;

        Transform _transform;
        float _speed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _transform = GetComponent<Transform>();
            _speed = 0;
        }

        // Update is called once per frame
        void Update()
        {
            _transform.position += new Vector3(_speed, 0, 0);
        }

        public void Move()
        {
            _speed = speed;
        }

        public void Stop()
        {
            _speed = 0;
        }
    }
}
