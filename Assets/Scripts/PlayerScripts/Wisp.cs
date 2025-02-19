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
    void Start()
    {
        canDash = true;
        isShooting = false;

        float MaxEssence = maxSoulEssence;
        float SoulEssence = soulEssence;
        float Cost = cost;
        float RegenAmmount = regenAmmount;
        float RegenSpeed = regenSpeed;
}

 
    void Update()
    {
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
            StartCoroutine(SoulFire(soulFire, spawnPoint));
        }

        //SoulEssenceManager();
    }
}
