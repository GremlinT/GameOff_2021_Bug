using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class programm : MonoBehaviour
{
    private int currentReady;
    private int finalReady;
    private int version;

    private void Awake()
    {
        version = 1;
        finalReady = 100;
        currentReady = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("touch");
        codeBase cb = col.gameObject.GetComponent<codeBase>();
        if (cb != null)
        {
            currentReady += cb.force;
            Debug.Log("current status is " + currentReady);
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
        Debug.Log("new version " + version);
    }
}
