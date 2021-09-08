using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class zombie_pathfinding_and_collision : MonoBehaviour
{
    // https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/


    private Transform[] points;
    [SerializeField] private float lp;
    [SerializeField] private int zombieDamages;
    [SerializeField] private float attackCooldown;
    private float attackCooldownTimer;
    
    private Transform currentObjective = null;
    private Transform zombie;
    private Vector3 oldPos;
    private NavMeshAgent navAgent;
    private int destPoint;

    [SerializeField] private float timeBeforeDespawn = 10f;
    
    private Animator m_animator;
    private bool finisedWalking = false;

    private static PhaseControllerScript phaseController = null;
    
    private bool isFirstUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
        if (phaseController == null)
            phaseController = FindObjectOfType<PhaseControllerScript>();

        points = new Transform[1];
        points[0] = GameObject.FindGameObjectWithTag("heart").transform;

        navAgent = GetComponent<NavMeshAgent>();
        zombie = transform.GetChild(0);
        m_animator = zombie.GetComponent<Animator>();
        oldPos = zombie.position;

        attackCooldownTimer = attackCooldown;
    }

    /*
    Next, go to the Update function. Before entering anything in this function, rename it to FixedUpdate.
    The difference between Update and FixedUpdate is that Update will run once every frame, whereas FixedUpdate is
    capable of running zero, one, or more times per frame.
    If, for example, your game is running at thirty frames per second, your FixedUpdate can make sure physics
    calculations are consistent and in synch with the global physics timestep of your game. Thus, it is recommended
    that any physics updates such as movement should be placed within the FixedUpdate. With that in mind, add the following
    code to the FixedUpdate function
    */

    private void FixedUpdate()
    {
        if (finisedWalking)
            return;



        oldPos = zombie.position;

        Vector3 velocity = navAgent.velocity;
        if (velocity.magnitude != 0) {
            m_animator.SetBool("Walking", true);
        }
        else {
            m_animator.SetBool("Walking", false);
        }

        /*
        if (isStuck() && !isGoalReachable() && isTargetNearestEndOfPathStructure()) {
            
        }
        */

        //Debug.Log("remaining distance is " + navAgent.remainingDistance);
        
        if (!navAgent.pathPending && navAgent.remainingDistance < 1f)
            if (isFirstUpdate)
            {
                // delay first movement
                GoToNextPoint();
                //StartCoroutine(GoToNextPointFirstTime());
            } else
                GoToNextPoint();
    }

    private bool isStuck()
    {
        return false;
       // return navAgent.velocity.magnitude < 0.3f;
    }

    private bool isGoalReachable()
    {
        if (currentObjective == null)
            return false;
        
        NavMeshPath path = new NavMeshPath();
        navAgent.CalculatePath(currentObjective.position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
    
    private bool isTargetNearestEndOfPathStructure()
    {
        GameObject[] structs = GameObject.FindGameObjectsWithTag("destructible");
        if (structs.Length == 0) {
            return false;
        }

        return true;
    }

    private void GoToNextPoint()
    {   
        if (points.Length == 0)
            return;
        
        if (points.Length == destPoint)
        {
            IArrived();
            return;
        }
        
        currentObjective = points[destPoint++];
        navAgent.destination = currentObjective.position;
    }

    private IEnumerator GoToNextPointFirstTime()
    {
        Debug.Log("I DONT KNOW IF THIS WORKS");
        isFirstUpdate = false;
        float t = Random.Range(0, 3);
        yield return new WaitForSecondsRealtime(t);
        GoToNextPoint();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float dmg = collision.relativeVelocity.magnitude;
        //Debug.Log("hey! you hit me for " + dmg + " damages (magnitude)");
        
        if ((lp -= dmg) <= 0)
        {
            DieYouFool();
        }
    }

    private void IArrived()
    {
        HitHeart();
        //DieYouFool();
    }

    private void DieYouFool()
    {
        finisedWalking = true;
        GetComponent<CapsuleCollider>().enabled = false;
        m_animator.SetTrigger("Death");
        GetComponent<AudioSource>().Stop();
        navAgent.isStopped = true;
        //navAgent.Stop();
        //Debug.Log("GET DOWN");
        zombie.transform.position += new Vector3(0, -0.2f, 0);
        //Quaternion target = Quaternion.Euler(-10f, 0, 0);
        //zombie.transform.localRotation = Quaternion.Slerp(zombie.transform.localRotation, target, 1);
        Destroy(gameObject, timeBeforeDespawn);
    }

    private void HitHeart()
    {
        if (attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
            return;
        }
        attackCooldownTimer = 0;
        //Debug.Log("ATTACK");
        m_animator.SetTrigger("Attack");
        phaseController.hit(zombieDamages);
    }
    
    // https://www.youtube.com/watch?v=FkLJ45Pt-mY
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                m_animator.SetBool("Walking", true);
                navAgent.SetDestination(hit.point);
                target = hit.point;
            }
        }

        CheckIfDestinationReached();
    }


    private bool CheckIfDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if(distanceToTarget < 0.5)
        {
            m_animator.SetBool("Walking", false);
            return true;
        }
        return false;
    }
    */
}
