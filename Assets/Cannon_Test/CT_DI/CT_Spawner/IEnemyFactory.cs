using UnityEngine;

public interface IEnemyFactory
{
    public void Create(EnemyType enemyType, Vector3 position);
}
