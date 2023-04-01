using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using TMPro;        // TextMeshPro用に必要

public class Game02Director : MonoBehaviour
{
    // 定数もろもろ
    const int Stage = 1;            // ●ステージ－１
    const int Point = 20;           // ●おむすびポイント
    const int ShowTime = 1800;      // ●注文表示時間

    // 変数もろもろ
    // private float remainTime = 120.999f;             // ●残り時間
    private float remainTime = 70.999f;             // ●残り時間
    private int GuestNum = 0;                       // お客様番号
    private int OrderMax = Stage+1;                 // 注文おむすび個数
    private int[] OrderNum = new int[Stage+1];      // 注文おむすび番号
    private bool[] OrderHit = new bool[Stage+1];    // 正解おむすび番号
    private int OrderHitNum = 0;                    // 正解数
    private bool isCountDown = false;               // カウントダウン中

    // 画像関連
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];
    public Sprite[] Sozai = new Sprite[21];
    public Sprite Patatan;
    public Sprite PataHino;

    // 音声関連
    AudioSource audioSource;
    public AudioClip[] vCd = new AudioClip[3];
    public AudioClip vStart;
    public AudioClip sePinpon;
    public AudioClip seBubuu;
    public AudioClip seStageClear;
    public AudioClip sePi;
    public AudioClip seResetSozai;
    public AudioClip seTimeout;

    // ゲームオブジェクト
    GameObject guest;
    GameObject background;
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
        background = GameObject.Find("background");
        txtStage = GameObject.Find("txtStage");
        txtScore = GameObject.Find("txtScore");
        txtHighScore = GameObject.Find("txtHighscore");
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
        background.SetActive(false);
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

        // スコアを前のステージから引き継ぐ
        txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");

        // タップ不可
        dt.isTappable = false;

        // ハイスコア表示
        txtHighScore.GetComponent<Text>().text = "HighScore:" + dt.HighScore.ToString("D4");

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
                background.SetActive(true);
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);

                // お客様のセットと注文の設定
                guest.GetComponent<SpriteRenderer>().sprite = Guest[GuestNum];
                OrderHitNum = 0;

                // OrderMax個のおむすび注文
                for (int i=0; i < OrderMax; i++)
                {
                    OrderNum[i] = Random.Range(1, 34);
                    OrderHit[i] = false;
                }
                dt.Phase++;

                // ゲーム開始！
                isCountDown = true;
                dt.isTappable = true;
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
                // 合ってるのあるかな？
                bool HitFlg = false;
                for (int i = 0; i < OrderMax; i++)
                {
                    if ((dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]] == OrderNum[i]) && !OrderHit[i])
                    {
                        HitFlg = true;
                        OrderHit[i] = true;
                        break;
                    }
                }

                // 合ってるのあった！
                if (HitFlg)
                {
                    correctOmsubi();
                }
                else    // なかった！
                {
                    incorrectOmsubi();
                }
                break;

            // 判定待ち
            case 5:
                break;

            // ステージクリア
            case 6:
                // カウントダウンを止めてタップ抑制
                isCountDown = false;
                dt.isTappable = false;

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
                // タップ可能であれば
                if (dt.isTappable)
                {
                    // タップしたら
                    if (Input.GetMouseButtonDown(0))
                    {
                        // 次のステージへ
                        SceneManager.LoadScene("Game03Scene");
                    }
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
        guest.GetComponent<SpriteRenderer>().sprite = Cd[0];
        audioSource.PlayOneShot(vStart);

        // BGM開始
        audioSource.Play();

        // ゲーム状態を進める
        dt.Phase++;
    }

    // 注文の表示
    private async void DispOrder()
    {
        // タップ可能だったら処理開始
        if(dt.isTappable)
        {
            // タップを一時抑制
            dt.isTappable = false;

            // ボタンとおむすび名と素材パネルを隠す
            btnMake.SetActive(false);
            txtOmusubiName.SetActive(false);
            cover.SetActive(true);

            // 一定時間だけ注文セリフの表示
            fukidashi.SetActive(true);
            reorder.SetActive(false);
            OrderText.text = dt.guestTalk[Stage, GuestNum, 0] + "<br>";
            for (int i=0; i<OrderMax; i++)
            {
                if (OrderHit[i])
                {
                    OrderText.text += "<color=#aaaaaa>" + dt.Omsubi[OrderNum[i]] + "</color>";
                }
                else
                {
                    OrderText.text += "<color=#cc0000>" + dt.Omsubi[OrderNum[i]] + "</color>";
                }
                if (i < OrderMax-1)
                {
                    OrderText.text += "と、<br>";
                }
                else
                {
                    OrderText.text += "<br>";
                }
            }
            OrderText.text += dt.guestTalk[Stage, GuestNum, 1];
            await Task.Delay(ShowTime);
            fukidashi.SetActive(false);
            reorder.SetActive(true);

            // ボタンとおむすび名と素材パネルを表示
            btnMake.SetActive(true);
            txtOmusubiName.SetActive(true);
            cover.SetActive(false);

            // タップ抑制を解除
            dt.isTappable = true;
        }
    }

    // おむすび正解
    private async void correctOmsubi()
    {
        // タップ可能だったら処理開始
        if(dt.isTappable)
        {
            // タップを一時抑制
            dt.isTappable = false;

            // 判定処理中
            dt.Phase = 5;

            // ポイント加算
            dt.Score += Point;
            txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");
            checkHighScore();

            // ピンポン○
            audioSource.PlayOneShot(sePinpon);
            maru.SetActive(true);

            // もし梅おむすびだったら日の丸おむすび
            if(dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]] == 4)
            {
                patatan[OrderHitNum].GetComponent<SpriteRenderer>().sprite = PataHino;
            }
            else
            {
                patatan[OrderHitNum].GetComponent<SpriteRenderer>().sprite = Patatan;
            }

            // パタタン増やす
            patatan[OrderHitNum].SetActive(true);
            OrderHitNum++;

            // 0.5秒くらい待ちます
            await Task.Delay(500);

            // 皿上の素材と○を消す
            dt.nowSozai[0] = 0;
            dt.nowSozai[1] = 0;
            maru.SetActive(false);

            // おむすび注文完了で次のお客さん
            if(OrderHitNum == OrderMax)
            {
                for(int i=0; i<OrderMax; i++)
                {
                    patatan[i].SetActive(false);
                }
                GuestNum++;

                // ６人完了で次のステージへ
                if (GuestNum < 6)
                {
                    dt.Phase = 1;
                }
                else
                {
                    dt.Phase = 6;
                }
            }
            else
            {
                dt.Phase = 3;
            }

            // タップ抑制を解除
            dt.isTappable = true;
        }
    }

    // おむすび間違い
    private async void incorrectOmsubi()
    {
        // タップ可能だったら処理開始
        if (dt.isTappable)
        {
            // タップを一時抑制
            dt.isTappable = false;

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
            await Task.Delay(1000);

            // 皿上の素材と○を消す
            dt.nowSozai[0] = 0;
            dt.nowSozai[1] = 0;
            peke.SetActive(false);

            // タップ抑制を解除
            dt.isTappable = true;

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
                div = 4;
            }
            else
            {
                div = 1;
            }
            dt.Score += (div * (Stage+1));
            Bonus -= div;
            checkHighScore();
            txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");
            txtTime.GetComponent<Text>().text = Bonus.ToString();
            audioSource.PlayOneShot(sePi);
            await Task.Delay(70);
        }

        // タップ抑制解除
        dt.isTappable = true;
    }

    // ハイスコアチェック
    private void checkHighScore()
    {
        // 現スコアがハイスコアを上回ったら
        if(dt.Score > dt.HighScore)
        {
            // ハイスコア更新
            dt.HighScore = dt.Score;
            txtHighScore.GetComponent<Text>().text = "HighScore:" + dt.HighScore.ToString("D4");

            // ハイスコア保存
            PlayerPrefs.SetInt(dt.SAVE_KEY, dt.HighScore);
            PlayerPrefs.Save();
        }
    }

    // ゲームオーバーエフェクト
    private async void gameOverEfect()
    {
        // 二度と帰ってこないようにゲームフェーズを99にする
        dt.Phase = 99;

        // カウントダウン停止・タップ抑制
        isCountDown = false;
        dt.isTappable = false;

        // BGM止めてタイムアウト効果音
        audioSource.Stop();
        audioSource.PlayOneShot(seTimeout);
        await Task.Delay(2700);

        // ゲームオーバー画面へ
        SceneManager.LoadScene("GameOverScene");
    }
}
