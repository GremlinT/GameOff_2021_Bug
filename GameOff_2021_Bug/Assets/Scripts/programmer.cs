using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class programmer : MonoBehaviour
{
    
    public Vector2 programmerPosition;
    [SerializeField]
    Vector3[] pathPoints;
    [SerializeField]
    int programmerSpeed;
    [SerializeField]
    int programmerEffectivnes;
    [SerializeField]
    GameObject codePrefab;
    [SerializeField]
    LineRenderer LR;
    bool moreThanZero;
    [SerializeField]
    int maxYpoint, minYpoint;

    private void Start()
    {
        pathPoints = new Vector3[6];
        transform.position = programmerPosition;
        if (programmerPosition.x > 0) moreThanZero = true;
        else moreThanZero = false;
        pathPoints[0] = programmerPosition;
        if (moreThanZero) pathPoints[1] = new Vector2(programmerPosition.x - 1, programmerPosition.y);
        else pathPoints[1] = new Vector2(programmerPosition.x + 1, programmerPosition.y);
        PathGenerate();
    }

    bool pointYrise = false;
    private int YpathRise(int pointY)
    {
        if (pointY < maxYpoint)
        {
            pointY += Random.Range(1, 2);
            pointYrise = true;
        }
        else
        {
            pointY -= Random.Range(1, 2);
            pointYrise = false;
        }
        return pointY;
    }
    private int YpathDown(int pointY)
    {
        if (pointY > minYpoint)
        {
            pointY -= Random.Range(1, 2);
            pointYrise = false;
        }
        else
        {
            pointY += Random.Range(1, 2);
            pointYrise = true;
        }
        return pointY;
    }
    private void PathGenerate()
    {
        int pointX = (int)pathPoints[1].x;
        int pointY = (int)pathPoints[1].y;
        bool pointXchahged = true;
        
        for (int i = 0; i < pathPoints.Length - 2; i++)
        {
            int changePointX = Random.Range(0, 2);
            if (changePointX == 0)
            {
                if (moreThanZero) pointX -= 1;
                else pointX += 1;
                pointXchahged = true;
            }
            else
            {
                if (pointXchahged)
                {
                    if (Random.value > 0.5f)
                    {
                        pointY = YpathRise(pointY);
                    }
                    else
                    {
                        pointY = YpathDown(pointY);
                    } 
                    pointXchahged = false;
                }
                else
                {
                    if (pointYrise)
                    {
                        if (pointY < maxYpoint)
                        {
                            pointY += Random.Range(1, 2);
                            pointYrise = true;
                        }
                        else
                        {
                            if (moreThanZero) pointX -= 1;
                            else pointX += 1;
                            pointXchahged = true;
                        }
                    }
                    else if (pointY > minYpoint)
                    {
                        pointY -= Random.Range(1,2);
                        pointYrise = false;
                    }
                    else
                    {
                        if (moreThanZero) pointX -= 1;
                        else pointX += 1;
                        pointXchahged = true;
                    }
                }
            }
            Vector2 pointPosition = new Vector2((float)pointX, (float)pointY);
            pathPoints[i + 2] = pointPosition;
        }
        LR.positionCount = 8;
        LR.SetPositions(pathPoints);
        LR.SetPosition(6, new Vector2(0, pathPoints[5].y));
        LR.SetPosition(7, Vector3.zero);
    }
    [SerializeField]
    private float codeTimer = 2;

    void CodeGenerate()
    {
        codeTimer -= Time.deltaTime;
        if (codeTimer <= 0)
        {
            GameObject code = (GameObject) Instantiate(codePrefab);
            codeBase cb = code.GetComponent<codeBase>();
            cb.startPoint = programmerPosition;
            Vector2[] ppV2 = new Vector2[8];
            for (int i = 0; i < LR.positionCount - 2; i++)
            {
                ppV2[i] = LR.GetPosition(i+1);
            }
            cb.movePoints.AddRange(ppV2);
            //характеристики кода
            codeTimer = 5;
        }
    }

    private void Update()
    {
        CodeGenerate();
    }
}
