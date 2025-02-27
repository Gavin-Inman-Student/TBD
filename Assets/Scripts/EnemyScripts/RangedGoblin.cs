using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGoblin : RangedEnemy
{
    [SerializeField] HealthBar hBar;
    [SerializeField] GameObject arrow;

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
        rotatePoint = this.transform.GetChild(0).gameObject;
        spawnPoint = rotatePoint.transform.GetChild(0);
        sps = 3;
        attackDistance = 20;
        attacking = false;

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        distance = Vector3.Distance(player.position, transform.position);
        
        Movement();
        StartCoroutine(Dash());
        
        Look();
        
        StartCoroutine(RangedAttack(arrow));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pAttack"))
        {
            StartCoroutine(HealthManager(PlayerController.damage));
        }

    }
}
