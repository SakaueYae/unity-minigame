using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils {
    public class ReloadScene : MonoBehaviour
    {
        void Start()
        {
            this.GetComponent<Button>().OnClickAsObservable()
                .Subscribe(_ => SceneManager.LoadScene(SceneManager.GetActiveScene().name))
                .AddTo(this);
        }
    }
}
