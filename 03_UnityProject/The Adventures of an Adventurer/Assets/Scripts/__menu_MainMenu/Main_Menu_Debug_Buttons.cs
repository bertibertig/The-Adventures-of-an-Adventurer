using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Debug_Buttons : MonoBehaviour {

    public void LoadLevel0()
    {
        SceneManager.LoadScene("0_Level_Tutorial");
    }

    public void LoadTestGround()
    {
        SceneManager.LoadScene("TestGround");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
