using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : PlayerController
{
    [SerializeField] Bars eBar;
    [SerializeField] Bars hBar;
    [SerializeField] Bars lBar;
    [SerializeField] GameObject soulFire;


    //Testing purposes...allows us to view the soul ammount from the inspector
    [SerializeField] float soulAmmount;
    [SerializeField] float healthAmmount;

    //Testing purposes...allows us to view the soul ammount from the inspector
    public float hp;
    public float mp;

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
        levelBar = lBar;
        isCasting = false;
    }
        
 
    void Update()
    {

        hp = health;
        mp = soulEssence;

        if (Input.GetKey(KeyCode.Y))
        {
            LevelManager(110);
        }

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
            SoulEssenceManager(20);
            StartCoroutine(SoulFire(soulFire));
        }

        //Essence Regen
        if (isSwapping == false)
        {
            StartCoroutine(EssenceRegen());
        }
    }

    //Melee Damage to player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("eAttack"))
        {
            StartCoroutine(HealthManager(0, 20));
        }

        else if (other.gameObject.CompareTag("Arrow"))
        {
            StartCoroutine(HealthManager(0, 10));
        }
    }
}

