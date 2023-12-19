using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void StartLevel(string Character)
    {
        SceneManager.LoadScene("Level");
        GameManager.character = Character;
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene("pick character");
    }
    public void creddits()
    {
        SceneManager.LoadScene("creddits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
