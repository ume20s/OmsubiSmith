using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so04Ctrl : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        // 同じものが素材皿に乗っていなかったら
        if(dt.nowSozai[0] != 4 && dt.nowSozai[1] != 4) {
            // 左が空いていたら左にセット
            if(dt.nowSozai[0] == 0) {
                dt.nowSozai[0] = 4;
            } else {
                // 右が開いていたら右にセット
                if (dt.nowSozai[1] == 0) {
                    dt.nowSozai[1] = 4;
                }
            }
        }
    }
}
