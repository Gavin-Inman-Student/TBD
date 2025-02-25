using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MeleeEnemy
{
    private void Start()
    {
        moveSpeed = 1;
        enemy = this.transform;
        maxHealth = 100;
        health = maxHealth;
    }


    void Update()
    {
        

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        
        StartCoroutine(Movement());


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager();
    }
}
