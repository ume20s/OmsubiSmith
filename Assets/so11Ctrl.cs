using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so11Ctrl : MonoBehaviour
{
    // �^�b�v������
    public void onClick()
    {
        // �������̂��f�ގM�ɏ���Ă��Ȃ�������
        if(dt.nowSozai[0] != 11 && dt.nowSozai[1] != 11) {
            // �����󂢂Ă����獶�ɃZ�b�g
            if(dt.nowSozai[0] == 0) {
                dt.nowSozai[0] = 11;
            } else {
                // �E���J���Ă�����E�ɃZ�b�g
                if (dt.nowSozai[1] == 0) {
                    dt.nowSozai[1] = 11;
                }
            }
        }
    }
}
