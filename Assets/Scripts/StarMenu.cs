using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class StarMenu : MonoBehaviour {

    //Launch a new game from scrath (0 progression)
    public void NewGame()
    {
        Data.NewGame();
        SceneManager.LoadScene("Horror City Level");
    }

    public void ContinueGame()
    {
        Data.LoadData();
        if(Data.mobWaves != null && Data.enemyStats != null)
        {
            SceneManager.LoadScene("Horror City Level");
        }
        else
        {
            Debug.LogError("Error is data loadging : no data");
        }
    }
}
