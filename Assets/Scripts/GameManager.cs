using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static BirdManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;

    public float spawnTime;
    public int timeLimit;
    private int m_level;
    public Text m_bestScore;
    private BirdManager m_levelObj;

    AudioController m_audio;

    int m_curTimeLimit;
    int m_birdKilled;
    bool m_isGameOver;

    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public int Level { get => m_level;}
    public BirdManager LevelObj { get => m_levelObj; }

    public void Awake()
    {
        m_curTimeLimit = timeLimit;
        m_isGameOver = false;
        m_audio = FindObjectOfType<AudioController>();

        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //GameGUIManager.Ins.ShowGameGUI(false);
        if (LevelsManager.Ins.CurLevel == 0)
        {
            m_bestScore.text = "Best score: " + Prefs.bestScore_1;
        }
        else if (LevelsManager.Ins.CurLevel == 1)
        {
            m_bestScore.text = "Best score: " + Prefs.bestScore_2;
        }
        else
        {
            m_bestScore.text = "Best score: " + m_birdKilled;
        }

        PlayGame();
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKilled);
    }
    public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGUI(true);
    }
    IEnumerator TimeCountDown()
    {
        BirdManager[] levelPrefabs = LevelsManager.Ins.levels;

        if(levelPrefabs != null && levelPrefabs.Length > 0 && levelPrefabs.Length > LevelsManager.Ins.CurLevel)
        {
            BirdManager levelPrefab = levelPrefabs[LevelsManager.Ins.CurLevel];

            if(levelPrefab != null)
            {
                m_level = LevelsManager.Ins.CurLevel;
                m_levelObj = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            }
        }
        while (m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);

            m_curTimeLimit--;

            if (m_curTimeLimit <= 0)
            {
                m_isGameOver = true;

                if (LevelsManager.Ins.CurLevel == 0)
                {
                    BestScore(Prefs.bestScore_1);
                    Prefs.bestScore_1 = m_birdKilled;
                }
                else if (LevelsManager.Ins.CurLevel == 1)
                {
                    BestScore(Prefs.bestScore_2);
                    Prefs.bestScore_2 = m_birdKilled;
                }
                else
                {
                    BestScore(Prefs.bestScore_3);
                    Prefs.bestScore_3 = m_birdKilled;
                }


                // GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED: x"+ m_birdKilled);
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;

            }
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));

        }

    }

    public void BestScore(int bestScore)
    {
        if (m_birdKilled > bestScore)
        {
            GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST SCORE : x" + m_birdKilled);
        }
        else if (m_birdKilled <= bestScore)
        {
            //số con chim mà bắn nhỏ hơn thì cập nhật lại số điểm cũ
            GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST SCORE : x" + bestScore);
        }

    }

    IEnumerator GameSpawn()
    {
        while (!m_isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        Vector3 spawnPosCrow = Vector3.zero;
        float randCheck = Random.Range(0f, 1f);
        if (randCheck >= 0.5f)
        {
            spawnPos = new Vector3(10, Random.Range(1.5f, 4f), 0);
            spawnPosCrow = new Vector3(11, Random.Range(1.2f, 4f), 1);
        }
        else
        {
            spawnPos = new Vector3(-10, Random.Range(1.5f, 4f), 0);
            spawnPosCrow = new Vector3(-11, Random.Range(1.2f, 4f), 1);
        }
        if (m_levelObj != null && m_levelObj.listBirds.Length > 0)
        {
            int randIdx = Random.Range(0, m_levelObj.listBirds.Length);
            if (m_levelObj.listBirds[randIdx] != null)
            {
                Bird birdClone = Instantiate(m_levelObj.listBirds[randIdx], spawnPos, Quaternion.identity);

            }

        }
        if (m_levelObj != null && m_levelObj.listCrows.Length > 0)
        {
            int randIdx = Random.Range(0, m_levelObj.listCrows.Length);
            if (m_levelObj.listCrows[randIdx] != null)
            {
                Crow crowClone = Instantiate(m_levelObj.listCrows[randIdx], spawnPosCrow, Quaternion.identity);

            }

        }
    }


    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);    
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    //public void AddScore(int scoreToAdd)
    //{
    //    m_score = scoreToAdd;
    //    Prefs.bestScore = scoreToAdd;
    //}
}
