using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnPress : MonoBehaviour
{

    public void btn_start()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }
    public void btn_exit()
    {
        Application.Quit();
    }

    public void btn_settings()
    {
        SceneManager.LoadScene("SettingsScreen");
    }

    public void btn_back()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void btn_rules()
    {
        SceneManager.LoadScene("RulesScreen");
    }
}
