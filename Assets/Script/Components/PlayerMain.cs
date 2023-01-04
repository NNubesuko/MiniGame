using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : PlayerImpl {

    [SerializeField] private int hp;
    [SerializeField] private float stamina;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float evasionSpeed;
    [SerializeField] private float evasionDistance;

    private void Awake() {
        Init(
            hp,
            stamina,
            moveSpeed,
            runSpeed,
            evasionSpeed,
            evasionDistance
        );
    }

    private void Update() {
        Move();
        Death();
    }

}
