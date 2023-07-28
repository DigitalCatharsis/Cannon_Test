using Cannon_Test;
using System.Collections;
using UnityEngine;

namespace Cannon_Test
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType;
        [SerializeField] private float _scheduledOffTime;
        private Coroutine _offRoutine;

        private void OnEnable()      

        {
            if (_offRoutine != null)
            {
                StopCoroutine(_offRoutine);
            }

            if (_scheduledOffTime > 0f)
            {
                _offRoutine = StartCoroutine(_ScheduledOff());
            }
        }

        public void TurnOff()
        {
            this.transform.parent = null;
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;

            PoolManager.Instance.AddObject(this);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(_scheduledOffTime);
            if (!PoolManager.Instance.poolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }

    }



}