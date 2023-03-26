using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so08Ctrl : MonoBehaviour
{
    // 音声関連
    AudioSource audioSource;
    public AudioClip seSetSozai;

    // Start is called before the first frame update
    void Start()
    {
        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    // タップしたら
    public void onClick()
    {
        // タップ可能だったら
        if (dt.isTappable)
        {
            // 同じものが素材皿に乗っていなかったら
            if (dt.nowSozai[0] != 8 && dt.nowSozai[1] != 8)
            {
                // 左が空いていたら左にセット
                if (dt.nowSozai[0] == 0)
                {
                    dt.nowSozai[0] = 8;
                    audioSource.PlayOneShot(seSetSozai);
                }
                else
                {
                    // 右が開いていたら右にセット
                    if (dt.nowSozai[1] == 0)
                    {
                        dt.nowSozai[1] = 8;
                        audioSource.PlayOneShot(seSetSozai);
                    }
                }
            }
        }
    }
}
