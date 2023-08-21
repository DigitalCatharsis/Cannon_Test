using UnityEngine;

namespace Cannon_Test
{
    public class ButtonScale : MonoBehaviour
    {
        private GameEventListener _listener;

        private void Awake()
        {
            _listener = gameObject.GetComponent<GameEventListener>();
        }
        public void ScaleUpButton()
        {
            _listener.gameEvent.eventObj.GetComponent<Animator>().SetBool(UIPrameters.ScaleUp.ToString(), true);
        }

        public void ResetButtonScale()
        {
            _listener.gameEvent.eventObj.GetComponent<Animator>().SetBool(UIPrameters.ScaleUp.ToString(), false);
        }
    }
}

