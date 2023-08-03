using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Cannon_Test
{
    public class PoolObject: MonoBehaviour
    {
        public Enum poolObjectType;

        private void Awake()
        {
            if (this.gameObject.GetComponent<CharacterControl>() != null)
            {
                poolObjectType = (this.gameObject.GetComponent<CharacterControl>()).enemyType;
            }
            else if (this.gameObject.GetComponent<ProjectileControl>() == null)
            {
                poolObjectType = (this.gameObject.GetComponent<ProjectileControl>()).projectileType;
            }
            else
            {
                throw new Exception("poolObjectType is null");
            }            
        }
    }
}
