using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using TMPro;        // TextMeshPro�p�ɕK�v

public class Game02Director : MonoBehaviour
{
    // �萔�������
    const int Stage = 1;            // ���X�e�[�W�|�P
    const int Point = 20;           // �����ނ��у|�C���g
    const int ShowTime = 1800;      // �������\������

    // �ϐ��������
    // private float remainTime = 120.999f;             // ���c�莞��
    private float remainTime = 70.999f;             // ���c�莞��
    private int GuestNum = 0;                       // ���q�l�ԍ�
    private int OrderMax = Stage+1;                 // �������ނ��ь�
    private int[] OrderNum = new int[Stage+1];      // �������ނ��єԍ�
    private bool[] OrderHit = new bool[Stage+1];    // �������ނ��єԍ�
    private int OrderHitNum = 0;                    // ����
    private bool isCountDown = false;               // �J�E���g�_�E����

    // �摜�֘A
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];
    public Sprite[] Sozai = new Sprite[21];
    public Sprite Patatan;
    public Sprite PataHino;

    // �����֘A
    AudioSource audioSource;
    public AudioClip[] vCd = new AudioClip[3];
    public AudioClip vStart;
    public AudioClip sePinpon;
    public AudioClip seBubuu;
    public AudioClip seStageClear;
    public AudioClip sePi;
    public AudioClip seResetSozai;
    public AudioClip seTimeout;

    // �Q�[���I�u�W�F�N�g
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

    // TextMeshPro�p�����I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI OrderText;

    // Start is called before the first frame update
    void Start()
    {
        // �I�u�W�F�N�g�̎擾
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

        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        // ���炩���ߗ]�v�ȕ\���������Ă���
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

        // �X�R�A��O�̃X�e�[�W��������p��
        txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");

        // �^�b�v�s��
        dt.isTappable = false;

        // �n�C�X�R�A�\��
        txtHighScore.GetComponent<Text>().text = "HighScore:" + dt.HighScore.ToString("D4");

        // �t�F�[�Y�O����J�n
        dt.Phase = 0;

        // �J�E���g�_�E���ƃQ�[������
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�����Ȃ�J�E���g�_�E��
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
            // �Q�[���J�n�̂������̐ݒ�
            case 1:
                // �Q�[���f�ނ̍ĕ\��
                background.SetActive(true);
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);

                // ���q�l�̃Z�b�g�ƒ����̐ݒ�
                guest.GetComponent<SpriteRenderer>().sprite = Guest[GuestNum];
                OrderHitNum = 0;

                // OrderMax�̂��ނ��ђ���
                for (int i=0; i < OrderMax; i++)
                {
                    OrderNum[i] = Random.Range(1, 34);
                    OrderHit[i] = false;
                }
                dt.Phase++;

                // �Q�[���J�n�I
                isCountDown = true;
                dt.isTappable = true;
                break;

            // ������\��
            case 2:
                DispOrder();
                dt.Phase++;
                break;

            // �f�ރZ���N�g���[�v
            case 3:
                makesozai[0].GetComponent<SpriteRenderer>().sprite = Sozai[dt.nowSozai[0]];
                makesozai[1].GetComponent<SpriteRenderer>().sprite = Sozai[dt.nowSozai[1]];
                txtOmusubiName.GetComponent<Text>().text = dt.Omsubi[dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]]];
                break;

            // ���ނ��є���
            case 4:
                // �����Ă�̂��邩�ȁH
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

                // �����Ă�̂������I
                if (HitFlg)
                {
                    correctOmsubi();
                }
                else    // �Ȃ������I
                {
                    incorrectOmsubi();
                }
                break;

            // ����҂�
            case 5:
                break;

            // �X�e�[�W�N���A
            case 6:
                // �J�E���g�_�E�����~�߂ă^�b�v�}��
                isCountDown = false;
                dt.isTappable = false;

                // BGM�~�߂ăX�e�[�W�N���A���ʉ�
                audioSource.Stop();
                audioSource.PlayOneShot(seStageClear);

                // �]�v�ȑf�ނ��\��
                cover.SetActive(true);
                reorder.SetActive(false);
                fukidashi.SetActive(false);
                txtOmusubiName.SetActive(false);
                txtOrder.SetActive(false);
                btnMake.SetActive(false);
                guest.SetActive(false);
                makesozai[0].GetComponent<SpriteRenderer>().sprite = Sozai[0];
                makesozai[1].GetComponent<SpriteRenderer>().sprite = Sozai[0];

                // �X�e�[�W�N���A�\��
                stageclear.SetActive(true);

                // �c�莞�ԃ{�[�i�X�����Z
                addBonus();

                dt.Phase = 7;
                break;

            // ���X�e�[�W�҂�
            case 7:
                // �^�b�v�\�ł����
                if (dt.isTappable)
                {
                    // �^�b�v������
                    if (Input.GetMouseButtonDown(0))
                    {
                        // ���̃X�e�[�W��
                        SceneManager.LoadScene("Game03Scene");
                    }
                }
                break;

            // �Q�[���I�[�o�[
            case 8:
                gameOverEfect();
                break;
        }
    }

    // �J�E���g�_�E���I
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

        // BGM�J�n
        audioSource.Play();

        // �Q�[����Ԃ�i�߂�
        dt.Phase++;
    }

    // �����̕\��
    private async void DispOrder()
    {
        // �^�b�v�\�������珈���J�n
        if(dt.isTappable)
        {
            // �^�b�v���ꎞ�}��
            dt.isTappable = false;

            // �{�^���Ƃ��ނ��і��Ƒf�ރp�l�����B��
            btnMake.SetActive(false);
            txtOmusubiName.SetActive(false);
            cover.SetActive(true);

            // ��莞�Ԃ��������Z���t�̕\��
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
                    OrderText.text += "�ƁA<br>";
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

            // �{�^���Ƃ��ނ��і��Ƒf�ރp�l����\��
            btnMake.SetActive(true);
            txtOmusubiName.SetActive(true);
            cover.SetActive(false);

            // �^�b�v�}��������
            dt.isTappable = true;
        }
    }

    // ���ނ��ѐ���
    private async void correctOmsubi()
    {
        // �^�b�v�\�������珈���J�n
        if(dt.isTappable)
        {
            // �^�b�v���ꎞ�}��
            dt.isTappable = false;

            // ���菈����
            dt.Phase = 5;

            // �|�C���g���Z
            dt.Score += Point;
            txtScore.GetComponent<Text>().text = "Score:" + dt.Score.ToString("D4");
            checkHighScore();

            // �s���|����
            audioSource.PlayOneShot(sePinpon);
            maru.SetActive(true);

            // �����~���ނ��т���������̊ۂ��ނ���
            if(dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]] == 4)
            {
                patatan[OrderHitNum].GetComponent<SpriteRenderer>().sprite = PataHino;
            }
            else
            {
                patatan[OrderHitNum].GetComponent<SpriteRenderer>().sprite = Patatan;
            }

            // �p�^�^�����₷
            patatan[OrderHitNum].SetActive(true);
            OrderHitNum++;

            // 0.5�b���炢�҂��܂�
            await Task.Delay(500);

            // �M��̑f�ނƁ�������
            dt.nowSozai[0] = 0;
            dt.nowSozai[1] = 0;
            maru.SetActive(false);

            // ���ނ��ђ��������Ŏ��̂��q����
            if(OrderHitNum == OrderMax)
            {
                for(int i=0; i<OrderMax; i++)
                {
                    patatan[i].SetActive(false);
                }
                GuestNum++;

                // �U�l�����Ŏ��̃X�e�[�W��
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

            // �^�b�v�}��������
            dt.isTappable = true;
        }
    }

    // ���ނ��ъԈႢ
    private async void incorrectOmsubi()
    {
        // �^�b�v�\�������珈���J�n
        if (dt.isTappable)
        {
            // �^�b�v���ꎞ�}��
            dt.isTappable = false;

            // ���菈����
            dt.Phase = 5;

            // �c�莞�Ԃ��T�b���炷
            remainTime -= 5.0f;
            if(remainTime < 0)
            {
                remainTime = 0;
            }

            // �u�b�u�[�~
            audioSource.PlayOneShot(seBubuu);
            peke.SetActive(true);
            await Task.Delay(1000);

            // �M��̑f�ނƁ�������
            dt.nowSozai[0] = 0;
            dt.nowSozai[1] = 0;
            peke.SetActive(false);

            // �^�b�v�}��������
            dt.isTappable = true;

            // �f�ޑI���t�F�[�Y��
            dt.Phase = 3;
        }
    }

    // �c�莞�Ԃɂ���ă{�[�i�X�|�C���g�����Z
    private async void addBonus()
    {
        // ���ʉ��̎��Ԃ���������Ƒ҂�
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

        // �^�b�v�}������
        dt.isTappable = true;
    }

    // �n�C�X�R�A�`�F�b�N
    private void checkHighScore()
    {
        // ���X�R�A���n�C�X�R�A����������
        if(dt.Score > dt.HighScore)
        {
            // �n�C�X�R�A�X�V
            dt.HighScore = dt.Score;
            txtHighScore.GetComponent<Text>().text = "HighScore:" + dt.HighScore.ToString("D4");

            // �n�C�X�R�A�ۑ�
            PlayerPrefs.SetInt(dt.SAVE_KEY, dt.HighScore);
            PlayerPrefs.Save();
        }
    }

    // �Q�[���I�[�o�[�G�t�F�N�g
    private async void gameOverEfect()
    {
        // ��x�ƋA���Ă��Ȃ��悤�ɃQ�[���t�F�[�Y��99�ɂ���
        dt.Phase = 99;

        // �J�E���g�_�E����~�E�^�b�v�}��
        isCountDown = false;
        dt.isTappable = false;

        // BGM�~�߂ă^�C���A�E�g���ʉ�
        audioSource.Stop();
        audioSource.PlayOneShot(seTimeout);
        await Task.Delay(2700);

        // �Q�[���I�[�o�[��ʂ�
        SceneManager.LoadScene("GameOverScene");
    }
}
