using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{

    public GameObject targetPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pressed()
    {
        Debug.Log("Navigate");
        GameObject g = Instantiate(targetPrefab, transform.parent.parent);
        Debug.Log(transform.parent.parent.name);

        //g.transform.SetParent(transform.parent.parent);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(1, 1, 0);
        g.transform.localRotation = new Quaternion(0, 0, 0, 0);

        Destroy(transform.parent.gameObject);

    }
}
