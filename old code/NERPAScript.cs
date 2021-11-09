using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NERPAScript : MonoBehaviour
{
    public MainGameLogicScript MGLS;
   
    private float baginProscent;
    private float codingPerscent;

    private void OnTriggerEnter2D(Collider2D code)
    {
        if (MGLS.gameOver != true)
        {
            codingPerscent = code.GetComponent<CodeMoveScript>().CodingPerscent;
            MGLS.CodingProcecs += codingPerscent;
            if (code.GetComponent<BagScript>())
            {
                baginProscent = code.GetComponent<BagScript>().bagingProecnt;
                MGLS.AntiScore += baginProscent;
            }
        }
        Destroy(code.gameObject);
    }
}
