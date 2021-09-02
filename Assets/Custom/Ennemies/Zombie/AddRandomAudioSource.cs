using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomAudioSource : MonoBehaviour
{

    public List<AudioClip> audioClips;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource aS = GetComponent<AudioSource>();
        aS.clip = audioClips[Random.Range(0, audioClips.Count - 1)];
        aS.PlayDelayed(Random.Range(0, 1));
        aS.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
