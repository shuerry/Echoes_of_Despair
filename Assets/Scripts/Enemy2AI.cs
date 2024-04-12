using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2AI : MonoBehaviour
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

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;
    float distanceToPlayer;

    int currentDestinationIndex = 0;
    NavEnemyHealth enemyHealth;
    Transform deadTransform;
    int health;
    float elapsedTime = 0;
    bool isDead;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoints2");
        anim = GetComponent<Animator>();


        enemyHealth = GetComponent<NavEnemyHealth>();
        health = enemyHealth.currentHealth;

        isDead = false;
        Initialize();
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


    private void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();



    }
    void UpdatePatrolState()
    {
        print("Patrolling!");
        anim.SetInteger("animState", 1);
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        /* transform.position = Vector3.MoveTowards
             (transform.position, nextDestination, enemySpeed*Time.deltaTime);
        */
    }

    void UpdateChaseState()
    {
        print("Chasing!");
        anim.SetInteger("animState", 2);
        nextDestination = player.transform.position;
        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        /*
        transform.position = Vector3.MoveTowards
            (transform.position, nextDestination, enemySpeed * Time.deltaTime);
        */
    }
    void UpdateAttackState()
    {
        print("attack");
        nextDestination = player.transform.position;
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
            FaceTarget(nextDestination);
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
            FaceTarget(nextDestination);
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
            FaceTarget(nextDestination);
        }

    }


    void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);
        isDead = true;
        deadTransform = gameObject.transform;
        Destroy(gameObject, 3);
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

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
