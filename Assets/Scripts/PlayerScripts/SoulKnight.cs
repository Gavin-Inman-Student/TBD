using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoulKnight : PlayerController
{
    [SerializeField] EssenceBar eBar;
    [SerializeField] HealthBar hBar;
    [SerializeField] GameObject soulFire;

    //Testing purposes...allows us to view the soul ammount from the inspector
    [SerializeField] float soulAmmount;
    [SerializeField] float healthAmmount;

    void Start()
    {
        //Movement
        rb = GetComponent<Rigidbody2D>();

        //Look and cast
        rotationPoint = transform.GetChild(1).gameObject;
        spawner = rotationPoint.transform.GetChild(0).gameObject;
        camera = Camera.main;

        //dash
        canDash = true;
        canRegen = true;

        //HealthManager
        healthBar = hBar;
        isDamaged = false;
        health = maxHealth;

        //EssenceManager
        essenceBar = eBar;
        isCasting = false;
        soulEssence = maxEssence;

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
            StartCoroutine(Dash());
        }

        //Look/Aim
        Look();

        //SoulFire
        if (Input.GetKey(KeyCode.E))
        {
            //SoulEssenceManager is called when abilty used
            SoulEssenceManager(20);
            StartCoroutine(Cast(soulFire));
        }

        //EssenceRegen
        StartCoroutine(EssenceRegen());

        //Test damage and healthbar/essence bar
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(HealthManager(0, 20));
        }
    }
}
