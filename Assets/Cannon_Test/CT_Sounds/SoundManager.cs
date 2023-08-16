using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class SoundManager : MonoBehaviour
    {
        [Inject] private PlayerControl _playerControl;
        [Inject] private ShootingSounds _shootingSounds;

        private AudioClip _audioData;
        private AudioSource _audioSource = new AudioSource();
        private float _volumeScale = 100.0f;

        public AudioClip AudioData;

        private void Awake()
        {
            _audioData = _shootingSounds.audioClips[0];
            _audioSource.PlayOneShot(_audioData, _volumeScale);  //???????????????????????????
            //_volumeScale = 1.0f;
            //Debug.Log("Сука, ну остановить!");
        }

    }
}
