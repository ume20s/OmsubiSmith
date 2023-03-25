using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using TMPro;        // TextMeshPro用に必要

public class Game01Director : MonoBehaviour
{
    // 変数もろもろ
    private int Stage = 0;                  // ステージ
    private int GuestNum = 0;               // お客様番号
    private int OrderNum;                   // 注文おむすび番号
    private float remainTime = 60.999f;     // 残り時間
    private bool isCountDown = false;       // カウントダウン中
    private bool isTappable = false;        // タップ可能

    // おむすびポイント
    const int Point = 10;

    // 画像関連
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];
    public Sprite[] Sozai = new Sprite[21];

    // 音声関連
    AudioSource audioSource;
    public AudioClip[] vCd = new AudioClip[3];
    public AudioClip sePinpon;
    public AudioClip seBubuu;
    public AudioClip seStageClear;
    public AudioClip sePi;
    public AudioClip seTimeout;

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
    GameObject reorder;
    GameObject cover;
    GameObject maru;
    GameObject peke;
    GameObject stageclear;
    GameObject[] patatan = new GameObject[8];
    GameObject[] makesozai = new GameObject[2];

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
        fukidashi = GameObject.Find("fukidashi");
        reorder = GameObject.Find("reorder");
        cover = GameObject.Find("so00_cover");
        btnMake = GameObject.Find("btnMake");
        maru = GameObject.Find("maru");
        peke = GameObject.Find("peke");
        stageclear = GameObject.Find("stageclear");
        patatan[0] = GameObject.Find("patatan0");
        patatan[1] = GameObject.Find("patatan1");
        patatan[2] = GameObject.Find("patatan2");
        patatan[3] = GameObject.Find("patatan3");
        patatan[4] = GameObject.Find("patatan4");
        patatan[5] = GameObject.Find("patatan5");
        patatan[6] = GameObject.Find("patatan6");
        patatan[7] = GameObject.Find("patatan7");
        makesozai[0] = GameObject.Find("sozai1");
        makesozai[1] = GameObject.Find("sozai2");

        // 音声のコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // あらかじめ余計な表示を消しておく
        txtStage.SetActive(false);
        txtTime.SetActive(false);
        txtOrder.SetActive(false);
        txtOmusubiName.SetActive(false);
        btnMake.SetActive(false);
        maru.SetActive(false);
        peke.SetActive(false);
        stageclear.SetActive(false);
        fukidashi.SetActive(false);
        reorder.SetActive(false);
        for (int i = 0; i<8; i++)
        {
            patatan[i].SetActive(false);
        }

        // スコアを０に
        dt.Score = 0;
        txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");

        // フェーズ０から開始
        dt.Phase = 0;

        // カウントダウンとゲーム準備
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム中ならカウントダウン
        if(isCountDown)
        {
            remainTime -= Time.deltaTime;
            txtTime.GetComponent<Text>().text = ((int)remainTime).ToString();
            if((int)remainTime <= 0)
            {
                isCountDown = false;
                dt.Phase = 8;
            }
        }

        switch(dt.Phase)
        {
            // ゲーム開始のもろもろの設定
            case 1:
                // ゲーム素材の再表示
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);

                // お客様のセットと注文の設定
                guest.GetComponent<SpriteRenderer>().sprite = Guest[GuestNum];
                OrderNum = Random.Range(1, 34);
                dt.Phase++;

                // ゲーム開始！
                isCountDown = true;
                isTappable = true;
                break;

            // 注文を表示
            case 2:
                DispOrder();
                dt.Phase++;
                break;

            // 素材セレクトループ
            case 3:
                makesozai[0].GetComponent<SpriteRenderer>().sprite = Sozai[dt.nowSozai[0]];
                makesozai[1].GetComponent<SpriteRenderer>().sprite = Sozai[dt.nowSozai[1]];
                txtOmusubiName.GetComponent<Text>().text = dt.Omsubi[dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]]];
                break;

            // おむすび判定
            case 4:
                if(dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]] == OrderNum)
                {
                    correctOmsubi();
                }
                else
                {
                    incorrectOmsubi();
                }
                break;

            // 判定待ち
            case 5:
                break;

            // ステージクリア
            case 6:
                // カウントダウンを止める
                isCountDown = false;

                // BGM止めてステージクリア効果音
                audioSource.Stop();
                audioSource.PlayOneShot(seStageClear);

                // 余計な素材を非表示
                cover.SetActive(true);
                reorder.SetActive(false);
                fukidashi.SetActive(false);
                txtOmusubiName.SetActive(false);
                txtOrder.SetActive(false);
                btnMake.SetActive(false);
                guest.SetActive(false);
                makesozai[0].GetComponent<SpriteRenderer>().sprite = Sozai[0];
                makesozai[1].GetComponent<SpriteRenderer>().sprite = Sozai[0];

                // ステージクリア表示
                stageclear.SetActive(true);

                // 残り時間ボーナスを加算
                addBonus();

                dt.Phase = 7;
                break;

            // 次ステージ待ち
            case 7:
                // タップしたら
                if (Input.GetMouseButtonDown(0))
                {
                    // 次のステージへ
                    SceneManager.LoadScene("GameClearScene");
                }
                break;

            // ゲームオーバー
            case 8:
                gameOverEfect();
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

        // BGM開始
        audioSource.Play();

        // ゲーム状態を進める
        dt.Phase++;
    }

    // 注文の表示
    private async void DispOrder()
    {
        // タップ可能だったら処理開始
        if(isTappable)
        {
            // タップを一時抑制
            isTappable = false;

            // ボタンとおむすび名と素材パネルを隠す
            btnMake.SetActive(false);
            txtOmusubiName.SetActive(false);
            cover.SetActive(true);

            // 一定時間だけ注文セリフの表示
            fukidashi.SetActive(true);
            reorder.SetActive(false);
            OrderText.text = dt.guestTalk[Stage, GuestNum, 0] +
                "<u><color=#cc0000>" + dt.Omsubi[OrderNum] + "</color></u>" +
                dt.guestTalk[Stage, GuestNum, 1];
            await Task.Delay(1500);
            fukidashi.SetActive(false);
            reorder.SetActive(true);

            // ボタンとおむすび名と素材パネルを表示
            btnMake.SetActive(true);
            txtOmusubiName.SetActive(true);
            cover.SetActive(false);

            // タップ抑制を解除
            isTappable = true;
        }
    }

    // おむすび正解
    private async void correctOmsubi()
    {
        // タップ可能だったら処理開始
        if(isTappable)
        {
            // タップを一時抑制
            isTappable = false;

            // 判定処理中
            dt.Phase = 5;

            // ポイント加算
            dt.Score += Point;
            txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");

            // ピンポン○
            audioSource.PlayOneShot(sePinpon);
            patatan[0].SetActive(true);
            maru.SetActive(true);
            await Task.Delay(500);

            // 皿上の素材と○を消してパタタン増やす
            dt.nowSozai[0] = 0;
            dt.nowSozai[1] = 0;
            maru.SetActive(false);
            patatan[0].SetActive(false);

            // 次のお客さんへ
            GuestNum++;
            if (GuestNum < 6)
            {
                dt.Phase = 1;
            }
            else
            {
                dt.Phase = 6;
            }

            // タップ抑制を解除
            isTappable = true;
        }
    }

    // おむすび間違い
    private async void incorrectOmsubi()
    {
        // タップ可能だったら処理開始
        if (isTappable)
        {
            // タップを一時抑制
            isTappable = false;

            // 判定処理中
            dt.Phase = 5;

            // 残り時間を５秒減らす
            remainTime -= 5.0f;
            if(remainTime < 0)
            {
                remainTime = 0;
            }

            // ブッブー×
            audioSource.PlayOneShot(seBubuu);
            peke.SetActive(true);
            await Task.Delay(1500);

            // 皿上の素材と○を消す
            dt.nowSozai[0] = 0;
            dt.nowSozai[1] = 0;
            peke.SetActive(false);

            // タップ抑制を解除
            isTappable = true;

            // 素材選択フェーズへ
            dt.Phase = 3;
        }
    }

    // 残り時間によってボーナスポイントを加算
    private async void addBonus()
    {
        // 効果音の時間だけちょっと待つ
        await Task.Delay(2000);

        int Bonus = (int)remainTime;
        while (Bonus > 0)
        {
            int div;
            if (Bonus > 10)
            {
                div = 5;
            }
            else
            {
                div = 1;
            }
            dt.Score += div;
            Bonus -= div;
            txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");
            txtTime.GetComponent<Text>().text = Bonus.ToString();
            audioSource.PlayOneShot(sePi);
            await Task.Delay(70);
        }

    }

    // ゲームオーバーエフェクト
    private async void gameOverEfect()
    {
        // 二度と帰ってこないようにゲームフェーズを99にする
        dt.Phase = 99;

        // カウントダウン停止・タップ抑制
        isCountDown = false;
        isTappable = false;

        // BGM止めてタイムアウト効果音
        audioSource.Stop();
        audioSource.PlayOneShot(seTimeout);
        await Task.Delay(2700);

        // ゲームオーバー画面へ
        SceneManager.LoadScene("GameOverScene");
    }
}
