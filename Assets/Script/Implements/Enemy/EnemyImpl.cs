using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpl : MonoBehaviour, Enemy {

    public int HP { get; private set; }
    public float MoveSpeed { get; private set; }

    public void Init(
        int hp,
        float moveSpeed
    ) {
        HP = hp;
        MoveSpeed = moveSpeed;
    }

    public void Move() {
        // プレイヤーに近い場合
            // 手元の武器で攻撃

        // プレイヤーから離れている場合
            // 地面の武器を飛ばして攻撃
            // ワープする -> プレイヤーに近い場合につながる
    }

    public void Damage(int ap) {
        HP -= ap;

        if (HP < 0) {
            HP = 0;
        }
    }

    public void Death() {
        if (HP == 0) {
            Debug.Log("ﾀﾋ");
        }
    }

}
