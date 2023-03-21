using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so01Ctrl : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        // 同じものが素材皿に乗っていなかったら
        if(dt.nowSozai[0] != 1 && dt.nowSozai[1] != 1) {
            // 左が空いていたら左にセット
            if(dt.nowSozai[0] == 0) {
                dt.nowSozai[0] = 1;
            } else {
                // 右が開いていたら右にセット
                if (dt.nowSozai[1] == 0) {
                    dt.nowSozai[1] = 1;
                }
            }
        }
    }
}
