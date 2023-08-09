using Cannon_Test;
using UnityEngine;
using Zenject;

public class PlayerControl : MonoBehaviour
{
    [Inject] private PoolManager _poolManager;

    [Header("MousePosition")]
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;

    [Header("Shooting")]
    [SerializeField] private Transform _cannonBallSpawnPoint;
    public ProjectileType cannonBallType;
    public float cannonBallSpeed;
    public float cannonBallAttackDamage;

    //я не стал писать InputManager дл€ контроллеров, потому что сомневаюсь, что игра будет расшир€тьс€, ну как минимум в “« об этом не говорилось.
    //Ќо если бы пришлось, думаю € бы сделал KeyboadInput.cs, VirtualInputManager, и уже с VirtualInputManager брал бы значени€ ака IsShooting и тд...

    private void Awake()
    {
    }

    private void Update()
    {
        RotatePlayerActor();

        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //почему-то первый летит быстрее остальных???? WTF?
            var cannonBall = _poolManager.GetObject(cannonBallType, _cannonBallSpawnPoint.transform.position, Quaternion.Euler(32,90,-15));
            cannonBall.GetComponent<Rigidbody>().velocity = _cannonBallSpawnPoint.transform.forward * cannonBallSpeed * Time.deltaTime;
            //Debug.Log($"{_cannonBallSpawnPoint.transform.forward} {cannonBallSpeed}");
        }
    }

    private void RotatePlayerActor()
    {
        this.transform.root.LookAt(GetPointerLocation());
    }
    private Vector3 GetPointerLocation()
    {
        _screenPosition = Input.mousePosition;
        _screenPosition.z = Camera.main.nearClipPlane + 3;

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        return _worldPosition;
    }
}

