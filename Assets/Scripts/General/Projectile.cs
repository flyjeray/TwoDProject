using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 movementDirection;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector3 target, Collider2D sender) {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), sender);
        Vector3 dir = Vector3.Normalize((target - transform.position) * 10000);
        movementDirection = dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate() {
        rb2d.MovePosition(transform.position + movementDirection * speed);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();

        if (healthManager) {
            healthManager.ModifyHealth(-damage);
        }

        Destroy(gameObject);
    }
}