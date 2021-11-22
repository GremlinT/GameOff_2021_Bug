using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class programm : MonoBehaviour
{
    private int currentReady;
    private int finalReady;
    int currentBugs;
    private int maxBugs;
    private int version;
    [SerializeField]
    private GameObject programmer;
    [SerializeField]
    private Vector2[] programmerPositions = new Vector2[8];
    private int currentProgrammerPosition;
    [SerializeField]
    Text versionText;
    [SerializeField]
    SpriteRenderer[] finSprites = new SpriteRenderer[10];
    [SerializeField]
    SpriteRenderer[] bugSprites = new SpriteRenderer[10];

    public bool allStop;
    public bool allDie;
    public bool allSlow;
    public bool gameOver;
    private void Awake()
    {
        version = 1;
        finalReady = 100;
        maxBugs = 100;
        currentReady = 0;
        currentBugs = 0;
        currentProgrammerPosition = 0;
        versionText.text = "V." + version;
        percentReady = 0;
        percentBuggy = 0;
    }

    int percentReady, percentBuggy;
    
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
            PercentInfoOnScreen(percentReady, finSprites);
            percentBuggy = currentBugs * 100 / maxBugs;
            PercentInfoOnScreen(percentBuggy, bugSprites);
            Debug.Log("Ready % = " + percentReady + ". Bug % = " + percentBuggy);
            cb.DestroyOnEnterProgramm();
            if (currentBugs > maxBugs)
            {
                Debug.Log("AAAAA!!!");
                allStop = true;
                allDie = true;
            }
            if (currentReady >= finalReady)
            {
                NewVersion();
                currentReady = 0;
                currentBugs = 0;
                PercentOnScreenOff();
            }
        }
    }

    private void NewVersion()
    {
        version += 1;
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
