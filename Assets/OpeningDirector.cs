using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningDirector : MonoBehaviour
{
    // BGM関連
    AudioSource audioSource;
    public AudioClip[] sOpening = new AudioClip[2];

    // Start is called before the first frame update
    void Start()
    {
        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // ランダムでBGMを選択
        int song = Random.Range(0, 5);
        switch (song)
        {
            case 0:
                audioSource.clip = sOpening[1];
                break;
            default:
                audioSource.clip = sOpening[0];
                break;
        }
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Game01Scene");
        }
    }
}
