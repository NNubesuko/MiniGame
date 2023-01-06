using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaoNubeLib.System;

public class WeaponMain : WeaponImpl {

    [SerializeField] private GameAdminMain gameAdmin;

    [SerializeField] private int ap;
    [SerializeField] private float period;
    [SerializeField] private Vector3 moveSpeed;
    [SerializeField] private float rotateMagnification;

    private void Awake() {
        Init(
            ap,
            period,
            moveSpeed,
            rotateMagnification,
            gameAdmin.PlayerObject.transform
        );
    }

    private void Update() {
        Move();
        Rotate();
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            IDamagable damagable = collider.GetComponent<PlayerMain>();
            Attack(damagable);
        }
    }

}
