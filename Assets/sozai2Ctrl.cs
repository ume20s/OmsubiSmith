using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sozai2Ctrl : MonoBehaviour
{
    // �����֘A
    AudioSource audioSource;
    public AudioClip seResetSozai;

    // Start is called before the first frame update
    void Start()
    {
        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
    }

    // �^�b�v������
    public void onClick()
    {
        // �^�b�v�\��������
        if(dt.isTappable)
        {
            // �f�ނ��N���A
            dt.nowSozai[1] = 0;
            audioSource.PlayOneShot(seResetSozai);
        }
    }
}
