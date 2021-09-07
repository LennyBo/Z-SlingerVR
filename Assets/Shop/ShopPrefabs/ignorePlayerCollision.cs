using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorePlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), FindObjectOfType<CharacterController>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
