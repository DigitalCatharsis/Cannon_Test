using Cannon_Test;
using System.Collections;
using System.Threading;
using UnityEngine;
using Zenject;

public class PlayerControl : MonoBehaviour
{
    [Inject] private PoolManager _poolManager;
    [Inject] private LevelLogic _levelLogic;
    [Inject] private SoundManager _soundManager;

    [Header("MousePosition")]
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;

    [Header("Shooting")]
    public Transform _cannonBallSpawnPoint;
    public ProjectileType cannonBallType;
    public float cannonBallSpeed = 5000;
    [SerializeField] private int _cannonBallAttackDamage = 1;

    [SerializeField] private float _shootDelay = 0.1f;
    private float _timer;

    public int CannonBallAttackDamage { get => _cannonBallAttackDamage + _levelLogic.AttackDamageBonus; private set => _cannonBallAttackDamage = value; }

    //я не стал писать InputManager дл€ контроллеров, потому что сомневаюсь, что игра будет расшир€тьс€, ну как минимум в “« об этом не говорилось.
    //Ќо если бы пришлось, думаю € бы сделал KeyboadInput.cs, VirtualInputManager, и уже с VirtualInputManager брал бы значени€ ака IsShooting и тд...

    private void Update()
    {
        _timer += Time.deltaTime;
        if (!_levelLogic.IsOnMenu && !_levelLogic.isGameOver)
        {
            RotatePlayerActor();
            Shoot();
        }
    }
    private void Shoot()
    {
        if (!_levelLogic.heaveMachineGun)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
        }
        else
        {
            if (_timer >= _shootDelay)
            {
                _timer = 0;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    for (var i = 0; i < 3; i++)
                    {
                        Attack();
                    }                    
                }
            }
        }
    }
    private void Attack()
    {
        if (_levelLogic.heaveMachineGun)
        {
            _soundManager.PlayCustomSound(AudioSourceType.SHOOT, 13);
        }
        else
        {
            _soundManager.PlayRandomSound(AudioSourceType.SHOOT, false);
        }
        //почему-то первый летит быстрее остальных???? WTF?
        var cannonBall = _poolManager.GetObject(cannonBallType, _cannonBallSpawnPoint.transform.position, Quaternion.Euler(32, 90, -15));
        cannonBall.SetActive(true);
        cannonBall.GetComponent<Rigidbody>().velocity = _cannonBallSpawnPoint.transform.forward * CannonBallAttackDamage * cannonBallSpeed * Time.deltaTime;
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

