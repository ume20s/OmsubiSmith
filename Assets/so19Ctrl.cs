using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so19Ctrl : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        // 同じものが素材皿に乗っていなかったら
        if(dt.nowSozai[0] != 19 && dt.nowSozai[1] != 19) {
            // 左が空いていたら左にセット
            if(dt.nowSozai[0] == 0) {
                dt.nowSozai[0] = 19;
            } else {
                // 右が開いていたら右にセット
                if (dt.nowSozai[1] == 0) {
                    dt.nowSozai[1] = 19;
                }
            }
        }
    }
}
