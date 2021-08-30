using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBloc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject bloc;
    private bool alreadyFired = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire2") != 0)
        {
            if (!alreadyFired)
            {
                alreadyFired = true;
                Vector3 vec = transform.position + transform.rotation.eulerAngles.normalized;
                Instantiate(bloc, vec, transform.rotation);
            }
        } else {
            alreadyFired = false;
        }
    }
}
