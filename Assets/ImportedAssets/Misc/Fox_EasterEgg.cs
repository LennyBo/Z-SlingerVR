using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_EasterEgg : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] private AudioClip otherclip;

    void OnCollisionEnter(Collision collision)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (counter++ == 2)
            audio.clip = otherclip;
        audio.Play();
    }
}
