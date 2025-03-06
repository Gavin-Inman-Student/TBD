using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering.Universal;
using UnityEditor.ShaderGraph.Internal;
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
    protected static float swapTime = 10;
    protected static Transform knightT;
    protected static Transform soulT;
    protected static GameObject selected;


    [Header("Dash")]
    protected static bool isDashing;
    protected static bool canDash;
    protected static float dashSpeed = 8;
    protected static float dashTime = 0.2f;
    protected static float dashCoolDown = 1;

    [Header("Look")]
    protected Camera camera;
    protected GameObject rotationPoint;

    [Header("SoulFire")]
    protected Transform spawnPoint;
    protected bool isCasting;
    protected bool canShoot;
    protected float soulFireDamage = 35;

    [Header("SoulEssence")]
    public static Bars essenceBar;
    protected static bool canRegen;
    public static float maxEssence = 50;
    protected static float soulEssence;
    protected static float regenAmmount = 2;
    protected static float regenSpeed = 1;

    [Header("Level")]
    protected static Bars levelBar;
    protected static float needed = 100;
    protected static float current;
    
    public static GameObject levelScene;
    public static GameObject bars;
    public static bool levelWindow = false;


    [Header("Health")]
    public static Bars healthBar;
    protected static bool isDamaged;
    public static float maxHealth = 100;
    protected static float health;
    protected static float invincibility = 0.5f;

    public static float damage;


    //Player Movement Function that takes which player body to control and the speed. Allows the player to move.

    private void Start()
    {

    }
    public void Movement(Rigidbody2D rb)
    {
        if (isDashing == true || isSwapping == true)
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
    public IEnumerator SoulFire(GameObject cast)
    {
        if (isCasting  == false && canShoot == true)
        {
            isCasting = true;
            damage = soulFireDamage;
            Instantiate(cast, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1);
            
            isCasting = false;
        }
        
    }

    //Swap Bodys
    public IEnumerator Swap()
    {
        if (Input.GetKey(KeyCode.Tab) && isDashing == false && isDamaged == false && isCasting == false && Upgrade.bodyOfSteel == true)
        {
            if (swapped == true && canSwap == true)
            {
                wisp.transform.position = knightT.position;
                canSwap = false;
                isSwapping = true;
                swapped = false;
                selected = soulKnight;
                soulKnight.SetActive(false);
                wisp.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                isSwapping = false;
                canDash = true;
                yield return new WaitForSeconds(swapTime);
                
                canSwap = true;
            }
            else if (swapped == false && canSwap == true)
            {
                soulKnight.transform.position = soulT.position;
                canSwap = false;
                isSwapping = true;
                swapped = true;
                selected = wisp;
                wisp.SetActive(false);
                canRegen = true;
                soulKnight.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                isSwapping = false;
                canDash = true;
                yield return new WaitForSeconds(swapTime);
                
                canSwap = true;
            }
        }
        
    }

    public IEnumerator HealthManager(float healingFactor, float damage)
    {
        if(isDamaged == false)
        {
            isDamaged = true;
            healthBar.SetMax(maxHealth, health);
            health -= damage;
            healthBar.SetAmmount(health);
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
        essenceBar.SetMax(maxEssence, soulEssence);
        if (soulEssence - cost < 0)
        {
            canShoot = false;
        }
        else if (soulEssence - cost >= 0 && isCasting == false)
        {
            soulEssence -= cost;
            essenceBar.SetAmmount(soulEssence);
            canShoot = true;   
        }
    }

    public static void LevelManager(float ammount)
    {
        levelBar.SetMax(needed, ammount);
        current += ammount;
        levelBar.SetAmmount(current);
        ammount = 0;
        if (current >= 100)
        {
            current =- 100;
            needed =+ 50;
            levelBar.SetAmmount(current);
            LevelScene();
        }
    }


    public static void LevelScene()
    {
        levelWindow = true;
        Time.timeScale = 0;
        levelScene.SetActive(true);
        bars.SetActive(false);

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
                essenceBar.SetAmmount(soulEssence);
                yield return new WaitForSeconds(regenSpeed);
                canRegen = true;       
            }
        }
        
    }

    public void Death()
    {
        
    }

}
