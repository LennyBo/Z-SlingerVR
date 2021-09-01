using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class zombie_pathfinding_and_collision : MonoBehaviour
{
    // https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/


    [SerializeField] private Transform[] points;
    [SerializeField] private float pv;
    
    private Transform currentObjective = null;
    private Transform zombie;
    private Vector3 oldPos;
    private NavMeshAgent navAgent;
    private int destPoint;

    static private float timeBeforeDespawn = 10f;
    
    private Animator m_animator;
    private bool finisedWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        zombie = transform.GetChild(0);
        m_animator = zombie.GetComponent<Animator>();
        oldPos = zombie.position;
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
    void FixedUpdate()
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

        if (isStuck() && !isGoalReachable() && isTargetNearestEndOfPathStructure()) {
            
        }

        //Debug.Log("remaining distance is " + navAgent.remainingDistance);
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    bool isStuck()
    {
        return false;
        return navAgent.velocity.magnitude < 0.3f;
    }

    bool isGoalReachable()
    {
        if (currentObjective == null)
            return false;
        
        NavMeshPath path = new NavMeshPath();
        navAgent.CalculatePath(currentObjective.position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
    
    bool isTargetNearestEndOfPathStructure()
    {
        GameObject[] structs = GameObject.FindGameObjectsWithTag("destructible");
        if (structs.Length == 0) {
            return false;
        }

        return true;
    }

    void GoToNextPoint()
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

    private void OnCollisionEnter(Collision collision)
    {
        float dmg = collision.relativeVelocity.magnitude;
        //Debug.Log("hey! you hit me for " + dmg + " damages (magnitude)");
        
        if ((pv -= dmg) <= 0)
        {
            DieYouFool();
        }
    }

    private void IArrived()
    {
        DieYouFool();
    }

    private void DieYouFool()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        m_animator.SetTrigger("Death");
        navAgent.Stop();
        Debug.Log("GET DOWN");
        zombie.transform.position += new Vector3(0, -0.2f, 0);
        //Quaternion target = Quaternion.Euler(-10f, 0, 0);
        //zombie.transform.localRotation = Quaternion.Slerp(zombie.transform.localRotation, target, 1);
        //Destroy(gameObject, timeBeforeDespawn);
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
