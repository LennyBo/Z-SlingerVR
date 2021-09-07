using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingoreTrapsCollision : MonoBehaviour
{

    [SerializeField] private GameObject trap;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), trap.GetComponent<Collider>());
    }
}
