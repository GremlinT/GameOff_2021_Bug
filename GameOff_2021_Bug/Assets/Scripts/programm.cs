using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class programm : MonoBehaviour
{
    int currentReady;
    int finalReady;

    int currentBugs;
    int maxBugs;

    int version;
    [SerializeField]
    Text versionText;
    bool newVersion;

    [SerializeField]
    GameObject programmer;
    
    [SerializeField]
    Vector2[] programmerPositions = new Vector2[8];
    int currentProgrammerPosition;
    
    [SerializeField]
    SpriteRenderer[] finSprites = new SpriteRenderer[10];
    [SerializeField]
    SpriteRenderer[] bugSprites = new SpriteRenderer[10];

    [SerializeField]
    int riseOfHardnes;
    
    public bool allStop;
    public bool allDie;
    public bool allSlow;
    
    [SerializeField]
    float slowTime;
    float slowTimer;
    [SerializeField]
    float slowRepeatTime, slowRepeatTimer;
    bool alreadySlow;

    [SerializeField]
    float energoTime;
    float energoTimer;
    [SerializeField]
    float dieTime;
    float dieTimer;
    bool alreadyAllDie;

    [SerializeField]
    Text slowButtonText;
    [SerializeField]
    Text energoButtonText;

    bool energoButtonClick;
    bool slowButtonClick;

    public int score;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text readyText, bugedText;

    int percentReady, percentBuggy;

    [SerializeField]
    GameObject gameOver;
        
    public GameObject pause;

    public void EnergoButtonClick()
    {
        if (!gameOver.activeSelf)
            if (!pause.activeSelf) energoButtonClick = true;
    }

    private void AllDie()
    {
        energoButtonText.text = energoTimer.ToString("0.0");
        if (!alreadyAllDie)
        {
            DeleteAllFromScene();
            alreadyAllDie = true;
        }
        dieTimer -= Time.deltaTime;
        if (dieTimer < 0 && allDie) allDie = false;
        EnergoButtonTime();
    }

    private void EnergoButtonTime()
    {
        energoTimer -= Time.deltaTime;
        if (energoTimer < 0)
        {
            energoTimer = energoTime;
            alreadyAllDie = false;
            energoButtonClick = false;
            dieTimer = dieTime;
            energoButtonText.text = "";
        }
    }
    private void DeleteAllFromScene()
    {
        allDie = true;
    }
    
      
    public void SlowButtonClick()
    {
        if (!gameOver.activeSelf)
            if (!pause.activeSelf) slowButtonClick = true;
    }
    private void AllSlow()
    {
        slowButtonText.text = slowRepeatTimer.ToString("0.0");
        slowTimer -= Time.deltaTime;
        if (slowTimer > 0)
        {
            if (!allSlow) allSlow = true;
        }
        else
        {
            if (allSlow) allSlow = false;
        }
        SlowButtonTime();
    }
    
    private void SlowButtonTime()
    {
        slowRepeatTimer -= Time.deltaTime;
        if (slowRepeatTimer < 0)
        {
            slowTimer = slowTime;
            slowRepeatTimer = slowRepeatTime;
            slowButtonClick = false;
            slowButtonText.text = "";
        }
    }

    private void Awake()
    {
        score = 0;
        version = 1;
        finalReady = 100;
        maxBugs = 100;
        currentReady = 0;
        currentBugs = 0;
        currentProgrammerPosition = 0;
        versionText.text = "V." + version;
        percentReady = 0;
        percentBuggy = 0;
        slowTimer = slowTime;
        energoTimer = energoTime;
        readyText.text = "R: " + currentReady + "/" + finalReady;
        bugedText.text = "B: " + currentBugs + "/" + maxBugs;
        alreadyAllDie = false;
        dieTimer = dieTime;
        slowRepeatTimer = slowRepeatTime;
    }
    
    private void Update()
    {
        if (slowButtonClick) AllSlow();
        if (energoButtonClick) AllDie();
        PercentInfoOnScreen(percentReady, finSprites);
        PercentInfoOnScreen(percentBuggy, bugSprites);
        if (newVersion)
        {
            PercentOnScreenOff();
            newVersion = false;
        }
        scoreText.text = "Score: " + score.ToString();
        if (Input.GetKey("escape"))
        {
            Pause();
        }
    }
    
    
    private void PercentInfoOnScreen(int percent, SpriteRenderer[] SR)
    {
        if (percent > 10) SR[0].enabled = true;
        if (percent > 20) SR[1].enabled = true;
        if (percent > 30) SR[2].enabled = true;
        if (percent > 40) SR[3].enabled = true;
        if (percent > 50) SR[4].enabled = true;
        if (percent > 60) SR[5].enabled = true;
        if (percent > 70) SR[6].enabled = true;
        if (percent > 80) SR[7].enabled = true;
        if (percent > 90) SR[8].enabled = true;
        if (percent > 95) SR[9].enabled = true;
    }
    private void PercentOnScreenOff()
    {
        for (int i = 0; i < finSprites.Length; i++)
        {
            finSprites[i].enabled = false;
            bugSprites[i].enabled = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        codeBase cb = col.gameObject.GetComponent<codeBase>();
        if (cb != null)
        {
            currentReady += cb.force;
            currentBugs += cb.bugForce;
            percentReady = currentReady * 100 / finalReady;
            percentBuggy = currentBugs * 100 / maxBugs;
            readyText.text = "R: " + currentReady + "/" + finalReady;
            bugedText.text = "B: " + currentBugs + "/" + maxBugs;
            cb.DestroyOnEnterProgramm();
            if (currentBugs > maxBugs)
            {
                GameOver();
            }
            if (currentReady >= finalReady)
            {
                newVersion = true;
                NewVersion();
                currentReady = 0;
                percentReady = 0;
                currentBugs = 0;
                percentBuggy = 0;
                readyText.text = "R: " + currentReady + "/" + finalReady;
                bugedText.text = "B: " + currentBugs + "/" + maxBugs;
            }
        }
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        allStop = true;
        allDie = true;
    }

    public void Pause()
    {
        pause.SetActive(true);
        allStop = true;
    }
    public void EndPause()
    {
        pause.SetActive(false);
        allStop = false;
    }
    public void Exit()
    {
        SceneManager.LoadScene("TesterMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("TesterMainScene");
    }
    //later maybe
    /*public void Records()
    {
        SceneManager.LoadScene("Records");
    }*/
    private void NewVersion()
    {
        version += 1;
        finalReady = finalReady + riseOfHardnes;
        maxBugs = maxBugs + riseOfHardnes;
        versionText.text = "V." + version;
        if (currentProgrammerPosition < programmerPositions.Length) AddProgrammer();
        
    }

    private void AddProgrammer()
    {
        GameObject progr = Instantiate(programmer);
        programmer pro = progr.GetComponent<programmer>();
        pro.programmerPosition = programmerPositions[currentProgrammerPosition];
        currentProgrammerPosition += 1;
    }
}
