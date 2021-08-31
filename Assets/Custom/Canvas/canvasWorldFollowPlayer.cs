using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasWorldFollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform player;
    private Transform me;

    private Vector3 currentEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Transform>();
        currentEulerAngles = me.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        me.LookAt(player);
    }
}
