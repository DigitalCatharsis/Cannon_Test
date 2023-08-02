using UnityEngine;

namespace Cannon_Test
{
    public class PlayerSpawnMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}

