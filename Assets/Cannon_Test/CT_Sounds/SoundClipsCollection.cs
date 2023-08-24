using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon_Test
{
    [CreateAssetMenu(fileName = "New state", menuName = "Cannon_Test/Audio/ShootingSounds" +
        "")]
    public class SoundClipsCollection : ScriptableObject
    {
        public List<AudioClip> audioClips;
    }
}