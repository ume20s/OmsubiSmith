using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class explainDirector : MonoBehaviour
{
    // �����p�l���T����
    public Sprite[] Exp = new Sprite[5];
    
    // �����p�l���I�u�W�F�N�g
    GameObject explain;

    // �p�l���i���o�[
    private int panel;

    // Start is called before the first frame update
    void Start()
    {
        // �p�l���i���o�[
        panel = 0;

        // �I�u�W�F�N�g�̎擾
        explain = GameObject.Find("explain");
    }

    // �^�b�v������
    public void onClick()
    {
        panel++;
        if(panel < 5)
        {
            explain.GetComponent<SpriteRenderer>().sprite = Exp[panel];
        }
        else
        {
            // �Ō�̃p�l���܂ł�����I�[�v�j���O��ʂ�
            SceneManager.LoadScene("Game00Scene");
        }
    }
}
