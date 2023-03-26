using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sozai2Ctrl : MonoBehaviour
{
    // 音声関連
    AudioSource audioSource;
    public AudioClip seResetSozai;

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
        if(dt.isTappable)
        {
            // 素材をクリア
            dt.nowSozai[1] = 0;
            audioSource.PlayOneShot(seResetSozai);
        }
    }
}
