using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;        // TextMeshPro用に必要

public class Game01Director : MonoBehaviour
{
    // 定数もろもろ
    const int Stage = 0;            // ステージ−１
    const int Point = 10;           // おむすびポイント
    const float ShowTime = 1.0f;    // 注文表示時間

    // 変数もろもろ
    private float remainTime = 60.999f;     // 残り時間
    private int GuestNum = 0;               // お客様番号
    private int OrderNum;                   // 注文おむすび番号
    private bool isCountDown = false;       // カウントダウン中

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

        // スコアを０に
        dt.Score = 0;
        txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");

        // タップ不可
        dt.isTappable = false;

        // ハイスコア表示
        txtHighScore.GetComponent<Text>().text = "HighScore:" + dt.HighScore.ToString("D4");

        // フェーズ０から開始
        dt.Phase = 0;

        // カウントダウンとゲーム準備
        StartCoroutine("CountDown");
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
                OrderNum = Random.Range(1, 34);
                dt.Phase++;

                // ゲーム開始！
                isCountDown = true;
                dt.isTappable = true;
                break;

            // 注文を表示
            case 2:
                StartCoroutine("DispOrder");
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
                    StartCoroutine("correctOmsubi");
                }
                else
                {
                    StartCoroutine("incorrectOmsubi");
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
                StartCoroutine("addBonus");

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
                        SceneManager.LoadScene("Game02Scene");
                    }
                }
                break;

            // ゲームオーバー
            case 8:
                StartCoroutine("gameOverEffect");
                break;
        }
    }

    // カウントダウン！
    IEnumerator CountDown()
    {
        guest.GetComponent<SpriteRenderer>().sprite = Cd[2];
        audioSource.PlayOneShot(vCd[2]);
        yield return new WaitForSeconds(1.0f);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[1];
        audioSource.PlayOneShot(vCd[1]);
        yield return new WaitForSeconds(1.0f);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[0];
        audioSource.PlayOneShot(vCd[0]);
        yield return new WaitForSeconds(1.0f);
        guest.GetComponent<SpriteRenderer>().sprite = Cd[0];
        audioSource.PlayOneShot(vStart);

        // BGM開始
        audioSource.Play();

        // ゲーム状態を進める
        dt.Phase++;
    }

    // 注文の表示
    IEnumerator DispOrder()
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
            OrderText.text = dt.guestTalk[Stage, GuestNum, 0] + "<br>" +
                "<color=#cc0000>" + dt.Omsubi[OrderNum] + "</color><br>" +
                dt.guestTalk[Stage, GuestNum, 1];
            yield return new WaitForSeconds(ShowTime);
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
    IEnumerator correctOmsubi()
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
            if(OrderNum == 4)
            {
                patatan[0].GetComponent<SpriteRenderer>().sprite = PataHino;
            }
            else
            {
                patatan[0].GetComponent<SpriteRenderer>().sprite = Patatan;
            }
            patatan[0].SetActive(true);

            // 0.5秒くらい待ちます
            yield return new WaitForSeconds(0.5f);

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
            dt.isTappable = true;
        }
    }

    // おむすび間違い
    IEnumerator incorrectOmsubi()
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
            yield return new WaitForSeconds(1.5f);

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
    IEnumerator addBonus()
    {
        // 効果音の時間だけちょっと待つ
        yield return new WaitForSeconds(2.0f);

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
            dt.Score += div;
            Bonus -= div;
            checkHighScore();
            txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");
            txtTime.GetComponent<Text>().text = Bonus.ToString();
            audioSource.PlayOneShot(sePi);
            yield return new WaitForSeconds(0.07f);
        }

        // タップ抑制解除
        dt.isTappable = true;
    }

    // ハイスコアチェック
    private void checkHighScore()
    {
        // 現スコアがハイスコアを上回ったら
        if (dt.Score > dt.HighScore)
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
    IEnumerator gameOverEffect()
    {
        // 二度と帰ってこないようにゲームフェーズを99にする
        dt.Phase = 99;

        // カウントダウン停止・タップ抑制
        isCountDown = false;
        dt.isTappable = false;

        // BGM止めてタイムアウト効果音
        audioSource.Stop();
        audioSource.PlayOneShot(seTimeout);
        yield return new WaitForSeconds(2.7f);

        // ゲームオーバー画面へ
        SceneManager.LoadScene("GameOverScene");
    }
}
