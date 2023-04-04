using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnExplainCtrl : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        // ゲーム説明画面へ
        SceneManager.LoadScene("GameExplainScene");
    }
}
