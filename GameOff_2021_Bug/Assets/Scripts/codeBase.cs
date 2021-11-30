using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codeBase : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    programm PR;
    
    public List<Vector2> movePoints;
    public Vector2 startPoint;
    
    private int nextPoint;
    
    private Transform tr;
    private float minDist;
    
    [SerializeField]    
    int defaultForce;
    [SerializeField]
    int bigForce;
    
    public int force;
    public int bugForce;
    
    [SerializeField]
    Sprite[] sprites = new Sprite[6];
    
    [SerializeField]
    SpriteRenderer SR;
    [SerializeField]
    ParticleSystem PS;
    [SerializeField]
    BoxCollider2D Col;

    public int forceModif;
    
    private void Awake()
    {
        PR = FindObjectOfType<programm>();
        nextPoint = 0;
        tr = transform;
        minDist = 0.001f;
    }
    private void Start()
    {
        tr.position = startPoint;
        CodeGenerate(forceModif);
        speed = Random.Range(2, 4);
        realSpeed = speed;
    }
    
    private void SmallClearCode()
    {
        SR.sprite = sprites[0];
        var part = PS.main;
        part.startColor = Color.green;
        variant = 1;
    }
    private void SmallBadCode()
    {
        SR.sprite = sprites[2];
        var part = PS.main;
        part.startColor = Color.red;
        variant = 2;
    }
    private void SmallVBadCode()
    {
        SR.sprite = sprites[3];
        var part = PS.main;
        part.startColor = Color.black;
        variant = 3;
    }
    private void BigCleanCode()
    {
        SR.sprite = sprites[1];
        var part = PS.main;
        part.startColor = Color.green;
        part.startSpeed = 1;
        Col.size = new Vector2(1, 1);
        variant = 4;
    }
    private void BigBadCode()
    {
        SR.sprite = sprites[4];
        var part = PS.main;
        part.startColor = Color.red;
        part.startSpeed = 1;
        Col.size = new Vector2(1, 1);
        variant = 5;
    }
    private void BigVBadCode()
    {
        SR.sprite = sprites[5];
        var part = PS.main;
        part.startColor = Color.black;
        part.startSpeed = 1;
        Col.size = new Vector2(1, 1);
        variant = 6;
    }
    private int variant;
    public void CodeGenerate(int forceModif)
    {
        int randomSize = Random.Range(1, 6);
        bool small = false;
        bool big = false;
        bool clear = false;
        bool bad = false;
        bool vBad = false;
        if (randomSize < 4)
        {
            small = true;
            force = defaultForce + forceModif; //9 + 1 = 10
        }
        else
        {
            big = true;
            force = (defaultForce + forceModif) * bigForce; //9+1*2 = 20
        }
        int randomBForce = Random.Range(1, 7);
        if (randomBForce <= 2)
        {
            clear = true;
            bugForce = 0;
        }
        else if (randomBForce <= 5)
        {
            bad = true;
            bugForce = force + force / 2; //10 + 5 = 15; 20+10 = 30
        }
        else if (randomBForce > 5)
        {
            vBad = true;
            bugForce = force + (int)((float)force * 1.5f); //10+15=25; 20+30= 50
        }
        if (small)
        {
            if (clear) SmallClearCode();
            if (bad) SmallBadCode();
            if (vBad) SmallVBadCode();
        }
        if (big)
        {
            if (clear) BigCleanCode();
            if (bad) BigBadCode();
            if (vBad) BigVBadCode();
        }
    }
    private void MoveToPoint()
    {
        Vector2 trPos = tr.position;
        Vector2 direction = movePoints[nextPoint] - trPos;
        if(direction.sqrMagnitude > minDist)
        {
            trPos += direction.normalized * speed * Time.deltaTime;
            transform.position = trPos;
        }
        else
        {
            if (nextPoint < movePoints.Count - 1)
            {
                nextPoint += 1;
            }
            else
            {
                Debug.Log("finish");
            }
        }
    }

    private void DestroyOnClick()
    {
        Destroy(gameObject);
        switch (variant)
        {
            case 1:
                PR.score -= 1;
                break;
            case 2:
                PR.score += 1;
                break;
            case 3:
                PR.score += 3;
                break;
            case 4:
                PR.score -= 2;
                break;
            case 5:
                PR.score += 2;
                break;
            case 6:
                PR.score += 4;
                break;
            default:
                break;
        }
    }
    
    public void DestroyOnEnterProgramm()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (!PR.pause.activeSelf) DestroyOnClick();
    }

    float realSpeed;
    
    private void SlowMovement()
    {
       speed = 0.2f;
    }
    private void FixedUpdate()
    {
        if (PR.allDie) Destroy(gameObject);
        if (!PR.allStop) MoveToPoint();
        if (PR.allSlow) SlowMovement(); else speed = realSpeed;
         
    }

  
 

}
