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

    // �Q�[����ԑJ�ځi�O���������j
    static public int Phase = 0;

    // �摜�֘A
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];
    public Sprite[] Sozai = new Sprite[21];

    // �����֘A
    AudioSource audioSource;
    public AudioClip[] vCd = new AudioClip[3];

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
        btnMake = GameObject.Find("btnMake");
        fukidashi = GameObject.Find("fukidashi");
        reorder = GameObject.Find("reorder");
        cover = GameObject.Find("so00_cover");
        maru = GameObject.Find("maru");
        peke = GameObject.Find("peke");
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
        fukidashi.SetActive(false);
        reorder.SetActive(false);
        for (int i = 0; i<8; i++)
        {
            patatan[i].SetActive(false);
        }

        // �J�E���g�_�E���ƃQ�[������
        CountDown();

    }

    // Update is called once per frame
    void Update()
    {
        switch(Phase)
        {
            // �Q�[���J�n�̂������̐ݒ�
            case 1:
                // BGM�J�n
                audioSource.Play();

                // �Q�[���f�ނ̍ĕ\��
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);
                txtOmusubiName.SetActive(true);

                // ���q�l�̃Z�b�g�ƒ����̐ݒ�
                guest.GetComponent<SpriteRenderer>().sprite = Guest[GuestNum];
                OrderNum = Random.Range(1, 34);
                Phase++;
                break;

            case 2:
                // �����̕\��
                DispOrder();
                Phase++;
                break;

            case 3:
                makesozai[0].GetComponent<SpriteRenderer>().sprite = Sozai[dt.nowSozai[0]];
                makesozai[1].GetComponent<SpriteRenderer>().sprite = Sozai[dt.nowSozai[1]];
                txtOmusubiName.GetComponent<Text>().text = dt.Omsubi[dt.makeOmsubi[dt.nowSozai[0], dt.nowSozai[1]]];
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

        // �Q�[����Ԃ�i�߂�
        Phase++;
    }

    // �����̕\��
    private async void DispOrder()
    {
        // �{�^���Ƒf�ރp�l�����B��
        btnMake.SetActive(false);
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

        // �{�^���Ƒf�ރp�l����\��
        btnMake.SetActive(true);
        cover.SetActive(false);
    }
}
