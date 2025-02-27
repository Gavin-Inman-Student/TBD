using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MeleeEnemy
{
    [SerializeField] HealthBar hBar;

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
        warning = rotatePoint.transform.GetChild(0).gameObject;
        attack = rotatePoint.transform.GetChild(1).gameObject;
        attackDistance = 1.5f;
        attacking = false;

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(MeleeAttack());

        Movement();
        StartCoroutine(Dash());

        Look();

        distance = Vector3.Distance(player.position, transform.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pAttack"))
        {
            StartCoroutine(HealthManager(PlayerController.damage));
        }
        
    }
}
