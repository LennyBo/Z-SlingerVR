using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public int time = 4;




    private float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        float dmg = collision.relativeVelocity.magnitude;
        Debug.Log("hey! I hit for " + dmg + " damages (magnitude)");
    }
}