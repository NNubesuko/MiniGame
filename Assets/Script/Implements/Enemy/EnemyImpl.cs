using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaoNubeLib.System;

public class EnemyImpl : Character, Enemy {

    protected WeaponsManager weaponsManager;

    public void Init(
        int hp,
        float moveSpeed
    ) {
        HP = hp;
        MoveSpeed = moveSpeed;
    }

    /*
     *
     */
    public override void Move() {
        // プレイヤーに近い場合
            // 手元の武器で攻撃

        // プレイヤーから離れている場合
            // 地面の武器を飛ばして攻撃
            // ワープする -> プレイヤーに近い場合につながる
    }

    public override void Death() {
        if (HP == 0) {
            Debug.Log("ﾀﾋ");
        }
    }

    /*
     * 敵の状態が停止状態の場合
     */
    public void Stop(Transform targetTransform) {
    }

    /*
     * 敵の状態が追いかける状態の場合
     */
    public void Chase(Transform targetTransform) {
    }

    /*
     * 敵の状態が遠距離攻撃状態の場合
     */
    public void LongAttack() {
        // 遠距離攻撃をする
        int weaponTypeNumber = Random.Range(0, weaponsManager.WeaponTypeNumber);
        WeaponType weaponType = (WeaponType)weaponTypeNumber;
        Debug.Log(weaponType);

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