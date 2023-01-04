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
    }

    public void Damage(int ap) {
    }

    public void Death() {
    }

}
