using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so10Ctrl : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        // 同じものが素材皿に乗っていなかったら
        if(dt.nowSozai[0] != 10 && dt.nowSozai[1] != 10) {
            // 左が空いていたら左にセット
            if(dt.nowSozai[0] == 0) {
                dt.nowSozai[0] = 10;
            } else {
                // 右が開いていたら右にセット
                if (dt.nowSozai[1] == 0) {
                    dt.nowSozai[1] = 10;
                }
            }
        }
    }
}
