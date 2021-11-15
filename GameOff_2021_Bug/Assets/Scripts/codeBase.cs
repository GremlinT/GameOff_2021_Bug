using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codeBase : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    List<Vector2> movePoints;
    [SerializeField]
    Vector2 startPoint;

    private int nextPoint;
    private Transform tr;
    private float minDist;
    
    public int force;

    private void Awake()
    {
        nextPoint = 0;
        tr = transform;
        minDist = 0.001f;
        tr.position = startPoint;
    }
    private void MoveToPoint()
    {
        Debug.Log("lets go!");
        Vector2 trPos = tr.position;
        Vector2 direction = movePoints[nextPoint] - trPos;
        if(direction.sqrMagnitude > minDist)
        {
            Debug.Log("im going to point " + nextPoint);
            trPos += direction.normalized * speed * Time.deltaTime;
            transform.position = trPos;
        }
        else
        {
            Debug.Log("at point " + nextPoint);
            if (nextPoint < movePoints.Count - 1)
            {
                nextPoint += 1;
                Debug.Log("my next point " + nextPoint);
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
        Debug.Log("enter programm");
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
