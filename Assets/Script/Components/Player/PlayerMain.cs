using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : PlayerImpl {

    [SerializeField, Header("Game Adminstrator")] private GameAdminMain gameAdmin;

    [Header("Player Status")]
    [SerializeField] private int hp;
    [SerializeField] private float stamina;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float evasionSpeed;
    [SerializeField] private float evasionDistance;

    private void Awake() {
        Init(
            hp,
            stamina,
            moveSpeed,
            runSpeed,
            evasionSpeed,
            evasionDistance,
            gameAdmin.PlayerCamera.GetComponent<CameraMain>()
        );
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            Run();
        } else {
            Move();
        }
        
        Rotate();
        Death();
    }

}
