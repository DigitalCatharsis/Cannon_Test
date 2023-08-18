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

        [Header("AudioSources")]
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _powerUpAudioSource;
        [SerializeField] private AudioSource _hitAudioSource;

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

        public void PlayHitSound()
        {
            _hitAudioSource.pitch = (Random.Range(0.6f, 0.9f));
            _hitAudioSource.PlayOneShot(GetRandomAudioClip(_hitSounds), _hitAudioSource.volume);
        }

        private AudioClip GetRandomAudioClip(SoundClipsCollection clipsCollection)
        {
            var randomIndex = Random.Range(0, (clipsCollection.audioClips.Count - 1));
            var randomValue = clipsCollection.audioClips[randomIndex];
            return randomValue;
        }

    }
}
