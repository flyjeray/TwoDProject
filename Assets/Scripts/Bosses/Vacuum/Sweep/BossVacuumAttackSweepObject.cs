using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVacuumAttackSweepObject : BossAttackObject
{
    [SerializeField]
    private float setupSpeed = 30;
    [SerializeField]
    private float attackSpeed = 10;

    private float speed;
    private float speedMultiplier;

    private bool rightSide = false;
    
    private Vector3 target;
    private Vector3 right;
    private Vector3 left;

    void Awake() {
        gameObject.layer = 7;
    }

    public void Setup(
        bool spawnOnRightSide, 
        Vector3 rightSpawn, 
        Vector2 leftSpawn,
        float objectSpeedMultiplier
    ) {
        rightSide = spawnOnRightSide;
        right = rightSpawn;
        left = leftSpawn;
        speedMultiplier = objectSpeedMultiplier;
        transform.localScale = new Vector3(
            transform.localScale.x * (rightSide ? 1 : -1),
            transform.localScale.y,
            transform.localScale.z
        );
        Vector3 basePosition = rightSide ? right : left;
        transform.position = new Vector3(
            basePosition.x + (rightSide ? 50 : -50),
            basePosition.y,
            basePosition.z
        );
    }

    public void SetReady() {
        speed = setupSpeed;
        target = rightSide ? right : left;
    }

    public void Launch() {
        speed = attackSpeed;
        target = rightSide ? left : right;
        GetComponent<Animator>().SetTrigger("Launched");
    }

    void FixedUpdate() {
        if (target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * speedMultiplier * Time.fixedDeltaTime);
        }
    }
}
