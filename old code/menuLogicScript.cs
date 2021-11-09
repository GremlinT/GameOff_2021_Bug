using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuLogicScript : MonoBehaviour
{
    public Text rulesText;
    public Image rulesBabl;

    public void PlayFunct()
    {
        SceneManager.LoadScene("gameScene");
    }
    public void gameExit()
    {
        Application.Quit();
    }
    public void gameRules()
    {
        rulesBabl.enabled = true;
        rulesText.enabled = true;
    }
}
