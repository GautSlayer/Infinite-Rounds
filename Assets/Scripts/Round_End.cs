using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Round_End : MonoBehaviour {

    public static bool lastRoundWin = false;

    public Text lastRoundResult;
    public Button nextRound;
    [SerializeField] List<AudioClip> audioLib;

    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        if (lastRoundWin)
        {
            lastRoundResult.text = "Round   "+ (Data.mobWaves.roundNumber-1) + "   Won !!";
            nextRound.GetComponentInChildren<Text>().text = "Go  Harder";
            audioS.clip = audioLib[0];
            audioS.Play();
        }
        else
        {
            lastRoundResult.text = "Round   " + (Data.mobWaves.roundNumber-1) + "   lost !!";
            nextRound.GetComponentInChildren<Text>().text = "Retry";
            audioS.clip = audioLib[1];
            audioS.Play();
        }
    }
    public void NewRound()
    {
        SceneManager.LoadScene("Horror City Level");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Start_Menu");
    }
}
