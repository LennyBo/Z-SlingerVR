using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class pathfinding : MonoBehaviour
{
    // https://www.red-gate.com/simple-talk/development/dotnet-development/pathfinding-unity-c/
    public Transform[] points;
    private Transform zombie;
    private NavMeshAgent nav;
    private int destPoint;

    static private float deathTime = 10f;
    
    private Animator m_animator;
    private bool finisedWalking = false;

    public GameObject camGO;

    private Camera cam;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        cam = camGO.transform.GetChild(0).GetComponent<Camera>();
        nav = GetComponent<NavMeshAgent>();
        zombie = transform.GetChild(0);
        m_animator = zombie.GetComponent<Animator>();
        target = transform.position;
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
                nav.SetDestination(hit.point);
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
    public void stop()
    {
        nav.isStopped = true;
        Debug.Log("STOP");
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
        {
            return;
        }

        Vector3 velocity = nav.velocity;
        if (velocity.z != 0)
        {
            m_animator.SetBool("Walking", true);
        }
        else
        {
            m_animator.SetBool("Walking", false);
        }
        if (!nav.pathPending && nav.remainingDistance < 0.5f)
  	        GoToNextPoint();
    }
    
    void GoToNextPoint()
    {
        if (points.Length == 0)
            return;
        
        if (points.Length == destPoint)
        {
            nav.isStopped = true;
            //m_animator.SetFloat("Direction", 0);
            //m_animator.SetFloat("Speed", 0);
            m_animator.SetTrigger("Death");
            finisedWalking = true;
            Destroy(gameObject, deathTime);
            return;
        }
        
        nav.destination = points[destPoint].position;
        destPoint += 1;
        
    }
}
