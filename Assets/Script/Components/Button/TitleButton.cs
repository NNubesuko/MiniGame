using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KaoNubeLib.System;

public class TitleButton : MonoBehaviour {

    public void OnClickStartButton() {
        SceneManager.LoadScene("Boss");
    }

    public void OnClickQuitButton() {
        GameAdministrator.QuitGame();
    }

}
