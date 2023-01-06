using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpl : Character, Player {

    public float Stamina { get; private set; }
    public float RunSpeed { get; private set; }
    public float EvasionSpeed { get; private set; }
    public float EvasionDistance { get; private set; }

    private const float MaxStamina = 100f;
    private const float MinStamina = 0f;

    private Camera cameraScript;
    private Vector3 currentPosition;

    public void Init(
        int hp,
        float stamina,
        float moveSpeed,
        float runSpeed,
        float evasionSpeed,
        float evasionDistance,
        Camera cameraScript
    ) {
        this.HP = hp;
        this.Stamina = stamina;
        this.MoveSpeed = moveSpeed;
        this.RunSpeed = runSpeed;
        this.EvasionSpeed = evasionSpeed;
        this.EvasionDistance = evasionDistance;
        this.cameraScript = cameraScript;
    }

    public override void Move() {
        Move(MoveSpeed);
    }

    public void Run() {
        Move(RunSpeed);
    }

    public void IncrementStamina() {
        Stamina = Mathf.Min(Stamina + Time.deltaTime, MaxStamina);
    }

    public void DecrementStamina() {
        Stamina = Mathf.Max(Stamina - Time.deltaTime, MinStamina);
    }

    public void Rotate() {
        if (currentPosition == Vector3.zero) return;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(cameraScript.HorizontalRotation * currentPosition),
            0.1f
        );
    }

    public override void Death() {
        if (HP == 0) {
            Debug.Log("ﾀﾋ");
        }
    }

    private void Move(float speed) {
        currentPosition = Vector3.zero;
        
        currentPosition += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        currentPosition = currentPosition.normalized * speed * Time.deltaTime;

        transform.position += cameraScript.HorizontalRotation * currentPosition;
    }

}
