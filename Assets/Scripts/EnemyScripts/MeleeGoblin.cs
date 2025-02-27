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
        stopTime = 3;
        moveTime = 10;

        //dash
        canDash = true;
        dashSpeed = 8;
        dashCoolDown = 5;
        dashTime = 0.2f;
        pauseTime = 1.3f;

        //health
        maxHealth = 100;
        health = maxHealth;
        healthBar = hBar;
        healthBar.SetMaxHealth(maxHealth, health);
        

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
        
        distance = Vector3.Distance(player.position, transform.position);
        
        

        Movement();
        Look();
        StartCoroutine(Dash());
        StartCoroutine(MeleeAttack());
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pAttack"))
        {
            StartCoroutine(HealthManager(PlayerController.damage));
        }
        
    }
}
