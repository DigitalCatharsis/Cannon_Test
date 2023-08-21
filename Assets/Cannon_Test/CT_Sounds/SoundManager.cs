using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
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
        [SerializeField] private SoundClipsCollection _hitSounds;
        [SerializeField] private SoundClipsCollection _playerDeathSounds;
        [SerializeField] private SoundClipsCollection _creditsSounds;
        [SerializeField] private SoundClipsCollection _leaderboardAudioSounds;

        [Header("AudioSources")]
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private AudioSource _powerUpAudioSource;
        [SerializeField] private AudioSource _hitAudioSource;
        [SerializeField] private AudioSource _playerDeathAudioSource;
        [SerializeField] private AudioSource _creditsAudioSource;
        [SerializeField] private AudioSource _leaderboardAudioSource;

        //public void SubscribeToPowerUp(PowerUpControl powerUpControl)
        //{
        //    powerUpControl.OnInvokePowerUp += PlayPowerUpSound;
        //}
        //public void UnsubscribeFromPowerUp(PowerUpControl powerUpControl)
        //{
        //    powerUpControl.OnInvokePowerUp -= PlayPowerUpSound;
        //}

        public void ShootSound()
        {
            _shootAudioSource.PlayOneShot(GetRandomAudioClip(_shootingSounds), _shootAudioSource.volume);
        }
        public void GetPowerUpSound(string powerUpType)
        {
            AudioClip audioClip = _powerUpSounds.audioClips.Find(x => x.name == powerUpType);

            _powerUpAudioSource.PlayOneShot(audioClip, 0.7F);
        }

        public void PlayPowerUpSound(PowerUpControl powerUpControl)
        {
            GetPowerUpSound(powerUpControl._powerUpType.ToString());
        }
        public void PlayCreditsMusic()
        {
            _creditsAudioSource.pitch = (Random.Range(0.6f, 0.9f));
            _creditsAudioSource.PlayOneShot(GetRandomAudioClip(_creditsSounds), _creditsAudioSource.volume);
        }

        public void PlayHitSound()
        {
            _hitAudioSource.pitch = (Random.Range(0.6f, 0.9f));
            _hitAudioSource.PlayOneShot(GetRandomAudioClip(_hitSounds), _hitAudioSource.volume);
        }

        public void PlayerDeathSound()
        {
            StopAllAudioSourceBut(_playerDeathAudioSource);

            foreach (var elem in  _playerDeathSounds.audioClips)
            {
                _playerDeathAudioSource.PlayOneShot(elem, _playerDeathAudioSource.volume);
            }
        }

        public void StopAllAudioSourceBut(AudioSource? audioSource = null)
        {
            var list = gameObject.GetComponentsInChildren<AudioSource>();
            foreach (var elem in list)
            {
                if(elem  != audioSource)
                {
                    elem.gameObject.SetActive(false);
                }
            }
        }

        private AudioClip GetRandomAudioClip(SoundClipsCollection clipsCollection)
        {
            var randomIndex = Random.Range(0, (clipsCollection.audioClips.Count - 1));
            var randomValue = clipsCollection.audioClips[randomIndex];
            return randomValue;
        }

    }
}
