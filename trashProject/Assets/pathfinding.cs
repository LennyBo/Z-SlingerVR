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

    // because I can't GetComponent<GameObject>(); // (return null);
    public GameObject gameObject;
    static private float deathTime = 5f;
    
    private Animator m_animator;
    private bool finisedWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        zombie = transform.GetChild(0);
        m_animator = zombie.GetComponent<Animator>();
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
