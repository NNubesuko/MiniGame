using UnityEngine;

public interface Weapon : IMovable, IAttackable {

    void Init(
        int ap,
        float period,
        Vector3 moveSpeed,
        float rotateMagnification,
        Transform targetTransform
    );

}
