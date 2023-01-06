using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidbody : MonoBehaviour {

    private Vector3 acceleration;
    private Vector3 velocity;
    private Vector3 position;

    private void Awake() {
        position = transform.position;
    }

    private void FixedUpdate() {
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;

        acceleration = Vector3.zero;
    }

    public void AddForce(Vector3 force) {
        acceleration += force;
    }

}
