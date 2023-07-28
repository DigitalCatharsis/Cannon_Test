using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon_Test
{
    public enum UIPrameters
    {
        ScaleUp, 
    }

    public class ButtonScale : MonoBehaviour
    {
        private GameEventListener listener;

        private void Awake()
        {
            listener = gameObject.GetComponent<GameEventListener>();
        }
        public void ScaleUpButton()
        {
            listener.gameEvent.eventObj.GetComponent<Animator>().SetBool(UIPrameters.ScaleUp.ToString(), true);
        }

        public void ResetButtonScale()
        {
            listener.gameEvent.eventObj.GetComponent<Animator>().SetBool(UIPrameters.ScaleUp.ToString(), false);
        }
    }
}

