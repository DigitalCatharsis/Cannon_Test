using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class DeathAnimationManager : MonoBehaviour
    {
        [Inject] private DeathAnimationLoader _deathAnimationLoader;
        private List<RuntimeAnimatorController> _Candidates = new List<RuntimeAnimatorController>();

        public RuntimeAnimatorController GetDefaultAnimator()
        {
            return _deathAnimationLoader.defaultAnimatorController.animator;
        }

        public RuntimeAnimatorController GetAnimator()
        {
            _Candidates.Clear();

            foreach (DeathAnimationData data in _deathAnimationLoader.deathAnimationDataList)
            {                
                _Candidates.Add(data.animator);
            }
            return _Candidates[Random.Range(0, _Candidates.Count)];
        }
    }
}