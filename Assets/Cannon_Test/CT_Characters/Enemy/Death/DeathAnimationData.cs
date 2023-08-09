using UnityEngine;

namespace Cannon_Test
{
    [CreateAssetMenu(fileName ="New ScriptableObject", menuName = "Cannon_Test/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        public RuntimeAnimatorController animator;
    }
}
