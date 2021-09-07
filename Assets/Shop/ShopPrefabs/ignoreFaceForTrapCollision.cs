using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreFaceForTrapCollision : MonoBehaviour
{

    [SerializeField] private GameObject faceForTrap;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), faceForTrap.GetComponent<Collider>());
    }
}
