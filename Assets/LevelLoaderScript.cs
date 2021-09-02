using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public Animator transition;
    public float delayLoad = 1f;

    private void Start()
    {
        this.Invoke("switchToGameOver", 5);
    }
    public void switchToMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void switchToGameOver()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void switchToMainMap()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int index)
    {
        //Play animation
        transition.SetTrigger("Start");
        //Switch Scene
        yield return new WaitForSeconds(delayLoad);
        //Wait
        SceneManager.LoadScene(index);
    }
}
