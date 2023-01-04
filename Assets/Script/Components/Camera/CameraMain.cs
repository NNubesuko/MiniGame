using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : CameraImpl {

    [SerializeField, Header("Game Adminstrator")] private GameAdminMain gameAdmin;

    [Header("Camera Status")]
    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int cameraUpDown;

    private void Start() {
        Init(
            distance,
            height,
            rotateSpeed,
            cameraUpDown,
            gameAdmin.PlayerObject.transform
        );
    }

    private void LateUpdate() {
        Rotate();
        Move();
    }

}
