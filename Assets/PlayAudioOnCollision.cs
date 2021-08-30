using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
