using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnExplainCtrl : MonoBehaviour
{
    // �^�b�v������
    public void onClick()
    {
        // �Q�[��������ʂ�
        SceneManager.LoadScene("GameExplainScene");
    }
}
