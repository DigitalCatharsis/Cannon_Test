using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cannon_Test
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private  string nextScene;

        public void ChangeSceneTo()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}

