using UnityEngine;

public abstract class Character : MonoBehaviour, IMovable, IDamageable, ITreatable {

    // HPプロパティのデフォルト実装
    public int HP { get; protected set; }
    public int MinHP => 0;
    public int MaxHP { get; protected set; }

    // 移動プロパティのデフォルト実装
    public float MoveSpeed { get; protected set; }

    // ダメージのデフォルト実装
    public void Damage(int ap) {
        HP = Mathf.Max(HP - ap, MinHP);
    }

    // 体力回復のデフォルト実装
    public void Treatment(int addHP) {
        HP = Mathf.Min(HP + addHP, MaxHP);
    }

    // 再抽象化
    public abstract void Move();

    // 再抽象化
    public abstract void Death();

}