using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnCtrl : MonoBehaviour
{
    // Update is called once per frame
    public void Judgement()
    {
        if (dt.isTappable)
        {
            dt.Phase = 4;
        }
    }
}
