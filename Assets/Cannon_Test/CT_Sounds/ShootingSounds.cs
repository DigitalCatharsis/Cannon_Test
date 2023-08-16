using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon_Test
{
    [CreateAssetMenu(fileName = "New state", menuName = "Cannon_Test/Audio/ShootingSounds" +
        "")]
    public class ShootingSounds : ScriptableObject
    {
        public List<AudioClip> audioClips = new List<AudioClip>();
    }
}