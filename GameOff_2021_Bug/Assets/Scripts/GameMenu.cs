using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject rulesField;
    public void NewGame()
    {
        SceneManager.LoadScene("TesterMainScene");
    }
    //later maybe
    /*public void Records()
    {
        SceneManager.LoadScene("Records");
    }*/

    public void Rules()
    {
        rulesField.SetActive(true);
    }
    public void RulesClose()
    {
        rulesField.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
