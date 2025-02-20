using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : PlayerController
{

    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;

    [Header("Dash")]

    [Header("Look")]
    [SerializeField] Camera camera;
    [SerializeField] GameObject rotationPoint;
    [SerializeField] GameObject spawner;

    [Header("SoulFire")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject soulFire;

    [Header("SoulEssence")]
    [SerializeField] EssenceBar essenceBar;
    //Testing purposes...allows us to view the soul ammount from the inspector
    [SerializeField] float soulAmmount;
    [SerializeField] float healthAmmount;

    [Header("Health")]
    [SerializeField] HealthBar healthBar;

    void Start()
    {
        canDash = true;
        canRegen = true;
        isCasting = false;
        isDamaged = false;
        soulEssence = maxEssence;
        health = maxHealth;
    }

 
    void Update()
    {
        //Testing purposes...allows us to view the soul ammount from the inspector
        soulAmmount = soulEssence;
        healthAmmount = health;
        
        //Movement
        Movement(rb);
        
        //Dash
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Dash(rb));
        }
        
        //Look/Aim
        Look(camera, rotationPoint, spawner);
        
        //SoulFire
        if (Input.GetKey(KeyCode.E))
        {
            //SoulEssenceManager is called when abilty used
            SoulEssenceManager(essenceBar, 20);
            StartCoroutine(SoulFire(soulFire, spawnPoint));  
        }

        //EssenceRegen
        StartCoroutine(EssenceRegen(essenceBar));
        
        //Test damage and healthbar/essence bar
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(HealthManager(healthBar, 0, 20));
        }
    }
}
