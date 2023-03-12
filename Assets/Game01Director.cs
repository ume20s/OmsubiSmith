using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Game01Director : MonoBehaviour
{
    // もろもろの変数
    float countdown = 3.99f;         // カウントダウン計数

    // 画像関連
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];

    // 音声関連
    AudioSource audioSource;
    public AudioClip[] vCd = new AudioClip[3];

    // ゲームオブジェクト
    GameObject guest;
    GameObject txtStage;
    GameObject txtScore;
    GameObject txtHighScore;
    GameObject txtTime;
    GameObject txtOmusubiName;
    GameObject txtOrder;
    GameObject btnMake;
    GameObject fukidashi;


    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの取得
        guest = GameObject.Find("guest");
        txtTime = GameObject.Find("txtTime");
        txtStage = GameObject.Find("txtStage");
        txtOrder = GameObject.Find("txtOrder");
        fukidashi = GameObject.Find("fukidashi");

        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // あらかじめ余計な表示を消しておく
        txtStage.SetActive(false);
        txtOrder.SetActive(false);
        fukidashi.SetActive(false);



        // カウントダウン
        CountDown();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private async void CountDown()
    {
        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        guest = GameObject.Find("guest");

        guest.GetComponent<SpriteRenderer>().sprite = Cd[2];
        audioSource.PlayOneShot(vCd[2]);
        await Task.Delay(1000);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[1];
        audioSource.PlayOneShot(vCd[1]);
        await Task.Delay(1000);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[0];
        audioSource.PlayOneShot(vCd[0]);
        await Task.Delay(1000);
    }

}
