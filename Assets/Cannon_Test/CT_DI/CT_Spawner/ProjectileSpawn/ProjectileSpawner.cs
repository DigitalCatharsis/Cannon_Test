using UnityEngine;
using Zenject;

namespace Cannon_Test
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [Inject]
        private ICoreFactory<ProjectileType> _projectyle;

        //courutine
        //uniTask?
        //Красивый таймер в апдейтах, кек

        private void Update()
        {
            //_projectyle.CreatePrefab(GetProjectileType(), GetRandomPosition());
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
        private ProjectileType GetProjectileType()
        {
            return (ProjectileType)Random.Range(0, System.Enum.GetValues(typeof(ProjectileType)).Length);
        }
    }
}
