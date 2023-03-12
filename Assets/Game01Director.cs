using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Game01Director : MonoBehaviour
{
    // �������̕ϐ�
    float countdown = 3.99f;         // �J�E���g�_�E���v��

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


    // Start is called before the first frame update
    void Start()
    {
        // �I�u�W�F�N�g�̎擾
        guest = GameObject.Find("guest");
        txtTime = GameObject.Find("txtTime");
        txtStage = GameObject.Find("txtStage");
        txtOrder = GameObject.Find("txtOrder");
        fukidashi = GameObject.Find("fukidashi");

        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        // ���炩���ߗ]�v�ȕ\���������Ă���
        txtStage.SetActive(false);
        txtOrder.SetActive(false);
        fukidashi.SetActive(false);



        // �J�E���g�_�E��
        CountDown();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private async void CountDown()
    {
        // �����̃R���|�[�l���g���擾
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
