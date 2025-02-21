using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoulKnight : PlayerController
{
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
        soulEssence = maxEssence;
    }


    void Update()
    {
        //Testing purposes...allows us to view the soul ammount from the inspector
        soulAmmount = soulEssence;

        //Movement
        Movement(rb);

        //Dash
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Dash(rb));
        }

        //Look/Aim
        Look();

        //SoulFire
        if (Input.GetKey(KeyCode.E))
        {
            //SoulEssenceManager is called when abilty used
            SoulEssenceManager(essenceBar, 20);
            StartCoroutine(SoulFire());
        }




        //EssenceRegen
        StartCoroutine(EssenceRegen(essenceBar));
    }
}
