﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnPress : MonoBehaviour
{

    public void btn_start()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void btn_exit()
    {
        Application.Quit();
    }

    public void btn_settings()
    {
        
    }
}