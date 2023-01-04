using UnityEngine;

public interface Camera {

    float Distance { get; }
    float Height { get; }
    float RotateSpeed { get; }
    int CameraUpDown { get; }
    Quaternion HorizontalRotation { get; }
    Quaternion VerticalRotation { get; }

    void Init(
        float distance,
        float height,
        float rotateSpeed,
        int cameraUpDown,
        Transform playerTransform
    );

}