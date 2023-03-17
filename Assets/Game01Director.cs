using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Game01Director : MonoBehaviour
{
    // �ϐ��������
    private int gamePhase = 0;          // �Q�[����ԑJ��

    // �摜�֘A
    public Sprite[] Cd = new Sprite[3];
    public Sprite[] Guest = new Sprite[6];

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
    GameObject cover;
    GameObject[] patatan = new GameObject[8];
    GameObject[] sozai = new GameObject[2];

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

        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        // ���炩���ߗ]�v�ȕ\���������Ă���
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

        // �J�E���g�_�E���ƃQ�[������
        CountDown();

    }

    // Update is called once per frame
    void Update()
    {
        switch(gamePhase)
        {
            // �Q�[���J�n�̂������̐ݒ�
            case 1:
                // �Q�[���f�ނ̍ĕ\��
                txtStage.SetActive(true);
                txtTime.SetActive(true);
                txtOrder.SetActive(true);
                btnMake.SetActive(true);
                txtOmusubiName.SetActive(true);
                fukidashi.SetActive(true);
                cover.SetActive(false);

                // ���q�l�̃Z�b�g�ƒ����̐ݒ�
                guest.GetComponent<SpriteRenderer>().sprite = Guest[0];

                break;


        }
    }

    private async void CountDown()
    {
        // �J�E���g�_�E���I
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
        gamePhase++;
    }
}
