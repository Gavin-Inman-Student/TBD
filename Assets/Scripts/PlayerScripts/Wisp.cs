using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : PlayerController
{

    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCoolDown;

    [Header("Look")]
    [SerializeField] Camera camera;
    [SerializeField] GameObject rotationPoint;
    [SerializeField] GameObject spawner;

    [Header("SoulFire")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject soulFire;

    [Header("SoulEssence")]
    [SerializeField] float regenAmmount;
    [SerializeField] float regenSpeed;
    //Testing purposes...allows us to view the soul ammount from the inspector
    [SerializeField] float soulAmmount;

    void Start()
    {
        canDash = true;
        canRegen = true;
        isCasting = false;
        soulEssence = maxEssence;
    }

 
    void Update()
    {
        //Testing purposes...allows us to view the soul ammount from the inspector
        soulAmmount = soulEssence;
        
        //Movement
        Movement(rb, moveSpeed);
        
        //Dash
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Dash(rb, dashSpeed, dashTime, dashCoolDown));
        }
        
        //Look/Aim
        Look(camera, rotationPoint, spawner);
        
        //SoulFire
        if (Input.GetKey(KeyCode.E))
        {
            //SoulEssenceManager is called when abilty used
            SoulEssenceManager(20);
            StartCoroutine(SoulFire(soulFire, spawnPoint));  
        }

        
        

        //EssenceRegen
        StartCoroutine(EssenceRegen(regenAmmount, regenSpeed));
    }
}
