using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool paused;
    public static string character;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("pick character");
        //game over code
    }
}
