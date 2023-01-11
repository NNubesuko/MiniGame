using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState {
    Stop,
    Chase,
    LongAttack
}

public class State {

    public EnemyState Type { get; private set; }
    public float Value { get; set; }

    public State(EnemyState type) {
        Type = type;
        Value = 0f;
    }

}

public class StateManager {

    public List<State> StateList { get; private set; } = new List<State>();

    public StateManager() {
        int stateNumber = System.Enum.GetNames(typeof(EnemyState)).Length;

        for (int i = 0; i < stateNumber; i++) {
            StateList.Add(
                new State( (EnemyState)i )
            );
        }
    }

    public State GetState(EnemyState type) {
        foreach (State state in StateList) {
            if (state.Type == type) {
                return state;
            }
        }

        return null;
    }

    public void SortDescstateManager() {
        StateList.Sort(
            (lhState, rhState) => rhState.Value.CompareTo(lhState.Value)
        );
    }

}

public class EnemyMain : EnemyImpl {

    [SerializeField] private GameAdminMain gameAdmin;

    [Header("ステータス")]
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseDistance;

    private StateManager stateManager = new StateManager();

    private float chaseCoolTime = 5f;   // クールタイム
    private float chaseTime = 3f;       // 追いかけている時間

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
            State state = stateManager.GetState(EnemyState.Chase);
            state.Value =
                Mathf.Min(
                    state.Value + Time.deltaTime / chaseCoolTime,
                    1f
                );
        }

        if (currentState != EnemyState.LongAttack) {
            State state = stateManager.GetState(EnemyState.LongAttack);
            state.Value =
                Mathf.Min(
                    state.Value + Time.deltaTime / longAttackCoolTime,
                    1f
                );
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

    /*
     * 停止
     */
    private void StopEnterAction() {
        Debug.Log("停止");
    }

    private void StopUpdateAction() {
        stateManager.SortDescstateManager();

        State state = stateManager.StateList[0];
        if (state.Value != 1f) return;

        switch (state.Type) {
            case EnemyState.Chase:
                ChangeState(EnemyState.Chase);
                break;
            case EnemyState.LongAttack:
                ChangeState(EnemyState.LongAttack);
                break;
        }
    }

    /*
     * 追いかける
     */
    private void ChaseEnterAction() {
        Debug.Log("追いかける");
        ChangeChaseAddPosition(
            Mathf.Cos(Random.Range(0f, 1f)),
            Mathf.Sin(Random.Range(0f, 1f))
        );
    }

    private void ChaseUpdateAction() {
        Chase(playerTransform);

        State state = stateManager.GetState(EnemyState.Chase);
        // 追いかけている時間をカウントする
        state.Value = Mathf.Max(state.Value - Time.deltaTime / chaseTime, 0f);
        // 追いかけ終わったら停止状態にする
        if (state.Value == 0f) {
            ChangeState(EnemyState.Stop);
        }
    }

    /*
     * 遠距離攻撃
     */
    private void LongAttackEnterAction() {
        Debug.Log("遠距離攻撃");
        LongAttack(weaponsManager);
    }

    private void LongAttackUpdateAction() {
        State state = stateManager.GetState(EnemyState.LongAttack);
        // 攻撃している時間をカウントする
        state.Value = Mathf.Max(state.Value - Time.deltaTime / longAttackTime, 0f);
        // 攻撃が終わったら停止状態に遷移する
        if (state.Value == 0f) {
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