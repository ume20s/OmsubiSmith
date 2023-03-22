using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearDirector : MonoBehaviour
{
    // BGM�֘A
    AudioSource audioSource;
    public AudioClip vGameClear;

    // Start is called before the first frame update
    void Start()
    {
        // �����̃R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = vGameClear;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Game00Scene");
        }
    }
}