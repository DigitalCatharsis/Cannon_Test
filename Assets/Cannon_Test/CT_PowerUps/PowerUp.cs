using Cannon_Test;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private void Update()
    {
        this.transform.Rotate(0, 0.5f, 0);
    }

    private void OnMouseDown()
    {
        (GetComponent<PowerUpPoolObject>()).GotKilled();
    }

}
