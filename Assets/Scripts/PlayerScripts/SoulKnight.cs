using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoulKnight : PlayerController
{
    [SerializeField] Bars eBar;
    [SerializeField] Bars lBar;
    [SerializeField] Bars hBar;
    [SerializeField] GameObject soulFire;
    
    //MeleeAttack
    GameObject attackRange;
    public static bool canAttack;
    float attackCoolDown = 0.8f;
    protected float meleeAttackDamage = 20;


    //Testing purposes...allows us to view the soul ammount from the inspector

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
        levelBar = lBar;
        isCasting = false;

        //Attack
        attackRange = rotationPoint.transform.GetChild(1).gameObject;
    }


    void Update()
    {


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
            StartCoroutine(SoulFire(soulFire));
        }

        //MeleeAttack
        StartCoroutine(MeleeAttack());

    }

    //Melee Attack
    IEnumerator MeleeAttack()
    {
        if(Input.GetMouseButton(0) && canAttack == true)
        {
            canAttack = false;
            damage = meleeAttackDamage;
            attackRange.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            attackRange.SetActive(false);
            yield return new WaitForSeconds(attackCoolDown);
            canAttack = true;
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


