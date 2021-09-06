using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public int time = 4;
    public string ennemyTag;
    private Collider playerCollider;
    private bool didHitSomething = false;


    private float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = FindObjectOfType<CharacterController>();
        Physics.IgnoreCollision(GetComponent<Collider>(), playerCollider);
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (didHitSomething || collision.gameObject.tag != ennemyTag)
            return;

        didHitSomething = true;
        GetComponents<AudioSource>()[0].Play();
    }
}
