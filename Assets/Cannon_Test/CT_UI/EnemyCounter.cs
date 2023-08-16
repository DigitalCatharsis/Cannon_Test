using TMPro;
using UnityEngine;
using Zenject;

namespace Cannon_Test 
{
    public class EnemyCounter : MonoBehaviour
    {
        private TextMeshProUGUI _enemyCounterText;
        [Inject] private LevelLogic _levelLogic;

        private void OnEnable()
        {
            _enemyCounterText = this.gameObject.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _enemyCounterText.SetText("x" + _levelLogic.EnemyCount.ToString());
        }
    }
}
