using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponImpl : MonoBehaviour, Weapon {

    public int AP { get; private set; }
    public float Period { get; private set; }
    public Vector3 MoveSpeed { get; private set; }
    public float RotateMagnification { get; private set; }

    private Transform targetTransform;
    private Vector3 acceleration;
    private Vector3 position;

    public void Init(
        int ap,
        float period,
        Vector3 moveSpeed,
        float rotateMagnification,
        Transform targetTransform
    ) {
        AP = ap;
        Period = period;
        MoveSpeed = moveSpeed;
        RotateMagnification = rotateMagnification;
        this.targetTransform = targetTransform;

        position = transform.position;
    }

    public void Move() {
        acceleration = Vector3.zero;

        Vector3 diff = targetTransform.position - position;
        acceleration += (diff - MoveSpeed * Period) * 2f / (Period * Period);

        if (acceleration.magnitude > 100f) {
            acceleration = acceleration.normalized * 100f;
        }

        Period -= Time.deltaTime;

        MoveSpeed += acceleration * Time.deltaTime;
        position += MoveSpeed * Time.deltaTime;
        transform.position = position;
    }

    public void Attack(IDamageable damageable) {
        damageable.Damage(AP);
    }

    public void Rotate() {
        var diff = targetTransform.position - transform.position;
        var targetRotate = Quaternion.LookRotation(diff);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, RotateMagnification);
    }

}
