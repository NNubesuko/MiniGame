using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpl : Character, Enemy {

    public float ChaseDistance { get; private set; }

    private Vector3 chaseAddPosition;
    private Vector3 lastPosition;

    public void Init(
        int hp,
        float moveSpeed,
        float chaseDistance
    ) {
        HP = hp;
        MoveSpeed = moveSpeed;
        ChaseDistance = chaseDistance;
    }

    public override void Move() {
    }

    public override void Death() {
        if (HP == 0) {
            Debug.Log("ﾀﾋ");
        }
    }

    public void Rotate() {
        Vector3 diff = lastPosition - transform.position;
        if (diff == Vector3.zero) return;

        transform.rotation = Quaternion.LookRotation(diff, Vector3.up);
    }

    /*
     * 敵の状態が停止状態の場合
     */
    public void Stop(Transform targetTransform) {
    }

    public void ChangeChaseAddPosition(float addX, float addZ) {
        chaseAddPosition = new Vector3(addX, 0f, addZ) * ChaseDistance;
    }

    /*
     * 敵の状態が追いかける状態の場合
     */
    public void Chase(Transform targetTransform) {
        Vector3 targetPosition = lastPosition = targetTransform.position;
        // 高さを固定するために自分の高さを格納する
        targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition + chaseAddPosition,
            MoveSpeed * Time.deltaTime
        );
    }

    /*
     * 敵の状態が遠距離攻撃状態の場合
     */
    public void LongAttack(WeaponsManager weaponsManager) {
        // 遠距離攻撃をする
        int weaponTypeNumber = Random.Range(0, weaponsManager.WeaponTypeNumber);
        WeaponType weaponType = (WeaponType)weaponTypeNumber;

        switch (weaponType) {
            case WeaponType.Axe:
                weaponsManager.ActivateWeapon(weaponType, 2);
                break;
            case WeaponType.Hammer:
                weaponsManager.ActivateWeapon(weaponType, 2);
                break;
            case WeaponType.Sword:
                weaponsManager.ActivateWeapon(weaponType, 2);
                break;
        }
    }

    /*
     * 敵の状態が瞬間移動状態の場合
     */
    public void Teleportation() {
    }

}