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
        if (PhaseControllerScript.score != 0) //means a game hasn't been played yet
        {
            Text t = GetComponent<Text>();
            t.text = "Game Over\n\n Tu as survecu " + PhaseControllerScript.score + " vagues";
        }   
    }
}
