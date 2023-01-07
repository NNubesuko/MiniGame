using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpl : Character, Enemy {

    public float LongDistanceAttackCoolTime { get; private set; }

    private bool hasEndedLongDistanceAttack = false;
    private bool isAssailableLongDistance = true;

    public void Init(
        int hp,
        float moveSpeed,
        float longDistanceAttackCoolTime
    ) {
        HP = hp;
        MoveSpeed = moveSpeed;
        LongDistanceAttackCoolTime = longDistanceAttackCoolTime;
    }

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
     * クールタイムを管理する
     */
    public void CoolTimeManagement() {
        if (hasEndedLongDistanceAttack) {
            hasEndedLongDistanceAttack = false;
            StartCoroutine(CoolUpLongDistanceAttack());
        }
    }

    private IEnumerator CoolUpLongDistanceAttack() {
        yield return new WaitForSeconds(LongDistanceAttackCoolTime);
        isAssailableLongDistance = true;
    }

}
