using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnReturnToOpeningDirector : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        // オープニング画面に戻る
        SceneManager.LoadScene("Game00Scene");
    }
}
