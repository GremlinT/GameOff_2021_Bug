using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMoveScript : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector3 CurrWayP;
    public Vector3 NextWayP;
    public Vector3 PrivWayP;

    public float CodingPerscent = 1;

    private Vector3 WayToGo;
    public int CodeSpeed = 2;

    private WayPointScript WPScript;
    private MainGameLogicScript MGLS;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MGLS = GameObject.FindGameObjectWithTag("MGLS").GetComponent<MainGameLogicScript>();
        CodeSpeed = CodeSpeed + MGLS.LevelNomber;
        if (CodeSpeed > 11) CodeSpeed = 11;
        switch (MGLS.LevelNomber)
        {
            case 2:
                CodingPerscent -= 0.1f;
                break;
            case 3:
                CodingPerscent -= 0.2f;
                break;
            case 4:
                CodingPerscent -= 0.3f;
                break;
            case 5:
                CodingPerscent -= 0.4f;
                break;
            case 6:
                CodingPerscent -= 0.5f;
                break;
            case 7:
                CodingPerscent -= 0.6f;
                break;
            case 8:
                CodingPerscent -= 0.7f;
                break;
            case 9:
                CodingPerscent -= 0.8f;
                break;
            case 10:
                CodingPerscent -= 0.9f;
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        if (MGLS.gameOver != true)
        {
            if (Vector3.Distance(transform.position, CurrWayP) <= 0.1f)
            {
                PrivWayP = CurrWayP;
                CurrWayP = NextWayP;
                WayToGo = CurrWayP - transform.position;
                if (CurrWayP.x-PrivWayP.x > 0) rb.rotation = -90f;
                if (CurrWayP.x - PrivWayP.x < 0) rb.rotation = 90f;
                if (CurrWayP.y - PrivWayP.y > 0) rb.rotation = 0f;
                if (CurrWayP.y - PrivWayP.y < 0) rb.rotation = 180f;
            }
            rb.velocity = WayToGo.normalized * CodeSpeed;  
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D cross)
    {
        if (cross.GetComponent<WayPointScript>())
        {
            WPScript = cross.GetComponent<WayPointScript>();
            ChooseWay(WPScript.NextWP1, WPScript.NextWP2);
        }
    }

    private void ChooseWay(Vector3 NextWP1, Vector3 NextWP2)
    {
        int SwitchNomber = Random.Range(1, 3);

        switch (SwitchNomber)
        {
            case 1:
                if (NextWP1 != PrivWayP)
                {
                    NextWayP = NextWP1;
                }
                else goto case 2;
                break;
            case 2:
                if (NextWP2 != PrivWayP)
                {
                    NextWayP = NextWP2;
                }
                else goto case 1;
                break;
        }
    }
}
