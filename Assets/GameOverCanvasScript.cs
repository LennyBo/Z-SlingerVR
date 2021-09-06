using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvasScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //phase.score = 1;
        if (phase.score != 0) //means a game hasn't been played yet
        {
            Text t = GetComponent<Text>();
            t.text = "Game Over\n\n Tu as survecu " + phase.score + " vagues";
        }   
    }
}
