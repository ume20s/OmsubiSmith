using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class explainDirector : MonoBehaviour
{
    // 説明パネル５枚分
    public Sprite[] Exp = new Sprite[5];
    
    // 説明パネルオブジェクト
    GameObject explain;

    // パネルナンバー
    private int panel;

    // Start is called before the first frame update
    void Start()
    {
        // パネルナンバー
        panel = 0;

        // オブジェクトの取得
        explain = GameObject.Find("explain");
    }

    // タップしたら
    public void onClick()
    {
        panel++;
        if(panel < 5)
        {
            explain.GetComponent<SpriteRenderer>().sprite = Exp[panel];
        }
        else
        {
            // 最後のパネルまできたらオープニング画面へ
            SceneManager.LoadScene("Game00Scene");
        }
    }
}
