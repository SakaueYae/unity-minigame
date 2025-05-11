using UnityEngine;

namespace GameScene.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        float speed;

        Transform _transform;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            _transform.position += new Vector3(speed, 0, 0);
        }
    }
}
