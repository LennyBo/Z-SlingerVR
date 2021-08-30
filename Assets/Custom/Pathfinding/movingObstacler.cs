using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObstacler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    
    }

    bool goingLeft = false;
    // Update is called once per frame
    void Update()
    {
        int x = goingLeft ? -1 : 1;
        Vector3 Vec = transform.localPosition;
        Vec.x += x * Time.deltaTime * 5;
        transform.localPosition = Vec;


        if (goingLeft && transform.localPosition.x < 0)
            goingLeft = false;
        else if (!goingLeft && transform.localPosition.x > 20)
            goingLeft = true;
    }
}
