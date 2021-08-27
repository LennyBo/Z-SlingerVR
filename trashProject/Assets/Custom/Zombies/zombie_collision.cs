using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_collision : MonoBehaviour
{
    public GameObject me;
    public float pv = 10;
    public float timeBeforeDespawn = 21f;

    private Animator m_animator;
    
    private void Start()
    {
        m_animator = transform.GetChild(0).GetComponent<Animator>();
        //m_animator.SetTrigger("Death");
        //Debug.Log("I will die in " + timeBeforeDespawn + "seconds");
        //Destroy(me, timeBeforeDespawn);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        float dmg = collision.relativeVelocity.magnitude;
        Debug.Log("hey! you hit me for " + dmg + " damages (magnitude)");
        
        if ((pv -= dmg) <= 0)
        {
            m_animator.SetTrigger("Death");
            Destroy(me, timeBeforeDespawn);
        }
    }
}
