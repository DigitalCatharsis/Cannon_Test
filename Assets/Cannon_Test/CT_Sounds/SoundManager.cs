using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{

    public enum AudioSourceType
    {
        SHOOT,
        LEVEL_MUSIC,
        POWERUP,
        HIT,
        PLAYER_DEATH,
        CREDITS,
        LEADERBOARD,
    }

    public class SoundManager : MonoBehaviour
    {

        [Header("Sound Collections")]
        [SerializeField] private SoundClipsCollection _shootingSounds;
        [SerializeField] private SoundClipsCollection _levelMusicSounds;
        [SerializeField] private SoundClipsCollection _powerUpSounds;
        [SerializeField] private SoundClipsCollection _hitSounds;
        [SerializeField] private SoundClipsCollection _playerDeathSounds;
        [SerializeField] private SoundClipsCollection _creditsSounds;
        [SerializeField] private SoundClipsCollection _leaderboardAudioSounds;

        [Header("AudioSources")]
        [SerializeField] private AudioSource _levelMusicAudioSource;
        [SerializeField] private AudioSource _shootAudioSource;
        [SerializeField] private AudioSource _powerUpAudioSource;
        [SerializeField] private AudioSource _hitAudioSource;
        [SerializeField] private AudioSource _playerDeathAudioSource;
        [SerializeField] private AudioSource _creditsAudioSource;
        [SerializeField] private AudioSource _leaderboardAudioSource;
        public void GetAndPlayPowerUpSound(string powerUpType)
        {
            AudioClip audioClip = _powerUpSounds.audioClips.Find(x => x.name == powerUpType);
            _powerUpAudioSource.PlayOneShot(audioClip, 0.7F);
        }
        public void PlayRandomSound(AudioSourceType type, bool randomPitch = false)
        {
            var audioSource = new AudioSource();
            var audioClips = ScriptableObject.CreateInstance<SoundClipsCollection>();

            switch (type)
            {
                case AudioSourceType.SHOOT:
                    {
                        audioSource = _shootAudioSource;
                        audioClips = _shootingSounds;
                        if (randomPitch)
                        {
                            audioSource.pitch = (Random.Range(0.6f, 0.9f));
                        }
                        audioSource.PlayOneShot(GetRandomAudioClip(audioClips), audioSource.volume);
                        return;
                    }
                case AudioSourceType.LEVEL_MUSIC:
                    {
                        audioClips = _levelMusicSounds;
                        audioSource = _levelMusicAudioSource;
                        audioSource.PlayOneShot(GetRandomAudioClip(audioClips), audioSource.volume);
                        return;
                    }
                case AudioSourceType.HIT:
                    {
                        audioClips = _hitSounds;
                        audioSource = _hitAudioSource;
                        if (randomPitch)
                        {
                            audioSource.pitch = (Random.Range(0.6f, 0.9f));
                        }
                        audioSource.PlayOneShot(GetRandomAudioClip(audioClips), audioSource.volume);
                        return;
                    }

                case AudioSourceType.CREDITS:
                    {
                        audioClips = _creditsSounds;
                        audioSource = _creditsAudioSource;
                        if (randomPitch)
                        {
                            audioSource.pitch = (Random.Range(0.6f, 0.9f));
                        }
                        audioSource.clip = GetRandomAudioClip(audioClips);
                        audioSource.Play();
                        return;
                    }
                case AudioSourceType.LEADERBOARD:
                    {
                        audioClips = _leaderboardAudioSounds;
                        audioSource = _leaderboardAudioSource;
                        audioSource.clip = GetRandomAudioClip(audioClips);
                        audioSource.Play();
                        return;
                    }
            }
        }

        public void PlayCustomSound(AudioSourceType type, int index = 0, bool randomPitch = false, PowerUpType powerUpType = default)
        {
            var audioSource = new AudioSource();
            var audioClips = ScriptableObject.CreateInstance<SoundClipsCollection>();
            switch (type)
            {
                case AudioSourceType.POWERUP:
                    {
                        GetAndPlayPowerUpSound(powerUpType.ToString());
                        return;
                    }
                case AudioSourceType.PLAYER_DEATH:
                    {
                        DisableAllAudioSourceExceptThisOne(AudioSourceType.PLAYER_DEATH);

                        foreach (var elem in _playerDeathSounds.audioClips)
                        {
                            _playerDeathAudioSource.PlayOneShot(elem, _playerDeathAudioSource.volume);
                        }
                        return;
                    }
                case AudioSourceType.LEVEL_MUSIC:
                    {
                        audioClips = _levelMusicSounds;
                        audioSource = _levelMusicAudioSource;
                        if (randomPitch)
                        {
                            audioSource.pitch = (Random.Range(0.6f, 0.9f));
                        }
                        audioSource.clip = GetCusomAudioClip(audioClips, index);
                        audioSource.Play();
                        return;
                    }
                case AudioSourceType.SHOOT:
                    {
                        audioSource = _shootAudioSource;
                        audioClips = _shootingSounds;
                        if (randomPitch)
                        {
                            audioSource.pitch = (Random.Range(0.6f, 0.9f));
                        }
                        audioSource.PlayOneShot(GetCusomAudioClip(audioClips, index), audioSource.volume);
                        return;
                    }
            }
        }
        public void DisableAllAudioSourceExceptThisOne(AudioSourceType audioType)
        {
            var audioSource = new AudioSource();
            switch (audioType)
            {
                case AudioSourceType.SHOOT:
                    audioSource = _shootAudioSource;
                    break;
                case AudioSourceType.LEVEL_MUSIC:
                    audioSource = _levelMusicAudioSource;
                    break;
                case AudioSourceType.POWERUP:
                    audioSource = _powerUpAudioSource;
                    break;
                case AudioSourceType.HIT:
                    audioSource = _hitAudioSource;
                    break;
                case AudioSourceType.PLAYER_DEATH:
                    audioSource = _playerDeathAudioSource;
                    break;
                case AudioSourceType.CREDITS:
                    audioSource = _creditsAudioSource;
                    break;
                case AudioSourceType.LEADERBOARD:
                    audioSource = _leaderboardAudioSource;
                    break;
            }

            audioSource.gameObject.SetActive(true);

            var list = gameObject.GetComponentsInChildren<AudioSource>();
            foreach (var elem in list)
            {
                if (elem != audioSource)
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
        private AudioClip GetCusomAudioClip(SoundClipsCollection clipsCollection, int index)
        {
            var value = clipsCollection.audioClips[index];
            return value;
        }
    }
}
