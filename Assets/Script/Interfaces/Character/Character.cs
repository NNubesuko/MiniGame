using UnityEngine;

public abstract class Character : MonoBehaviour, IMovable, IDamageable, ITreatable {

    public int HP { get; protected set; }
    public float MoveSpeed { get; protected set; }

    public const int MinHP = 0;

    public void Damage(int ap) {
        HP = Mathf.Max(HP - ap, MinHP);
    }

    public void Treatment(int addHP) {
    }

    public abstract void Move();

    public abstract void Death();

}