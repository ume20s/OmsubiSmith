using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so04Ctrl : MonoBehaviour
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
            if (dt.nowSozai[0] != 4 && dt.nowSozai[1] != 4)
            {
                // �����󂢂Ă����獶�ɃZ�b�g
                if (dt.nowSozai[0] == 0)
                {
                    dt.nowSozai[0] = 4;
                    audioSource.PlayOneShot(seSetSozai);
                }
                else
                {
                    // �E���J���Ă�����E�ɃZ�b�g
                    if (dt.nowSozai[1] == 0)
                    {
                        dt.nowSozai[1] = 4;
                        audioSource.PlayOneShot(seSetSozai);
                    }
                }
            }
        }
    }
}
