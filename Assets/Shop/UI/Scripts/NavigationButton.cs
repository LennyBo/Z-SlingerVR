using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButton : UIButton
{

    public GameObject targetPrefab;

    public override void pressed()
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
