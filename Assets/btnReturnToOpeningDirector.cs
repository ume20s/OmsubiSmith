using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnReturnToOpeningDirector : MonoBehaviour
{
    // �^�b�v������
    public void onClick()
    {
        // �I�[�v�j���O��ʂɖ߂�
        SceneManager.LoadScene("Game00Scene");
    }
}
