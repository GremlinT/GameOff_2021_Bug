using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class programm : MonoBehaviour
{
    private int currentReady;
    private int finalReady;
    private int version;
    [SerializeField]
    private GameObject programmer;
    [SerializeField]
    private Vector2[] programmerPositions = new Vector2[8];
    private int currentProgrammerPosition;
    [SerializeField]
    Text versionText;

    private void Awake()
    {
        version = 1;
        finalReady = 100;
        currentReady = 0;
        currentProgrammerPosition = 0;
        versionText.text = "V." + version;
        percentReady = 0;
        percentBuggy = 0;
    }

    int percentReady, percentBuggy;
    private void OnCollisionEnter2D(Collision2D col)
    {
        codeBase cb = col.gameObject.GetComponent<codeBase>();
        if (cb != null)
        {
            currentReady += cb.force;
            cb.DestroyOnEnterProgramm();
            if (currentReady >= finalReady)
            {
                NewVersion();
                currentReady = 0;
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
