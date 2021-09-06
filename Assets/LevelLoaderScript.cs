using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public Animator transition;
    public float delayLoad = 1f;
    private int index;

    private void Start()
    {
        //this.Invoke("switchToGameOver", 5);
    }

    public void switchToMenu()
    {
        StartCoroutine(LoadLevel("Scenes/MainMenuScene"));
    }

    public void switchToMainMap()
    {
        StartCoroutine(LoadLevel("Scenes/MainMap"));
    }


    IEnumerator LoadLevel(string index)
    {
        //Play animation
        transition.SetTrigger("Start");
        //Switch Scene
        yield return new WaitForSeconds(delayLoad);
        //Wait
        SceneManager.LoadScene(index);
    }
}
