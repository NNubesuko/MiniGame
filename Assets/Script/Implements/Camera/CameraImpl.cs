using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraImpl : MonoBehaviour, Camera {

    public float Distance { get; private set; }
    public float Height { get; private set; }
    public float RotateSpeed { get; private set; }
    public int CameraUpDown { get; private set; }
    public Quaternion HorizontalRotation { get; private set; }
    public Quaternion VerticalRotation { get; private set; }

    private Transform playerTransform;

    public void Init(
        float distance,
        float height,
        float rotateSpeed,
        int cameraUpDown,
        Transform playerTransform
    ) {
        Distance = distance;
        Height = height;
        RotateSpeed = rotateSpeed;
        CameraUpDown = cameraUpDown;
        this.playerTransform = playerTransform;

        HorizontalRotation = Quaternion.identity;
        VerticalRotation = Quaternion.Euler(30f, 0f, 0f);
    }

    public void Move() {
        Vector3 targetPosition = playerTransform.position + new Vector3(0f, Height, 0f);
        Vector3 distancePosition = transform.rotation * Vector3.forward * Distance;
        transform.position = targetPosition - distancePosition;
    }

    public void Rotate() {
        HorizontalRotation *= Quaternion.Euler(
            0f,
            Input.GetAxis("Mouse X") * RotateSpeed,
            0f
        );
        VerticalRotation *= Quaternion.Euler(
            Input.GetAxis("Mouse Y") * RotateSpeed * CameraUpDown,
            0f,
            0f
        );
        transform.rotation = HorizontalRotation * VerticalRotation;
    }

}
