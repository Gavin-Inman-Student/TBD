using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    protected bool isMoving;
    protected float moveSpeed = 1.5f;
    protected Rigidbody2D rb;
    protected GameObject wisp;
    protected GameObject soulKnight;

    [Header("Swap")]
    protected static bool swapped = true;
    protected static bool isSwapping;
    protected static bool canSwap = true;
    protected static float swapTime = 1;
    protected static GameObject wispClone;
    protected static GameObject soulKnightClone;
    protected static GameObject selected;


    [Header("Dash")]
    protected bool isDashing;
    protected bool canDash;
    protected float dashSpeed = 8;
    protected float dashTime = 0.2f;
    protected float dashCoolDown = 2;

    [Header("Look")]
    protected Camera camera;
    protected GameObject rotationPoint;

    [Header("Cast")]
    protected GameObject cast;
    protected Transform spawnPoint;
    protected bool isCasting;
    protected bool canShoot;

    [Header("SoulEssence")]
    protected EssenceBar essenceBar;
    protected bool canRegen;
    protected float maxEssence = 50;
    protected float soulEssence;
    protected float regenAmmount = 5;
    protected float regenSpeed = 5;


    [Header("Health")]
    protected HealthBar healthBar;
    protected bool isDamaged;
    protected float maxHealth = 100;
    protected float health;
    protected float invincibility = 0.5f;



    //Player Movement Function that takes which player body to control and the speed. Allows the player to move.

    private void Start()
    {

    }
    public void Movement(Rigidbody2D rb)
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
    public IEnumerator Dash()
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
    public void Look()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - rotationPoint.transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rotationPoint.transform.rotation = Quaternion.Euler(0,0,rotationZ);

    }

    //Instantiates SoulFire Ranged Attack.
    public IEnumerator Cast(GameObject cast)
    {
        if (isCasting  == false && canShoot == true)
        {
            isCasting = true;
            Instantiate(cast, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1);
            
            isCasting = false;
        }
        
    }

    //Swap Bodys
    public IEnumerator Swap()
    {

        if (swapped == true && canSwap == true && isSwapping == false)
        {
            canSwap = false;
            isSwapping = true;
            selected = wisp;
            wispClone = GameObject.Instantiate(wisp, spawnPoint.position, Quaternion.identity);
            GameObject.Destroy(soulKnightClone);
            yield return new WaitForSeconds(swapTime);
            swapped = false;
            isSwapping = false;
            canSwap = true;
        }
        else if (swapped == false && canSwap == true && isSwapping == false)
        {
            canSwap = false;
            isSwapping = true;
            selected = soulKnight;
            soulKnightClone = GameObject.Instantiate(soulKnight, spawnPoint.position, Quaternion.identity);
            GameObject.Destroy(wispClone);
            yield return new WaitForSeconds(swapTime);
            swapped = true; 
            isSwapping = false;
            canSwap = true;
        }
        
        
    }

    public IEnumerator HealthManager(float healingFactor, float damage)
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

    public void SoulEssenceManager(float cost)
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

    public IEnumerator EssenceRegen()
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

    public void Death()
    {
        
    }

}
