using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openingDirector : MonoBehaviour
{
    // タップしたら
    public void onClick()
    {
        SceneManager.LoadScene("Game01Scene");
    }
}
