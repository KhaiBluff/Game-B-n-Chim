using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGUIManager : MonoBehaviour
{
    public LevelSelectionDialog lvSelectionDialog;

    public void PlayGame()
    {
        if (lvSelectionDialog)
        {
            lvSelectionDialog.Show(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
