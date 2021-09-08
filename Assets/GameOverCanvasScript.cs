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
        uint score = PhaseControllerScript.score;
        if (score != 0) //0 means a game hasn't been played yet
        {
            Text t = GetComponent<Text>();
            if (score == 1)
                t.text = "Game Over\n\n Tu as survécu " + score + " vague (nullos)";
            else
                t.text = "Game Over\n\n Tu as survécu " + score + " vagues !";
        }   
    }
}
