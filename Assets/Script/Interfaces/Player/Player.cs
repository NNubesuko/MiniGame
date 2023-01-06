public interface Player {

    float Stamina { get; }
    float RunSpeed { get; }
    float EvasionSpeed { get; }
    float EvasionDistance { get; }

    void Init(
        int hp,
        float stamina,
        float moveSpeed,
        float runSpeed,
        float evasionSpeed,
        float evasionDistance,
        Camera cameraScript
    );

}
