using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MeleeEnemy
{
    [SerializeField] HealthBar hBar;
    [SerializeField] float dist;

    private void Start()
    {
        //movement
        moveSpeed = 1;
        enemy = this.transform;
        
        //health
        maxHealth = 100;
        health = maxHealth;
        healthBar = hBar;

        //attack
        rotatePoint = transform.GetChild(0).gameObject;
        warning = rotatePoint.transform.GetChild(0).gameObject;
        attack = rotatePoint.transform.GetChild(1).gameObject;
        attacking = false;

    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(MeleeAttack());

        Movement();

        Look();

        distance = Vector3.Distance(player.position, transform.position);
        dist = distance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (string.IsNullOrEmpty(tag) && !other.gameObject.CompareTag("pAttack"))
        {
            return;
        }
        StartCoroutine(HealthManager());
    }
}
