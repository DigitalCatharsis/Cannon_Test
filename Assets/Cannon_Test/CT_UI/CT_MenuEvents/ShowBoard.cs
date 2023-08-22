using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ShowBoard : MonoBehaviour
    {
        [Inject] private SoundManager _soundManager;

        [SerializeField] private GameObject _boardToDisable;
        [SerializeField] private GameObject _boardToOpen;
        [SerializeField] private AudioSourceType _audioSourceType;
        [SerializeField] private bool _makeItPitched = false;

        public void ShowBoardFunc()
        {            
            _soundManager.DisableAllAudioSourceExceptThisOne(_audioSourceType);
            _boardToOpen.gameObject.SetActive(true);
            if (_audioSourceType != null)
            {
                _soundManager.PlaySound(_audioSourceType, _makeItPitched);
            }
            _boardToDisable.gameObject.SetActive(false);
        }
    }
}

