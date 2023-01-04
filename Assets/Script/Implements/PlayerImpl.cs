using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpl : MonoBehaviour, Player {

    public int HP { get; private set; }
    public float Stamina { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RunSpeed { get; private set; }
    public float EvasionSpeed { get; private set; }
    public float EvasionDistance { get; private set; }

    public void Init(
        int hp,
        float stamina,
        float moveSpeed,
        float runSpeed,
        float evasionSpeed,
        float evasionDistance
    ) {
        this.HP = hp;
        this.Stamina = stamina;
        this.MoveSpeed = moveSpeed;
        this.RunSpeed = runSpeed;
        this.EvasionSpeed = evasionSpeed;
        this.EvasionDistance = evasionDistance;
    }

    public void Move() {
    }

    public void Damage(int ap) {
        HP -= ap;

        // 体力が0より小さくならないように制御
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
