using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MacDoubleClickScript : MonoBehaviour {

    private static MacDoubleClickScript macScript;

    private WaitForSeconds doubleClickTime = new WaitForSeconds(0.25f);
    private int clickCount;

    public bool DoubleClick { get; private set; }

    public static MacDoubleClickScript MacScript
    {
        get
        {
            return macScript;
        }

        set
        {
            macScript = value;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnPointerClick();
        }
    }

    public void OnPointerClick()
    {
        clickCount++;
        if(clickCount == 2)
        {
            DoubleClick = true;
            clickCount = 0;
        }
        else
        {
            StartCoroutine("CountDown");
            DoubleClick = false;
        }
    }

    private IEnumerator CountDown()
    {
        yield return doubleClickTime;
        if(clickCount > 0)
        {
            clickCount--;
        }
    }
}
