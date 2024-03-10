using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelGoto;
    public GameObject levelState;

    Button m_btnComp;

    private void Start()
    {
        m_btnComp = GetComponent<Button>();
        if (m_btnComp)
        {
            m_btnComp.onClick.AddListener(() => GotoLevel());
        }
    }

    public void GotoLevel()
    {
        LevelsManager.Ins.CurLevel = levelGoto;
        SceneManager.LoadScene("GamePlay");
    }
}
