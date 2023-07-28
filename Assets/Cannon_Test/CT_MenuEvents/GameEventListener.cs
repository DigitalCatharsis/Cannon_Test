using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cannon_Test
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        [Space(10)]
        [SerializeField] UnityEvent _response;

        private void Start()
        {
            if (gameEvent != null)
            {
                if (!gameEvent.listListeners.Contains(this))
                {
                    gameEvent.listListeners.Add(this);
                }
            }
        }

        public void OnRaiseEvent()
        {
            _response.Invoke();
        }
    }
}
