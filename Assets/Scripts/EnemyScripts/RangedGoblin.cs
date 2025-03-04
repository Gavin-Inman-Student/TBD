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
        moveSpeed = 1.2f;
        followDistance = 5.5f;

        //dash
        dashSpeed = 8;
        dashCoolDown = 5;
        dashTime = 0.2f;
        //health
        maxHealth = 80;
        health = maxHealth;
        healthBar = hBar;
        healthBar.SetMaxHealth(maxHealth, health);

        //attack
        rotatePoint = this.transform.GetChild(0).gameObject;
        spawnPoint = rotatePoint.transform.GetChild(0);
        sps = 3;
        attackDistance = 2;

        StartCoroutine(Freeze());

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        distance = Vector3.Distance(player.position, transform.position);
        
        Run();
        
        Follow();
        
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
