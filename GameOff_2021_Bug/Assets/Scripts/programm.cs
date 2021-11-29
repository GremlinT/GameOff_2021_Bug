using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int percentReady, percentBuggy;

    public void EnergoButtonClick()
    {
        energoButtonClick = true;
    }

    private void AllDie()
    {
        energoButtonText.text = energoTimer.ToString("0.0");
        dieTimer -= Time.deltaTime;
        if (!alreadyAllDie)
        {
            allDie = true;
            alreadyAllDie = true;
        }
        else if (dieTimer < 0 && allDie)
        {
            allDie = false;
        }
            
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
    
    public void SlowButtonClick()
    {
        slowButtonClick = true;
    }
    private void AllSlow()
    {
        slowButtonText.text = slowTimer.ToString("0.0");
        if (slowTimer >= slowTime)
        {
            allSlow = true;
        }
        if (slowTimer > 0) slowTimer -= Time.deltaTime;
        if (slowTimer <= 0)
        {
            allSlow = false;
            slowTimer = slowTime;
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
            
            Debug.Log("Ready % = " + percentReady + ". Bug % = " + percentBuggy);
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
                
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("AAAAA!!!");
        allStop = true;
        allDie = true;
    }
    private void NewVersion()
    {
        version += 1;
        finalReady = finalReady + riseOfHardnes;
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
