using UnityEngine.Pool;
using UnityEngine;

namespace Cannon_Test
{

    public enum PoolObjectType
    {
        ZOMBIE_GENERAL,
        ZOMBIE_ELITE,
        POWERUP,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;

            switch (objType)
            {
                case PoolObjectType.ZOMBIE_GENERAL:
                    {
                        obj = Instantiate(Resources.Load("ZombieElite", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.ZOMBIE_ELITE:
                    {
                        obj = Instantiate(Resources.Load("ZombieGeneral", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.POWERUP:
                    {
                        //obj = Instantiate(Resources.Load("", typeof(GameObject)) as GameObject);
                        break;
                    }
            }

            return obj.GetComponent<PoolObject>();
        }




    }
}