using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cannon_Test
{
    public enum UIPrameters
    {
        ScaleUp,
    }

    public class GameOver : MonoBehaviour
    {
        [Inject] private SoundManager _soundManager;

        public void OnEnable()  
        {
            ScaleUpText();
            PlayDeathSound();
        }
        public void ScaleUpText()
        {
            GetComponent<Animator>().SetBool(UIPrameters.ScaleUp.ToString(), true);
        }

        public void PlayDeathSound()
        {
            _soundManager.PlayerDeathSound();
        }
    }
}

