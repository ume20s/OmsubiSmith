using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fukidasiCtrl : MonoBehaviour
{
    // タップしたら注文を再表示
    public void onClickFikidashi()
    {
        Game01Director.Phase = 2;
    }
}
