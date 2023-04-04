using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game00Director : MonoBehaviour
{
    // BGM関連
    AudioSource audioSource;
    public AudioClip[] sOpening = new AudioClip[2];

    // Start is called before the first frame update
    void Start()
    {
        // 大域変数の初期化
        dt.nowSozai[0] = 0;
        dt.nowSozai[1] = 0;
        dt.Phase = 0;
        dt.Score = 0;

        // デバッグ用ハイスコアをゼロクリア
        // dt.HighScore = 0;
        // PlayerPrefs.SetInt(dt.SAVE_KEY, dt.HighScore);
        // PlayerPrefs.Save();

        // ハイスコア読み込み
        dt.HighScore = PlayerPrefs.GetInt(dt.SAVE_KEY, 0);

        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // ランダムでBGMを選択
        int song = Random.Range(0, 5);
        switch (song)
        {
            case 0:
                audioSource.clip = sOpening[0];
                break;
            default:
                audioSource.clip = sOpening[1];
                break;
        }
        audioSource.Play();
    }
}
