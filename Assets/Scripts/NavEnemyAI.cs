using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavEnemyAI : MonoBehaviour
{

    public enum FSMStates
    {
        //list of values that we can select 
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public float attackDistance = 5;
    public FSMStates currentState;
    public float enemySpeed = 5;
    public float chaseDistance = 10;
    public GameObject player;
  
 
    public GameObject deadVFX;
    public Transform enemyEyes;
    public float fieldOfView = 45f;






    GameObject[] wanderPoints;
    Animator anim;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    float distanceToPlayer;
    float elapsedTime;
    NavEnemyHealth enemyHealth;
    int health;
    Transform deadTransform;
    bool isDead;
    UnityEngine.AI.NavMeshAgent agent;




    // Start is called before the first frame update
    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoints");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //wandTip = GameObject.FindGameObjectWithTag("WandTip");
        enemyHealth = GetComponent<NavEnemyHealth>();
        health = enemyHealth.currentHealth;
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //enemy can use the nav mesh system


        Initalize();

    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        health = enemyHealth.currentHealth;

        //in update 
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
        if (health < 0)
        {
            currentState = FSMStates.Dead;
        }


    }

    private void Initalize()
    {
        currentState = FSMStates.Patrol;
        FindNextPoint();


    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    void UpdatePatrolState()
    {

        print("Patrolling");
        anim.SetInteger("animState", 1); //indicate that the walking animation should be played
        agent.stoppingDistance = 0;
        agent.speed = 3.5f;
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);


    }

    void UpdateChaseState()
    {
        print("Chasing");
        anim.SetInteger("animState", 2); //indicate that the walking animation should be played
        agent.stoppingDistance = attackDistance;
        nextDestination = player.transform.position;
        agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        print("Attack");

        nextDestination = player.transform.position;

        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);
        anim.SetInteger("animState", 3); //indicate that the walking animation should be played
      //  EnemySpellCast();

    }

    void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);
        isDead = true;
        deadTransform = gameObject.transform;
        Destroy(gameObject, 3);

    }

    private void OnDestroy()
    {
        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);

    }



    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
        agent.SetDestination(nextDestination);


    }

   

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);  //can see this during the gameplay
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * .5f, 0) * frontRayPoint; //enemy?s forward direction
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * .5f, 0) * frontRayPoint;
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);

    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;

        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;
        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            //player is within the field of view 
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in sight!");
                    return true;

                }
                return false;
            }
            return false;


        }

        return false;

    }
    }