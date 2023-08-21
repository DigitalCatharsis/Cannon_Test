using Cannon_Test;
using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ShowBoard : MonoBehaviour
    {
        public class ButtonScale : MonoBehaviour
        {
            [Inject] private SoundManager _soundManager;

            [SerializeField] private GameObject _mainMenu;
            [SerializeField] private GameObject _boardToOpen;
            [SerializeField] private AudioSource? _audioSource;

            public void ShowBoardFunc()
            {
                _mainMenu.gameObject.SetActive(false);
                _soundManager.StopAllAudioSourceBut();
                _boardToOpen.gameObject.SetActive(true);
                if (_audioSource != null ) 
                {
                    _soundManager.play
                }
                
            }
        }
    }
}

