using UnityEngine;

public interface IProjectileFactory
{
    public void Create(ProjectileType enemyType, Vector3 position);
}
