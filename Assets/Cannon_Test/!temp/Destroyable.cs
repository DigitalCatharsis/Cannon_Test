using Cannon_Test;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private void OnMouseDown()
    {
        (GetComponent<PoolObject>()).GotKilled();
    }
}