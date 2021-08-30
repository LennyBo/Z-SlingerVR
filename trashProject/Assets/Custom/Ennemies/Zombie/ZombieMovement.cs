using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Animator m_animator;
    public Transform m_feet;
    private CharacterController m_controller;
    
    public float groundDistanceCheck = 0.2f;
    public LayerMask groundMask;
    private bool isGrounded;

    public static float gravity = -9.81f;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_feet = GetComponent<Transform>();
        m_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // animations
        float speed = Input.GetAxis("Vertical");
        float direction = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Fire1") != 0)
        {
            Debug.Log("ATTACK!");
            m_animator.SetTrigger("Attack");
        }

        if (speed != 0)
            m_animator.SetFloat("Speed", speed);
        if (direction != 0)
            m_animator.SetFloat("Direction", direction);


        // gravity
        isGrounded = Physics.CheckSphere(m_feet.position, groundDistanceCheck, groundMask);
        
        if (isGrounded && velocity.y < 0)
            velocity.y = -2.0f;
        else
            velocity.y += gravity * Time.deltaTime;
        
        m_controller.Move(velocity * Time.deltaTime);
    }
}
