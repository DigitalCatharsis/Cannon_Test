using UnityEngine;

namespace Cannon_Test
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        public EnemyType enemyType;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}

