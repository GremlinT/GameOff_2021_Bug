using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameLogicScript : MonoBehaviour
{
    public float AntiScore = 0;
    public int Score = 0;
    public float CodingProcecs = 0;

    public Text score;
    public Text progress;
    public Text bagging;
    public Text countdown;
    public Text endTurnText;

    public int LevelNomber = 1;

    public Sprite[] ProgSprites;
    public SpriteRenderer NERPASprite;
        
    public bool gameOver = true;

    public float TimeBeforeStart = 5f;
    private bool newLevel = true;

    private void FixedUpdate()
    {
        score.text = "Отловлено багов: " + Score;
        progress.text = "Прогресс разработки: " + CodingProcecs.ToString("##0") + "%";
        bagging.text = "Забагованность: " + AntiScore.ToString("##0") + "%";
        if (AntiScore >= 100)
        {
            GameOver();
        } 
        if (CodingProcecs >= 100)
        {
            WinLevel();
        }
        if (newLevel == true)
        {
            if (TimeBeforeStart > 0)
            {
                TimeBeforeStart -= Time.deltaTime;
                if (TimeBeforeStart < 3.5)
                {
                    endTurnText.enabled = false;
                    countdown.enabled = true;
                    countdown.text = "3";
                    countdown.fontSize = 300;
                    countdown.text = TimeBeforeStart.ToString("0");
                }
                if (TimeBeforeStart < 1)
                {
                    countdown.text = "ВПЕРЕД!";
                    countdown.fontSize = 150;
                }
            }
            else
            {
                TimeBeforeStart = 5f;
                newLevel = false;
                gameOver = false;
                countdown.enabled = false;
            }
        }
    }

    void GameOver()
    {
        gameOver = true;
        endTurnText.enabled = true;
        endTurnText.color = Color.red;
        endTurnText.text = "Не получилось...:(";
    }

    void WinLevel()
    {
        gameOver = true;
        NewLevel();
    }
    void NewLevel()
    {
        LevelNomber += 1;
        endTurnText.enabled = true;
        NERPASprite.sprite = ProgSprites[LevelNomber-1];
        CodingProcecs = 0;
        AntiScore = 0;
        newLevel = true;
    }
}
