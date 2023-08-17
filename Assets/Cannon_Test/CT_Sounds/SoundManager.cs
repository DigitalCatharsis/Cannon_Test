using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class SoundManager : MonoBehaviour
    {
        [Inject] private PlayerControl _playerControl;

        [Header("Sound Collections")]
        [SerializeField] private SoundClipsCollection _shootingSounds;
        [SerializeField] private SoundClipsCollection _musicSounds;
        [SerializeField] private SoundClipsCollection _powerUpSounds;

        [Header("AudioSources")]
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _powerUpAudioSource;

        public void SubscribeToPowerUp(PowerUpControl powerUpControl)
        {
            powerUpControl.OnInvokePowerUp += PlayPowerUpSound;
        }
        public void UnsubscribeFromPowerUp(PowerUpControl powerUpControl)
        {
            powerUpControl.OnInvokePowerUp -= PlayPowerUpSound;
        }

        public void ShootSound()
        {
            _shootAudioSource.PlayOneShot(GetRandomAudioClip(_shootingSounds), _shootAudioSource.volume);
        }
        public void GetPowerUpSound(string powerUpType)
        {
            AudioClip audioClip = _powerUpSounds.audioClips.Find(x => x.name == powerUpType);

            _powerUpAudioSource.PlayOneShot(audioClip, 0.7F);
            Debug.Log(_powerUpAudioSource.volume);
        }

        public void PlayPowerUpSound(PowerUpControl powerUpControl, PowerUpType powerUpType)
        {
            GetPowerUpSound(powerUpType.ToString());
        }

        private AudioClip GetRandomAudioClip(SoundClipsCollection clipsCollection)
        {
            var randomIndex = UnityEngine.Random.Range(0, (clipsCollection.audioClips.Count - 1));
            var randomValue = _shootingSounds.audioClips[randomIndex];
            return randomValue;
        }

    }
}
