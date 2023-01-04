using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaoNubeLib.System;

public class GameAdminMain : MonoBehaviour {

    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject playerCamera;

    public GameObject PlayerObject { get; private set; }
    public GameObject PlayerCamera { get; private set; }

    private void Awake() {
        PlayerObject = playerObject;
        PlayerCamera = playerCamera;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameAdministrator.QuitGame();
        }
    }

}
