using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    protected static bool isMoving;
    protected static float moveSpeed = 1.5f;
    protected static Rigidbody2D rb;

    [Header("Dash")]
    protected static bool isDashing;
    protected static bool canDash;
    protected static float dashSpeed = 8;
    protected static float dashTime = 0.2f;
    protected static float dashCoolDown = 2;

    [Header("Look")]
    protected static Camera camera;
    protected static GameObject rotationPoint;
    protected static GameObject spawner;

    [Header("SoulFire")]
    protected static GameObject soulFire;
    protected static Transform spawnPoint;
    protected static bool isCasting;
    protected static bool canShoot;

    [Header("SoulEssence")]
    protected static bool canRegen;
    protected static float maxEssence = 50;
    protected static float soulEssence;
    protected static float regenAmmount = 5;
    protected static float regenSpeed = 5;


    [Header("Health")]
    protected static bool isDamaged;
    protected static float maxHealth = 100;
    protected static float health;
    protected static float invincibility = 0.5f;



    //Player Movement Function that takes which player body to control and the speed. Allows the player to move.

    private void Start()
    {
        
    }
    public static void Movement(Rigidbody2D rb)
    {
        if (isDashing == true)
        {
            return;
        }

        float movY = Input.GetAxisRaw("Vertical");
        float movX = Input.GetAxisRaw("Horizontal");
        Vector2 moveDirection = new Vector2(movX, movY).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

    }

    //Player Dash Function that takes which player body to control, how fast, how long, and the cooldown for the dash. Allows the player to dash.
    public static IEnumerator Dash(Rigidbody2D rb)
    {
        float movY = Input.GetAxisRaw("Vertical");
        float movX = Input.GetAxisRaw("Horizontal");
        Vector2 moveDirection = new Vector2(movX, movY).normalized;

        if (canDash == true)
        {
            canDash = false;
            isDashing = true;
            rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }
    }

    //Player Look Function that takes a camera and gameobject. Rotates the gameobject to face the cursor of the player.
    public static void Look()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - rotationPoint.transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rotationPoint.transform.rotation = Quaternion.Euler(0,0,rotationZ);

    }

    //Instantiates SoulFire Ranged Attack.
    public static IEnumerator SoulFire(GameObject soulFire, Transform spawnPoint)
    {
        if (isCasting  == false && canShoot == true)
        {
            isCasting = true;
            GameObject projectile = Instantiate(soulFire, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1);
            
            isCasting = false;
        }
        
    }

    public static IEnumerator HealthManager(HealthBar healthBar, float healingFactor, float damage)
    {
        if(isDamaged == false)
        {
            isDamaged = true;
            healthBar.SetMaxHealth(maxHealth, health);
            health -= damage;
            healthBar.SetHealth(health);
            if (health - damage <= 0)
            {
                Death();
            }
            if(healingFactor > 0)
            {
                health += healingFactor;
            }
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            yield return new WaitForSeconds(invincibility);
            isDamaged = false;
        }
        
    }

    public static void SoulEssenceManager(EssenceBar essenceBar, float cost)
    {
        essenceBar.SetMaxEssence(maxEssence, soulEssence);
        if (soulEssence - cost < 0)
        {
            canShoot = false;
        }
        else if (soulEssence - cost >= 0 && isCasting == false)
        {
            soulEssence -= cost;
            essenceBar.SetEssence(soulEssence);
            canShoot = true;   
        }
    }

    public static IEnumerator EssenceRegen(EssenceBar essenceBar)
    {
        if(canRegen == true)
        {
            if (soulEssence + regenAmmount >= maxEssence)
            {
                soulEssence = maxEssence;
                yield break;
            }
            else if (soulEssence + regenAmmount <= maxEssence)
            {
                canRegen = false;
                soulEssence = soulEssence + regenAmmount;
                essenceBar.SetEssence(soulEssence);
                yield return new WaitForSeconds(regenSpeed);
                canRegen = true;       
            }
        }
        
    }

    public static void Death()
    {
        
    }

}
