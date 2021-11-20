using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codeBase : MonoBehaviour
{
    [SerializeField]
    float speed;
    
    public List<Vector2> movePoints;
    
    public Vector2 startPoint;

    private int nextPoint;
    private Transform tr;
    private float minDist;
    [SerializeField]    
    int defaultForce;
    public int force;

    private int bugForce;
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
        nextPoint = 0;
        tr = transform;
        minDist = 0.001f;
    }
    private void Start()
    {
        tr.position = startPoint;
        CodeGenerate(forceModif);
        speed = Random.Range(1, 4);
    }

    [SerializeField]
    int bigForce;

    private void SmallClearCode()
    {
        SR.sprite = sprites[0];
        var part = PS.main;
        part.startColor = Color.green;
    }
    private void SmallBadCode()
    {
        SR.sprite = sprites[2];
        var part = PS.main;
        part.startColor = Color.red;
    }
    private void SmallVBadCode()
    {
        SR.sprite = sprites[3];
        var part = PS.main;
        part.startColor = Color.black;
    }
    private void BigCleanCode()
    {
        SR.sprite = sprites[1];
        var part = PS.main;
        part.startColor = Color.green;
        part.startSpeed = 1;
        Col.size = new Vector2(1, 1);
    }
    private void BigBadCode()
    {
        SR.sprite = sprites[4];
        var part = PS.main;
        part.startColor = Color.red;
        part.startSpeed = 1;
        Col.size = new Vector2(1, 1);
    }
    private void BigVBadCode()
    {
        SR.sprite = sprites[5];
        var part = PS.main;
        part.startColor = Color.black;
        part.startSpeed = 1;
        Col.size = new Vector2(1, 1);
    }
    public void CodeGenerate(int forceModif)
    {
        int random = Random.Range(1, 11);
        if (random < 8) force = defaultForce * forceModif;
        else force = defaultForce * forceModif * bigForce;
        if (random < 5) bugForce = 0;
        else if (random < 8) bugForce = force;
        else if (random > 8) bugForce = force * force;
        if (force == defaultForce * forceModif && bugForce == 0) SmallClearCode();//чистый маленький код
        if (force > defaultForce * forceModif && bugForce == 0) BigCleanCode(); //чистый большой код
        if (force == defaultForce * forceModif && bugForce > 0 && bugForce <= force) SmallBadCode();//маленький код с маленьким багом
        if (force == defaultForce * forceModif && bugForce > force) SmallVBadCode(); //маленький код с большим багом
        if (force > defaultForce * forceModif && bugForce >0 && bugForce <= force) BigBadCode(); //большой код с маленьким багом
        if (force > defaultForce * forceModif && bugForce > force) BigVBadCode(); //большой код с большим багом
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
        Debug.Log("click");
    }
    
    public void DestroyOnEnterProgramm()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DestroyOnClick();
    }
    private void FixedUpdate()
    {
        MoveToPoint();
    }

 

}
