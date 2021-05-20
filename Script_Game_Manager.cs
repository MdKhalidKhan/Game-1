using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Game_Manager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public Text scoreText;
    public Text ballsText;
    public Text levelText;
    public Text highscoreText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;

    public GameObject[] levels;

    public static Script_Game_Manager Instance { get; private set; }

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETE, LOADLEVEL, GAMEOVER }
    State state;
    GameObject currentBall;
    GameObject currentLevel;
    bool isSwitchingState;

    private int score;

    public int Score
    {
        get { return score; }
        set { score = value;
            scoreText.text = "Score: " + score;
        }
    }

    private int level;

    public int Level
    {
        get { return level; }
        set { level = value;
            levelText.text = "LEVEL: " + level;
        }
    }

    private int balls;

    public int Balls
    {
        get { return balls; }
        set { balls = value;
            ballsText.text = "BALLS: " + balls;
        }
    }

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }
    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
    }
    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }
    public void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
                highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                if(currentLevel != null)
                {
                    Destroy(currentLevel);
                }
                Instantiate(playerPrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETE:
                Destroy(currentBall);
                Destroy(currentLevel);
                Level++;
                panelLevelCompleted.SetActive(true);
                SwitchState(State.LOADLEVEL, 2f);
                break;
            case State.LOADLEVEL:
                if (Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    currentLevel = Instantiate(levels[level]);
                    SwitchState(State.PLAY);
                }

                break;
            case State.GAMEOVER:
                if(score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", score);
                }
                panelGameOver.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(currentBall == null)
                {
                    if(Balls > 0 )
                    {
                       currentBall =Instantiate(ballPrefab);
                    }

                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                if(currentLevel != null && currentLevel.transform.childCount == 0 && !isSwitchingState)
                {
                    SwitchState(State.LEVELCOMPLETE);
                }
                break;
            case State.LEVELCOMPLETE:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if(Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }

    public void EndState()
    {
        switch (state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETE:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }
}