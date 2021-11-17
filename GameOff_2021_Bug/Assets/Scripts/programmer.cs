using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class programmer : MonoBehaviour
{
    [SerializeField]
    Vector2 programmerPosition;
    [SerializeField]
    Vector3[] pathPoints;
    [SerializeField]
    int programmerSpeed;
    [SerializeField]
    int programmerEffectivnes;
    [SerializeField]
    private GameObject codePrefab;
    [SerializeField]
    private LineRenderer LR;
    private bool moreThanZero;
    private void Awake()
    {
        pathPoints = new Vector3[6];
        transform.position = programmerPosition;
        if (programmerPosition.x > 0) moreThanZero = true;
        else moreThanZero = false;
        pathPoints[0] = programmerPosition;
        if(moreThanZero) pathPoints[1] = new Vector2(programmerPosition.x - 1, programmerPosition.y);
        else pathPoints[1] = new Vector2(programmerPosition.x + 1, programmerPosition.y);
        PathGenerate();
    }

    private void CodeClear()
    {

    }

    private void PathGenerate()
    {
        int pointX = (int)pathPoints[1].x;
        int pointY = (int)pathPoints[1].y;
        bool pointXchahged = true;
        bool pointYrise = false;
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
                        pointY += Random.Range(1, 2);
                        pointYrise = true;
                    }
                    else
                    {
                        pointY -= Random.Range(1, 2);
                        pointYrise = false;
                    }
                    pointXchahged = false;
                }
                else
                {
                    if (pointYrise)
                    {
                        pointY += Random.Range(1, 2);
                        pointYrise = true;
                    }
                    else
                    {
                        pointY -= Random.Range(1, 2);
                        pointYrise = false;
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
    private float codeTimer = 5;

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
