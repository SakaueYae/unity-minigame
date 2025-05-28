using System;
using UnityEngine;

namespace GameScene.Audio
{
    [Serializable]
    class AudioList
    {
        [SerializeField]
        string _name;
        [SerializeField]
        AudioClip _clip;

        public AudioClip GetAudioClip(string name)
        {
            if (name == _name) return _clip;
            else return null;
        }
    }

    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        AudioList[] audioLists;

        public static SoundManager Instance = null;

        AudioClip _playClip;
        AudioSource _audioSource;

        private void Awake()
        {
            if (Instance==null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySE(string name)
        {
            foreach (var list in audioLists)
            {
                _playClip = list.GetAudioClip(name);
                if (_playClip) break;
            }

            if (_playClip == null) return;

            _audioSource.PlayOneShot(_playClip);
        }
    }
}
