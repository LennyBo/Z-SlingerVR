using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIButton : MonoBehaviour
{
    //Each button of the UI needs a UIButton that the cursor will call pressed on
    public abstract void pressed();
}
