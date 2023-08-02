using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class EnemySpawner : MonoBehaviour 
    {
        [Inject]
        private IEnemyFactory _enemyFactory;

        //courutine
        //uniTask?
        //Красивый таймер в апдейтах, кек

        private void Update()
        {
            _enemyFactory.Create(GetEnemyType(), GetRandomPosition());
        }

        private Vector3 GetRandomPosition()
        {
            var position = new Vector3(GetRandomFloat(-22.0f, 22.0f), 0.50f, GetRandomFloat(-5.0f, 10.0f));
            return position;
        }

        private float GetRandomFloat(float start, float end)
        {
            return Random.Range(start, end);
        }
        private EnemyType GetEnemyType()
        {
            return (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        }
    }
}
