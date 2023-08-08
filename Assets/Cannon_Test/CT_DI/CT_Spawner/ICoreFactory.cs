using UnityEngine;

namespace Cannon_Test
{
    public interface ICoreFactory<in T> where T : System.Enum
    {
        public GameObject AddToPool(T Type, Vector3 position, Quaternion rotation);
    }

}
