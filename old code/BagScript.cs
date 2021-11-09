using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    private MainGameLogicScript MGLS;
    public float bagingProecnt = 10;

    private void Start()
    {
        MGLS = GameObject.FindGameObjectWithTag("MGLS").GetComponent<MainGameLogicScript>();
        bagingProecnt = bagingProecnt / MGLS.LevelNomber;
    }

    private void OnMouseDown()
    {
        MGLS.Score++;
        Destroy(gameObject);
    }
}
