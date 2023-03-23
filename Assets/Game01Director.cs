using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using TMPro;        // TextMeshPro�p�ɕK�v

public class Game01Director : MonoBehaviour
{
    // �ϐ��������
    private int Stage = 0;                  // �X�e�[�W
    private int GuestNum = 0;               // ���q�l�ԍ�
    private int OrderNum;                   // �������ނ��єԍ�
    // private float remainTime = 60.999f;     // �c�莞��
    private float remainTime = 6.999f;     // �c�莞��
    private bool inGame = false;            // �Q�[�����i�J�E���g�_�E������j

    // �摜�֘A
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];
    public Sprite[] Sozai = new Sprite[21];

    // �����֘A
    AudioSource audioSource;
    public AudioClip[] vCd = new AudioClip[3];
    public AudioClip sePinpon;
    public AudioClip seBubuu;
    public AudioClip seStageClear;
    public AudioClip seTimeout;

    // �Q�[���I�u�W�F�N�g
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

    // TextMeshPro�p�����I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI OrderText;

    // Start is called before the first frame update
    void Start()
    {
        // �I�u�W�F�N�g�̎擾
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

        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        // ���炩���ߗ]�v�ȕ\���������Ă���
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

        // �t�F�[�Y�O����J�n
        dt.Phase = 0;

        // �J�E���g�_�E���ƃQ�[������
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�����Ȃ�J�E���g�_�E��
        if(inGame)
        {
            remainTime -= Time.deltaTime;
            txtTime.GetComponent<Text>().text = ((int)remainTime).ToString();
            if((int)remainTime <= 0)
            {
                inGame = false;
                dt.Phase = 8;
            }
        }

        switch(dt.Phase)
        {
            // �Q�[���J�n�̂������̐ݒ�
            case 1:
                // �Q�[���f�ނ̍ĕ\��
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);

                // ���q�l�̃Z�b�g�ƒ����̐ݒ�
                guest.GetComponent<SpriteRenderer>().sprite = Guest[GuestNum];
                OrderNum = Random.Range(1, 34);
                dt.Phase++;

                // �Q�[���J�n�I
                inGame = true;
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
                if(dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]] == OrderNum)
                {
                    correctOmsubi();
                }
                else
                {
                    incorrectOmsubi();
                }
                break;

            // ����҂�
            case 5:
                break;

            // �X�e�[�W�N���A
            case 6:
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
                dt.Phase = 7;
                break;

            // ���X�e�[�W�҂�
            case 7:
                // �^�b�v������
                if (Input.GetMouseButtonDown(0))
                {
                    // ���̃X�e�[�W��
                    SceneManager.LoadScene("GameClearScene");
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

        // BGM�J�n
        audioSource.Play();

        // �Q�[����Ԃ�i�߂�
        dt.Phase++;
    }

    // �����̕\��
    private async void DispOrder()
    {
        // �{�^���Ƃ��ނ��і��Ƒf�ރp�l�����B��
        btnMake.SetActive(false);
        txtOmusubiName.SetActive(false);
        cover.SetActive(true);

        // ��莞�Ԃ��������Z���t�̕\��
        fukidashi.SetActive(true);
        reorder.SetActive(false);
        OrderText.text = dt.guestTalk[Stage, GuestNum, 0] +
            "<u><color=#cc0000>" + dt.Omsubi[OrderNum] + "</color></u>" +
            dt.guestTalk[Stage, GuestNum, 1];
        await Task.Delay(1500);
        fukidashi.SetActive(false);
        reorder.SetActive(true);

        // �{�^���Ƃ��ނ��і��Ƒf�ރp�l����\��
        btnMake.SetActive(true);
        txtOmusubiName.SetActive(true);
        cover.SetActive(false);
    }

    // ���ނ��ѐ���
    private async void correctOmsubi()
    {
        dt.Phase = 5;
        audioSource.PlayOneShot(sePinpon);
        patatan[0].SetActive(true);
        maru.SetActive(true);
        await Task.Delay(500);
        dt.nowSozai[0] = 0;
        dt.nowSozai[1] = 0;
        patatan[0].SetActive(false);
        maru.SetActive(false);
        GuestNum++;
        if(GuestNum < 6)
        {
            dt.Phase = 1;
        }
        else
        {
            dt.Phase = 6;
        }
    }

    // ���ނ��ъԈႢ
    private async void incorrectOmsubi()
    {
        dt.Phase = 5;
        audioSource.PlayOneShot(seBubuu);
        peke.SetActive(true);
        await Task.Delay(1500);
        dt.nowSozai[0] = 0;
        dt.nowSozai[1] = 0;
        peke.SetActive(false);
        dt.Phase = 3;
    }

    // �Q�[���I�[�o�[�G�t�F�N�g
    private async void gameOverEfect()
    {
        // ��x�ƋA���Ă��Ȃ��悤�ɃQ�[���t�F�[�Y��99�ɂ���
        dt.Phase = 99;

        // BGM�~�߂ă^�C���A�E�g���ʉ�
        audioSource.Stop();
        audioSource.PlayOneShot(seTimeout);
        await Task.Delay(2700);

        // �Q�[���I�[�o�[��ʂ�
        SceneManager.LoadScene("GameOverScene");
    }
}
