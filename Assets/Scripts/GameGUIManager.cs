using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : MonoBehaviour
{
    public static GameGUIManager Ins;

    //public GameObject homeGui;
    public GameObject gameGui;

    public Dialog gameDialog;
    public Dialog pauseDialog;

    public Image fireRateFilled;
    public Text timerText;
    public Text killedCountingText;
    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    private void Awake()
    {
        MakeSingleTon();
    }

    private void MakeSingleTon()
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
    public void ShowGameGUI(bool isShow)
    {
        if (gameGui)
        {
            gameGui.SetActive(isShow);
        }
        //if (homeGui)
        //{
        //    homeGui.SetActive(!isShow);
        //}
    }
    public void UpdateTimer(string time)
    {
        if (timerText)
        {
            timerText.text = time;
        }

    }
    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
        {
            killedCountingText.text = "x" + killed.ToString();
        }
    }
    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
        {
            fireRateFilled.fillAmount = rate;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (pauseDialog)
        {
            pauseDialog.Show(true);
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED : " + killedCountingText.text);
            m_curDialog = pauseDialog;
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }
    }
    public void BackToHome()
    {
        ResumeGame();
        SceneManager.LoadScene("Main");
    }
    public void Relay()
    {
        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ShowGameGUI(true);
        //  GameManager.Ins.PlayGame();

    }
    public void Exit()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }
}
