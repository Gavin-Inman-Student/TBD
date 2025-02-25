using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : PlayerController
{
    [SerializeField] EssenceBar eBar;
    [SerializeField] HealthBar hBar;
    [SerializeField] GameObject soulFire;


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

        //EssenceManager
        essenceBar = eBar;
        isCasting = false;
        
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

        //Essence Regen
        if(isSwapping != true)
        {
            StartCoroutine(EssenceRegen());
        }
        

        //swap

    }
}
