using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    // Enemy Fields
    public LayerMask GroundLayer, PlayerLayer;
    public NavMeshAgent Agent;
    public Transform Player;
    [SerializeField] private HealthHandler EnemyHealthHandler;
    public Animator AnimatorRef;
    public bool CanChase = true;
    private WaveSystem _waveSystem;


    // Attacking
    public float AttackCoolDownTime;
    public bool AlreadyAttacked;
    public bool HitSuccess;

    // States
    public float AttackRange;
    private bool PlayerInSightRange, PlayerInAttackRange;
    private float _playerDistance;

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioClip _breathing;
    [SerializeField] AudioClip _gargle;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        _waveSystem = WaveSystem.Instance;
        Player = _waveSystem.Player.transform;
        EnemyHealthHandler.OnDeathOccured += EnemyHealthHandler_OnDeathOccured;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(_breathing, 0.5f);
    }

    private void Update()
    {
        RunEnemy();
    }


    public void RunEnemy()
    {
        //Check for sight and attack range
        _playerDistance = Vector3.Distance(Player.transform.position, transform.position);

        PlayerInSightRange = true;
        PlayerInAttackRange = CheckRange(AttackRange);


        ToggleHPBarState();

        if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        if (PlayerInAttackRange && PlayerInSightRange) AttackPlayer(false);

        AnimationHandler();

        if (HitSuccess)
        {
            HitSuccess = false;
            Invoke("RegisterIndicator", 0);
        }
        
        EnemyHealthHandler.CanBeInstaKilled = _waveSystem.CanBeInstaKilled;
    }

    private bool CheckRange(float rangeToCheck)
    {
        if (_playerDistance <= rangeToCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void ToggleHPBarState()
    {
        if (PlayerInSightRange)
        {
            EnemyHealthHandler.ToggleHealthBar(true);
        }
        else
        {
            EnemyHealthHandler.ToggleHealthBar(false);
        }
    }

    private bool CanSeePlayer()
    {
        bool CanSee = false;
        if (Physics.Raycast(transform.position, transform.forward, AttackRange, PlayerLayer))
        {
            CanSee = true;
        }
        return CanSee;
    }

    virtual public void ChasePlayer()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (CanChase)
        {
            Agent.SetDestination(Player.position);
        }
    }

    virtual public void AttackPlayer(bool changeAttack)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        //Make sure enemy doesn't move
        Agent.SetDestination(transform.position);

        //transform.LookAt(Player);
        var lookPos = Player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Agent.angularSpeed);

        if (!changeAttack)
        {
            if (!AlreadyAttacked)
            {
                //Attack code here
                AttackPatternController();
                audioSource2.PlayOneShot(_gargle, 1);
                //Debug.Log("Attacked");
                //End of attack code

                AlreadyAttacked = true;
                StartCoroutine(ResetAttack(AttackCoolDownTime, delegate () { AlreadyAttacked = false; }));
            }
        }
    }

    public delegate void Callback();

    public IEnumerator ResetAttack(float attackCoolDown, Callback boolToReset)
    {
        //Debug.Log("Reseting");
        yield return new WaitForSeconds(attackCoolDown);
        boolToReset();
        //Debug.Log("Done");
    }

    virtual public void AttackPatternController()
    {

    }

    virtual public void AnimationHandler()
    {

    }

    // Registering To Player Indicator
    private void RegisterIndicator()
    {
        //if (!DamageIndicatorSystem.CheckIfObjInSight(transform))
        //{
        DamageIndicatorSystem.CreateIndicator(transform);
        //}
    }

    private void EnemyHealthHandler_OnDeathOccured(object sender, System.EventArgs e)
    {
        EnemyDeath();
    }

    public virtual void EnemyDeath()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
