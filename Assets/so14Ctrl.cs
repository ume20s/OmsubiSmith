using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class so14Ctrl : MonoBehaviour
{
    // �^�b�v������
    public void onClick()
    {
        // �������̂��f�ގM�ɏ���Ă��Ȃ�������
        if(dt.nowSozai[0] != 14 && dt.nowSozai[1] != 14) {
            // �����󂢂Ă����獶�ɃZ�b�g
            if(dt.nowSozai[0] == 0) {
                dt.nowSozai[0] = 14;
            } else {
                // �E���J���Ă�����E�ɃZ�b�g
                if (dt.nowSozai[1] == 0) {
                    dt.nowSozai[1] = 14;
                }
            }
        }
    }
}