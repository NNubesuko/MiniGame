public interface Player : IMovable, IDamagable {

    int HP { get; }
    float Stamina { get; }
    float MoveSpeed { get; }
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
