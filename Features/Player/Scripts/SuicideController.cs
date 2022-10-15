using System;
using Features.Cam;
using Features.Enemy.Scripts.Abstract;
using Features.Enemy.Scripts.Waves;
using Features.Global.LevelsSwitching.Scripts;
using Features.Player.Scripts;
using Features.Sword.Scripts;
using UnityEngine;

public class SuicideController : MonoBehaviour
{
    [SerializeField] private SwordMoving swordMoving;
    [SerializeField] private SwordAttack swordAttack;
    [SerializeField] private ParticleSystem moveParticles;
    
    private CapsuleCollider2D capsuleCollider;
    private PlayerMovement playerMovement;
    private Animator animator;

    private CameraMovement _camera;
    
    private static bool isAllowed;
    private static bool isSuicided;

    private bool _isAvailable = true;

    public static bool IsAllowed => isAllowed;
    public static bool IsSuicided => isSuicided;

    public void Inject(CameraMovement cam)
    {
        _camera = cam;
    }
    
    private void Awake()
    {
        isAllowed = false;
        isSuicided = false;
        
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }


    private void OnEnable()
    {
        Health.Died += OnDeath;
    }
    

    private void OnDeath()
    {
        _isAvailable = false;
    }

    private void OnDisable()
    {
        Health.Died -= OnDeath;
        
        isAllowed = false;
        isSuicided = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isAllowed == true && SuicideTip.IsNear == true && _isAvailable == true)
            Suicide();
    }

    public void AllowSuicide()
    {
        isAllowed = true;
    }

    private void Suicide()
    {
        moveParticles.Stop();
        _camera.ToSuicide();
        
        isAllowed = false;
        isSuicided = true;

        var enemies = FindObjectsOfType<EnemyEntity>();

        foreach (var enemy in enemies)
        {
            var health = enemy.GetComponent<Health>();
            health.TakeDamage(10000);
        }

        swordMoving.enabled = false;
        swordAttack.enabled = false;
        capsuleCollider.enabled = false;
        animator.SetTrigger("Suicide");
    }

    public void NextWave()
    {
        WavesProcessor._waveNumber++;
        
        if (WavesProcessor._waveNumber < 9)
            Transitions.LoadGame();
        else
            Transitions.LoadEscape();
    }
}
