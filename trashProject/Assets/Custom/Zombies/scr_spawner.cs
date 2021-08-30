using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spawner : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    [SerializeField] private float width;
    [SerializeField] private float length;

    [SerializeField] public Transform zombie;
    [SerializeField] private bool alreadyStarted;
    // Start is called before the first frame update
    void Start()
    {
        if (alreadyStarted)
            return;
        
        //Debug.Log("I Start");
        alreadyStarted = true;
        Transform loc = GetComponent<Transform>();
        x = loc.position.x;
        y = loc.position.y;
        z = loc.position.z;
        width = GetComponent<Renderer>().bounds.extents.z;
        length = GetComponent<Renderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Object[] spawn(uint spawn_nbr)
    {
        if (!alreadyStarted)
            Start();

        Debug.Log("I have to spawn " + spawn_nbr + " times");

        Object[] ret = new Object[spawn_nbr];
        for (int i = 0; i < spawn_nbr; ++i)
        {
            Vector2 v = Random.insideUnitCircle;
            //Debug.Log("\t" + v + " x, y: " + v.x + " " + v.y);
            v.x *= length;
            v.y *= width;

            Vector3 v2 = new Vector3(v.x + x, y, v.y + z);
            //Debug.Log("\t" + v2 + " x, y, z, witdh, length: " + x + " " + y + " " + z + " " + width + " " + length);
            ret[i] = (Instantiate(zombie, v2, new Quaternion(0, 0, 0, 0)));
        }

        return ret;
    }
}
