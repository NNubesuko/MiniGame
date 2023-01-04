public interface Enemy : IMovable, IDamagable {

    int HP { get; }
    float MoveSpeed { get; }

    void Init(
        int hp,
        float moveSpeed
    );

}