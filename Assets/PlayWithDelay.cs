using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWithDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource aS = GetComponent<AudioSource>();
        aS.PlayDelayed(5);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
