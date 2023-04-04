using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game00Director : MonoBehaviour
{
    // BGM�֘A
    AudioSource audioSource;
    public AudioClip[] sOpening = new AudioClip[2];

    // Start is called before the first frame update
    void Start()
    {
        // ���ϐ��̏�����
        dt.nowSozai[0] = 0;
        dt.nowSozai[1] = 0;
        dt.Phase = 0;
        dt.Score = 0;

        // �f�o�b�O�p�n�C�X�R�A���[���N���A
        // dt.HighScore = 0;
        // PlayerPrefs.SetInt(dt.SAVE_KEY, dt.HighScore);
        // PlayerPrefs.Save();

        // �n�C�X�R�A�ǂݍ���
        dt.HighScore = PlayerPrefs.GetInt(dt.SAVE_KEY, 0);

        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        // �����_����BGM��I��
        int song = Random.Range(0, 5);
        switch (song)
        {
            case 0:
                audioSource.clip = sOpening[0];
                break;
            default:
                audioSource.clip = sOpening[1];
                break;
        }
        audioSource.Play();
    }
}
