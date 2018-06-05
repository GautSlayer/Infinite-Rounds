using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarMenu : MonoBehaviour {

    //Launch a new game from scrath (0 progression)
    public void NewGame()
    {
        SceneManager.LoadScene("Gauthier Test");
    }
}
