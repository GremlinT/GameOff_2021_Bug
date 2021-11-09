using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgerScript : MonoBehaviour
{
    public float TimeToAppear = 5;
    private float timer = 0;

    public float MBChance = 10;
    public float FBChance = 10;
    public float KBChance = 10;
    private float ClearCodeChanse = 0;
    private GameObject[] ListOfBag;
    private float[] ChanseOfBag;

    public bool ThreeWayProger = false;

    public Vector3 CWP;
    public Vector3 NWP;
    public Vector3 NWP0;
    public Vector3 NWP1;
    public Vector3 NWP2;
    
    public GameObject Mbag;
    public GameObject Fbag;
    public GameObject Kbag;
    public GameObject code;

    public MainGameLogicScript MGLS;
    
    void Start()
    {
        timer = TimeToAppear;
        ClearCodeChanse = 100 - (MBChance + FBChance + KBChance);
        ListOfBag = new GameObject[] { Mbag, Fbag, Kbag, code};
        ChanseOfBag = new float[] { MBChance, FBChance, KBChance, ClearCodeChanse};
        for (int i = 1; i < ChanseOfBag.Length; i++)
        {
            ChanseOfBag[i] = ChanseOfBag[i] + ChanseOfBag[i - 1];
        }
    }

    void FixedUpdate()
    {
        if (MGLS.gameOver != true)
        {
        timer -= Time.deltaTime;
            if (timer <= 0)
            {
                float r = Random.Range(1,100);
                for (int i = 0; i < ChanseOfBag.Length; i++)
                {
                    if (r < ChanseOfBag[i])
                    {
                        GameObject result = Instantiate(ListOfBag[i], transform.position, Quaternion.identity);
                        if (ThreeWayProger == true) ChooseNWP();
                        result.GetComponent<CodeMoveScript>().CurrWayP = CWP;
                        result.GetComponent<CodeMoveScript>().NextWayP = NWP;
                        break;
                    }
                }
                timer = TimeToAppear;
            }
        }
        
    }

    void ChooseNWP()
    {
        int WayToGo = Random.Range(1, 4);
        switch (WayToGo)
        {
            case 1:
                NWP = NWP0;
                break;
            case 2:
                NWP = NWP1;
                break;
            case 3:
                NWP = NWP2;
                break;
        }
    }
}
