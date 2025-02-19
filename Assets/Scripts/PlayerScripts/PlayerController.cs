using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public bool isMoving;

    [Header("Dash")]
    protected static bool isDashing;
    protected static bool canDash;

    [Header("SoulFire")]
    protected static bool isShooting;
    protected static bool canShoot;

    [Header("SoulEssence")]
    protected static bool canRegen;
    protected static float maxSoulEssence;
    protected float soulEssence;
    protected float cost;
    protected float regenAmmount;
    protected float regenSpeed;


    //Player Movement Function that takes which player body to control and the speed. Allows the player to move.
    public static void Movement(Rigidbody2D rb, float moveSpeed)
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
    public static IEnumerator Dash(Rigidbody2D rb, float dashSpeed, float dashTime, float dashCoolDown)
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
    public static void Look(Camera camera, GameObject rotationPoint, GameObject spawnPoint)
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - rotationPoint.transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rotationPoint.transform.rotation = Quaternion.Euler(0,0,rotationZ);

    }

    //Instantiates SoulFire Ranged Attack.
    public static IEnumerator SoulFire(GameObject soulFire, Transform spawnPoint)
    {
        if (isShooting  == false)
        {
            GameObject projectile = Instantiate(soulFire, spawnPoint.position, spawnPoint.rotation);
            isShooting = true;
            yield return new WaitForSeconds(1);
            isShooting = false;
        }
        
    }

    public static void HealthManager(float maxHealth, float health, float healingFactor, float damage)
    {
        health -= damage;
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
    }

    public static void SoulEssenceManager(float maxSoulEssence, float soulEssence, float cost, float regenAmmount, float regenSpeed)
    {
        
        if (soulEssence - cost <= 0)
        {
            canShoot = false;
            soulEssence += cost;
        }
        else if (soulEssence - cost >= 0)
        {
            canShoot = true;
        }
    }

    IEnumerator EssenceRegen(float maxSoulEssence, float soulEssence, float regenAmmount, float regenSpeed)
    {
        if(canRegen == true)
        {
            if (soulEssence + regenAmmount >= maxSoulEssence)
            {
                soulEssence = maxSoulEssence;
            }
            else if (soulEssence + regenAmmount <= maxSoulEssence)
            {
                soulEssence += regenAmmount;
                canRegen = false;
            }
        }
        yield return new WaitForSeconds(regenSpeed);
        canRegen = true;       
    }

    public static void Death()
    {

    }

}
