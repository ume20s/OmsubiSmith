using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so13Ctrl : MonoBehaviour
{
    // �����֘A
    AudioSource audioSource;
    public AudioClip seSetSozai;

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
        if (dt.isTappable)
        {
            // �������̂��f�ގM�ɏ���Ă��Ȃ�������
            if (dt.nowSozai[0] != 13 && dt.nowSozai[1] != 13)
            {
                // �����󂢂Ă����獶�ɃZ�b�g
                if (dt.nowSozai[0] == 0)
                {
                    dt.nowSozai[0] = 13;
                    audioSource.PlayOneShot(seSetSozai);
                }
                else
                {
                    // �E���J���Ă�����E�ɃZ�b�g
                    if (dt.nowSozai[1] == 0)
                    {
                        dt.nowSozai[1] = 13;
                        audioSource.PlayOneShot(seSetSozai);
                    }
                }
            }
        }
    }
}
