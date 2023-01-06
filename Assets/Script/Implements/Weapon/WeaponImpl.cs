using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponImpl : MonoBehaviour, Weapon {

    public int AP { get; private set; }
    public float Period { get; private set; }
    public Vector3 MoveSpeed { get; private set; }
    public float RotateMagnification { get; private set; }

    private Transform targetTransform;
    private Vector3 acceleration;
    private Vector3 position;
    private bool moveType = false;  // false -> InitMove, true -> Move

    public void Init(
        int ap,
        float initMoveSpeed,
        float period,
        Vector3 moveSpeed,
        float rotateMagnification,
        Transform targetTransform
    ) {
        AP = ap;
        Period = period;
        MoveSpeed = moveSpeed;
        RotateMagnification = rotateMagnification;
        this.targetTransform = targetTransform;

        position = transform.position;

        StartCoroutine(InitMove(initMoveSpeed));
        Invoke("ChangeMoveType", 5f);
    }

    /*
     * 移動方法を切り替える
     */
    public void ChangeMoveType() {
        moveType = true;
    }

    /*
     * 攻撃指示を受けた際の初期の移動
     */
    private IEnumerator InitMove(float initMoveSpeed) {
        while (true) {
            if (moveType) break;
            
            position.y += initMoveSpeed * Time.deltaTime;
            transform.position = position;
            yield return null;
        }
    }

    /*
     * プレイヤーに向かう際の通常移動
     */
    public void Move() {
        if (!moveType) return;

        acceleration = Vector3.zero;

        Vector3 diff = targetTransform.position - position;
        acceleration += (diff - MoveSpeed * Period) * 2f / (Period * Period);

        if (acceleration.magnitude > 100f) {
            acceleration = acceleration.normalized * 100f;
        }

        Period -= Time.deltaTime;

        MoveSpeed += acceleration * Time.deltaTime;
        position += MoveSpeed * Time.deltaTime;
        transform.position = position;
    }

    /*
     * プレイヤーへの攻撃
     */
    public void Attack(IDamageable damageable) {
        damageable.Damage(AP);
    }

    /*
     * プレイヤーに向かう際の回転
     */
    public void Rotate() {
        var diff = targetTransform.position - transform.position;
        var targetRotate = Quaternion.LookRotation(diff);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, RotateMagnification);
    }

}
