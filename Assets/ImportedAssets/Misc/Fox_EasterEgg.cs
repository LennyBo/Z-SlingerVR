using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_EasterEgg : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] private AudioClip clip_stop_that;
    [SerializeField] private AudioClip clip_stop;
    [SerializeField] private AudioClip clip_madness;

    void OnCollisionEnter(Collision collision)
    {
        AudioSource audio = GetComponent<AudioSource>();
        switch(counter++) {
            case 3: audio.clip = clip_stop_that; break;
            case 4: audio.clip = clip_stop; break;
            case 6: audio.clip = clip_madness; break;
        }
        audio.Play();
    }
}
