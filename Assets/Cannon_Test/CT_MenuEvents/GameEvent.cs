using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannon_Test  
{

    public class GameEvent : MonoBehaviour
    {
        public List<GameEventListener> listListeners = new List<GameEventListener>();
        public GameObject eventObj;

        private void Awake()
        {
            listListeners.Clear();
        }

        public void Raise()
        {
            foreach (GameEventListener listener in listListeners)
            {
                listener.OnRaiseEvent();
            }
        }
        public void Raise(GameObject eventObj)
        {
            foreach (GameEventListener listener in listListeners)
            {
                this.eventObj = eventObj;
                listener.OnRaiseEvent();
            }
        }
    }
}