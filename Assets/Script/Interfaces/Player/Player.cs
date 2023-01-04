public interface Player : IMovable, IDamagable {

    void Init(
        int hp,
        float stamina,
        float moveSpeed,
        float runSpeed,
        float evasionSpeed,
        float evasionDistance
    );

}
