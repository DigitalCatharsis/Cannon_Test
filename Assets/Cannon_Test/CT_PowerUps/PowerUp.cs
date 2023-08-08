using Cannon_Test;
using System.Collections;
using System.Collections.Generic;
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
