using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : PlayerController
{
    [SerializeField] EssenceBar eBar;
    [SerializeField] HealthBar hBar;
    [SerializeField] GameObject soulFire;
    [SerializeField] GameObject pWisp;
    [SerializeField] GameObject knight;



    //Testing purposes...allows us to view the soul ammount from the inspector
    [SerializeField] float soulAmmount;
    [SerializeField] float healthAmmount;

    private void Awake()
    {
    }

    void Start()
    {
        //Movement
        rb = GetComponent<Rigidbody2D>();
        wisp = pWisp;
        soulKnight = knight;

        //Look and cast
        rotationPoint = transform.GetChild(1).gameObject;
        spawnPoint = rotationPoint.transform.GetChild(0);
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
            if (soulFire == null)
            {
                Debug.Log("soulFire is not assigned!");
            }
            else
            {
                SoulEssenceManager(20);
                StartCoroutine(Cast(soulFire));
            }
        }

        //EssenceRegen
        StartCoroutine(EssenceRegen());
        
        //Test damage and healthbar/essence bar
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(HealthManager(0, 20));
        }

        //swap
        if (Input.GetKey(KeyCode.Tab))
        {
            StartCoroutine(Swap());
        }
    }
}
