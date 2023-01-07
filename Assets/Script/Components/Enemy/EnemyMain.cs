using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : EnemyImpl {

    [Header("ステータス")]
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;

    [Header("クールタイム")]
    [SerializeField] private float longDistanceAttackCoolTime;

    private void Awake() {
        Init(
            hp,
            moveSpeed,
            longDistanceAttackCoolTime
        );
    }

    private void Update() {
        Death();
    }

}
