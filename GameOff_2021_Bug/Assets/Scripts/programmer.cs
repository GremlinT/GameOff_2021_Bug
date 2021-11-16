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

    private GameObject[] codeTypes = new GameObject[5];
    [SerializeField]
    private LineRenderer LR;
    private bool moreThanZero;
    private void Awake()
    {
        pathPoints = new Vector3[8];
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
        for (int i = 0; i < pathPoints.Length - 4; i++)
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
    void CodeGenerate()
    {

    }

    private void Update()
    {
        
    }
}
