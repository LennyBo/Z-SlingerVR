using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour
{
   
     public void onClickFunc()
    {
        LevelLoaderScript level = FindObjectOfType<LevelLoaderScript>();
        level.switchToMainMap();
    }
}
