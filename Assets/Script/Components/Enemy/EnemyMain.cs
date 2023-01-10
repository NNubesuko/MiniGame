using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState {
    Stop,
    Chase,
    LongAttack
}

public class EnemyMain : EnemyImpl {

    [SerializeField] private GameAdminMain gameAdmin;

    [Header("ステータス")]
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;

    private float chase = 0f;           // 0 ~ 1の間で変動する 0 -> 追いかけない 1 -> 追いかける
    private float chaseCoolTime = 5f;   // クールタイム
    private float chaseTime = 3f;       // 追いかけている時間

    private float longAttack = 0f;          // 0 ~ 1の間で変動する 0 -> クールアップが必要 1 -> 遠距離攻撃可能
    private float longAttackCoolTime = 10f; // クールタイム
    private float longAttackTime = 5f;      // 遠距離攻撃している時間

    private EnemyState currentState = EnemyState.Stop;
    private bool stateEnter = true;

    private void Start() {
        Init(
            hp,
            moveSpeed
        );

        weaponsManager = gameAdmin.WeaponsManager;
    }

    private void Update() {
        if (currentState != EnemyState.Chase) {
            chase = Mathf.Min(chase + Time.deltaTime / chaseCoolTime, 1);
        }

        if (currentState != EnemyState.LongAttack) {
            longAttack = Mathf.Min(longAttack + Time.deltaTime / longAttackCoolTime, 1);
        }

        switch (currentState) {
            case EnemyState.Stop:
                StateEnter(StopEnterAction);

                if (chase == 1f) {
                    ChangeState(EnemyState.Chase);
                    return;
                }

                if (longAttack == 1f) {
                    ChangeState(EnemyState.LongAttack);
                    return; // すぐに変更したステートの行動に移れるように処理を終了する
                }

                break;

            case EnemyState.Chase:
                StateEnter(ChaseEnterAction);

                // 追いかけている時間をカウントする
                chase = Mathf.Max(chase - Time.deltaTime / chaseTime, 0f);
                // 追いかけ終わったら停止状態にする
                if (chase == 0f) {
                    ChangeState(EnemyState.Stop);
                }

                break;

            case EnemyState.LongAttack:
                StateEnter(LongAttackEnterAction);

                // 攻撃している時間をカウントする
                longAttack = Mathf.Max(longAttack - Time.deltaTime / longAttackTime, 0f);
                // 攻撃が終わったら停止状態に遷移する
                if (longAttack == 0f) {
                    ChangeState(EnemyState.Stop);
                }

                break;
        }
    }

    private void ChangeState(EnemyState newState) {
        currentState = newState;
        stateEnter = true;
    }

    private void StopEnterAction() {
        Debug.Log("停止");
    }

    private void ChaseEnterAction() {
        Debug.Log("追いかける");
    }

    private void LongAttackEnterAction() {
        Debug.Log("遠距離攻撃");
    }

    private void StateEnter(UnityAction action) {
        if (stateEnter) {
            stateEnter = false;
            action?.Invoke();
        }
    }

}