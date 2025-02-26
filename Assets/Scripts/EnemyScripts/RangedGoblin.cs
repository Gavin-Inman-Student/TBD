using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGoblin : RangedEnemy
{
    [SerializeField] HealthBar hBar;
    [SerializeField] float dist;

    private void Start()
    {
        //movement
        moveSpeed = 1;

        //dash
        canDash = true;
        dashSpeed = 8;
        dashCoolDown = 5;
        dashTime = 0.2f;
        //health
        maxHealth = 100;
        health = maxHealth;
        healthBar = hBar;

        //attack
        rotatePoint = transform.GetChild(0).gameObject;
        spawnPoint = rotatePoint.transform.GetChild(0);
        attackDistance = 8f;
        attacking = false;

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(RangedAttack());

        Movement();
        StartCoroutine(Dash());

        Look();

        distance = Vector3.Distance(player.position, transform.position);
        dist = distance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pAttack"))
        {
            StartCoroutine(HealthManager(PlayerController.damage));
        }

    }
}
