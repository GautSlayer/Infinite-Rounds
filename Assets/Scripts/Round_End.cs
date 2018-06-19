﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Round_End : MonoBehaviour {

    public static bool lastRoundWin = false;

    public Text lastRoundResult;
    public Button nextRound;

    private void Start()
    {
        if (lastRoundWin)
        {
            lastRoundResult.text = "Round "+ MobWaves.roundNumber + " Won !!";
            nextRound.GetComponentInChildren<Text>().text = "Go Harder";
        }
        else
        {
            lastRoundResult.text = "Round " + MobWaves.roundNumber + " lost !!";
            nextRound.GetComponentInChildren<Text>().text = "Retry";
        }
    }
    public void NewRound()
    {
        SceneManager.LoadScene("Gauthier Test");
    }
}
