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
    [SerializeField] private float chaseDistance;

    private float chase = 0f;           // 0 ~ 1の間で変動する 0 -> 追いかけない 1 -> 追いかける
    private float chaseCoolTime = 5f;   // クールタイム
    private float chaseTime = 3f;       // 追いかけている時間

    private float longAttack = 0f;          // 0 ~ 1の間で変動する 0 -> クールアップが必要 1 -> 遠距離攻撃可能
    private float longAttackCoolTime = 10f; // クールタイム
    private float longAttackTime = 5f;      // 遠距離攻撃している時間

    private EnemyState currentState = EnemyState.Stop;
    private bool stateEnter = true;

    private Transform playerTransform;
    private WeaponsManager weaponsManager;

    private void Start() {
        Init(
            hp,
            moveSpeed,
            chaseDistance
        );

        playerTransform = gameAdmin.PlayerObject.transform;
        weaponsManager = gameAdmin.WeaponsManager;
    }

    private void Update() {
        MoveManager();
        Rotate();
    }

    private void MoveManager() {
        if (currentState != EnemyState.Chase) {
            chase = Mathf.Min(chase + Time.deltaTime / chaseCoolTime, 1);
        }

        if (currentState != EnemyState.LongAttack) {
            longAttack = Mathf.Min(longAttack + Time.deltaTime / longAttackCoolTime, 1);
        }

        switch (currentState) {
            case EnemyState.Stop:
                StateEnter(StopEnterAction);
                StateUpdate(StopUpdateAction);
                break;

            case EnemyState.Chase:
                StateEnter(ChaseEnterAction);
                StateUpdate(ChaseUpdateAction);
                break;

            case EnemyState.LongAttack:
                StateEnter(LongAttackEnterAction);
                StateUpdate(LongAttackUpdateAction);
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
        ChangeChaseAddPosition(
            Mathf.Cos(Random.Range(0f, 1f)),
            Mathf.Sin(Random.Range(0f, 1f))
        );
    }

    private void LongAttackEnterAction() {
        Debug.Log("遠距離攻撃");
        LongAttack(weaponsManager);
    }

    private void StopUpdateAction() {
        if (chase == 1f) {
            ChangeState(EnemyState.Chase);
            return;
        }

        if (longAttack == 1f) {
            ChangeState(EnemyState.LongAttack);
            return; // すぐに変更したステートの行動に移れるように処理を終了する
        }
    }

    private void ChaseUpdateAction() {
        Chase(playerTransform);

        // 追いかけている時間をカウントする
        chase = Mathf.Max(chase - Time.deltaTime / chaseTime, 0f);
        // 追いかけ終わったら停止状態にする
        if (chase == 0f) {
            ChangeState(EnemyState.Stop);
        }
    }

    private void LongAttackUpdateAction() {
        // 攻撃している時間をカウントする
        longAttack = Mathf.Max(longAttack - Time.deltaTime / longAttackTime, 0f);
        // 攻撃が終わったら停止状態に遷移する
        if (longAttack == 0f) {
            ChangeState(EnemyState.Stop);
        }
    }

    /*
     * ステータスに遷移した瞬間に一度だけ実行されるヘルプメソッド
     */
    private void StateEnter(UnityAction action) {
        if (stateEnter) {
            stateEnter = false;
            action?.Invoke();
        }
    }

    /*
     * ステータスに遷移した瞬間から別のステータスに遷移するまで実行されるヘルプメソッド
     */
    private void StateUpdate(UnityAction action) {
        action?.Invoke();
    }

}