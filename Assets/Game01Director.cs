using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using TMPro;        // TextMeshPro用に必要

public class Game01Director : MonoBehaviour
{
    // 変数もろもろ
    private int Stage = 0;                  // ステージ
    private int GuestNum = 0;               // お客様番号
    private int OrderNum;                   // 注文おむすび番号
    private int Phase = 0;                  // ゲーム状態遷移

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
    GameObject cover;
    GameObject[] patatan = new GameObject[8];
    GameObject[] sozai = new GameObject[2];

    // TextMeshPro用注文オブジェクト
    [SerializeField] TextMeshProUGUI OrderText;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの取得
        guest = GameObject.Find("guest");
        txtStage = GameObject.Find("txtStage");
        txtScore = GameObject.Find("txtScore");
        txtHighScore = GameObject.Find("txtHighScore");
        txtTime = GameObject.Find("txtTime");
        txtOmusubiName = GameObject.Find("txtOmusubiName");
        txtOrder = GameObject.Find("txtOrder");
        btnMake = GameObject.Find("btnMake");
        fukidashi = GameObject.Find("fukidashi");
        cover = GameObject.Find("so00_cover");
        patatan[0] = GameObject.Find("patatan0");
        patatan[1] = GameObject.Find("patatan1");
        patatan[2] = GameObject.Find("patatan2");
        patatan[3] = GameObject.Find("patatan3");
        patatan[4] = GameObject.Find("patatan4");
        patatan[5] = GameObject.Find("patatan5");
        patatan[6] = GameObject.Find("patatan6");
        patatan[7] = GameObject.Find("patatan7");
        sozai[0] = GameObject.Find("sozai1");
        sozai[1] = GameObject.Find("sozai2");

        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // あらかじめ余計な表示を消しておく
        txtStage.SetActive(false);
        txtTime.SetActive(false);
        txtOrder.SetActive(false);
        btnMake.SetActive(false);
        txtOmusubiName.SetActive(false);
        fukidashi.SetActive(false);
        for(int i = 0; i<8; i++)
        {
            patatan[i].SetActive(false);
        }
        sozai[0].SetActive(false);
        sozai[1].SetActive(false);

        // カウントダウンとゲーム準備
        CountDown();

    }

    // Update is called once per frame
    void Update()
    {
        switch(Phase)
        {
            // ゲーム開始のもろもろの設定
            case 1:
                // ゲーム素材の再表示
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);
                txtOmusubiName.SetActive(true);
                fukidashi.SetActive(true);

                // お客様のセットと注文の設定
                guest.GetComponent<SpriteRenderer>().sprite = Guest[GuestNum];
                OrderNum = Random.Range(1, 34);
                Phase++;
                break;

            case 2:
                // 注文の表示
                DispOrder();
                Phase++;
                break;

        }
    }

    // カウントダウン！
    private async void CountDown()
    {
        guest.GetComponent<SpriteRenderer>().sprite = Cd[2];
        audioSource.PlayOneShot(vCd[2]);
        await Task.Delay(1000);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[1];
        audioSource.PlayOneShot(vCd[1]);
        await Task.Delay(1000);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[0];
        audioSource.PlayOneShot(vCd[0]);
        await Task.Delay(1000);

        // ゲーム状態を進める
        Phase++;
    }

    // 注文の表示
    private async void DispOrder()
    {
        // ボタンと素材パネルを隠す
        btnMake.SetActive(false);
        cover.SetActive(true);

        // 一定時間だけ注文セリフの表示
        OrderText.text = dt.guestTalk[Stage, GuestNum, 0] +
            "<u><color=#cc0000>" + dt.Omsubi[OrderNum] + "</color></u>" +
            dt.guestTalk[Stage, GuestNum, 1];
        await Task.Delay(1500);
        OrderText.text = "もう１回注文を聞く";

        // ボタンと素材パネルを表示
        btnMake.SetActive(true);
        cover.SetActive(false);
    }

    // もう１回注文を聞く
    public void ReOrder()
    {
        Phase = 1;
    }
}
