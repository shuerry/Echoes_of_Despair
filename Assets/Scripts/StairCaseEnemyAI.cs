using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCaseEnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMStates currentState;
    public float attackDistance = 5;
    public float chaseDistance = 10;
    public float enemySpeed = 5;
    public GameObject player;
    public int attackDamage = 5;
    public float attackCooldown = .3f;
    //public AudioClip attackSFX; // sound effect for enemy attack
    public GameObject deadVFX;

    Animator anim;
    float distanceToPlayer;

    EnemyHealth enemyHealth;
    Transform deadTransform;
    Vector3 nextDesitination;
    int health;
    float elapsedTime = 0;
    bool isDead;
    public GameObject enemySpawner;
    public GameObject enemyPrefab;
    void Start()
    {
        Initialize();
    }

    void Initialize() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        anim = GetComponent<Animator>();

        enemyHealth = GetComponent<EnemyHealth>();
        health = enemyHealth.currentHealth;

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        health = enemyHealth.currentHealth;
        if (health <= 0 && currentState != FSMStates.Dead)
        {
            currentState = FSMStates.Dead;
            return; 
        }
        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
            }
            elapsedTime += Time.deltaTime;
        
       /* if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }*/

    }

    void UpdatePatrolState()
    {
        print("Patrolling!");
        anim.SetInteger("animState", 1);
        if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(player.transform.position);        
        /* transform.position = Vector3.MoveTowards
             (transform.position, nextDestination, enemySpeed*Time.deltaTime);
        */
    }

    void UpdateChaseState()
    {
        print("Chasing!");
        anim.SetInteger("animState", 2);
        nextDesitination = player.transform.position;
        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDesitination);
        /*
        transform.position = Vector3.MoveTowards
            (transform.position, nextDestination, enemySpeed * Time.deltaTime);
        */
    }
    void UpdateAttackState()
    {
        print("attack");
        nextDesitination = player.transform.position;
        if (distanceToPlayer <= attackDistance)
        {
            if (elapsedTime >= attackCooldown)
            {
                elapsedTime = 0f;
                currentState = FSMStates.Attack;
              
                anim.SetInteger("animState", 3);
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                // [lay attack sound effect
                // AudioSource.PlayClipAtPoint(attackSFX, transform.position);
            }
            FaceTarget(nextDesitination);
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
            FaceTarget(nextDesitination);
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
            FaceTarget(nextDesitination);
        }
     
    }


        void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);
        isDead = true;
        deadTransform = gameObject.transform;
        Destroy(gameObject, 3);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void OnDestroy()
    {
       Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

    }

}
