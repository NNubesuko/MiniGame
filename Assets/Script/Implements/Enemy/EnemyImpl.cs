using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaoNubeLib.System;

public class EnemyImpl : Character, Enemy {

    public enum EnemyState {
        Stop,
        Chase,
        ShortAttack,
        LongAttack,
        Teleportation
    }

    public EnemyState State { get; private set; }

    public float LongAttackCoolTime { get; private set; }
    public CoolUp LongAttackCoolUp { get; private set; }
    private bool isAssailableLong = true;

    protected List<GameObject> AxeList;
    protected List<GameObject> HammerList;
    protected List<GameObject> SwordList;

    public void Init(
        int hp,
        float moveSpeed,
        float longAttackCoolTime
    ) {
        HP = hp;
        MoveSpeed = moveSpeed;
        LongAttackCoolTime = longAttackCoolTime;

        AsyncTimer longAttackTimer = new AsyncTimer(LongAttackCoolTime);
        longAttackTimer.AddEvent(ActivateLongAttack);
        LongAttackCoolUp = new CoolUp(longAttackTimer);
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

    protected void SetState(EnemyState state) {
        State = state;
    }

    public void MoveManager(Transform targetTransform = null) {
        switch (State) {
            case EnemyState.Stop:
                EnemyStateStop(targetTransform);
                break;
            case EnemyState.Chase:
                EnemyStateChase(targetTransform);
                break;
            case EnemyState.LongAttack:
                EnemyStateLongAttack(targetTransform);
                break;
        }
    }

    /*
     * 敵の状態が停止状態の場合
     */
    public void EnemyStateStop(Transform targetTransform) {
    }

    /*
     * 敵の状態が追いかける状態の場合
     */
    public void EnemyStateChase(Transform targetTransform) {
        // ターゲットが存在しない場合は、追いかけることが出来ないため停止状態に変更して処理を終了する
        if (!targetTransform) {
            SetState(EnemyState.Stop);
            return;
        }

        // ターゲットが存在する場合は、ターゲットを追いかける
    }

    /*
     * 敵の状態が近距離攻撃状態の場合
     */
    public void EnemyStateShortAttack() {
    }

    /*
     * 敵の状態が遠距離攻撃状態の場合
     */
    public void EnemyStateLongAttack(Transform targetTransform) {
        // 遠距離攻撃をする
    }

    /*
     * 敵の状態が瞬間移動状態の場合
     */
    public void EnemyStateTeleportation() {
    }

    /*
     * 近距離攻撃を可能にする
     * クールタイム経過後のコールバックで使用する
     */
    private void ActivateLongAttack() {
        isAssailableLong = true;
    }

}

public class CoolUp {

    private AsyncTimer timer;
    private bool oneTime = true;

    public CoolUp(AsyncTimer timer) {
        this.timer = timer;
    }

    public void Reset() {
        oneTime = true;
    }

    public async void Start() {
        if (oneTime) {
            oneTime = false;
            await timer.Start();
        }
    }

}