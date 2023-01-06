using UnityEngine;

public interface Weapon : IMovable, IAttackable {

    /*
     * ステータス初期化
     */
    void Init(
        int ap,
        float moveHeight,
        float period,
        Vector3 moveSpeed,
        float rotateMagnification,
        Transform targetTransform
    );

}
