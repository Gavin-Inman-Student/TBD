using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    protected Transform enemy;
    protected Transform player;
    protected float moveSpeed;
    protected float moveTime;
    protected float stopTime;
    protected bool isStopping = false;

    [Header("Health")]
    protected HealthBar healthBar;
    protected static bool isDamaged;
    protected static float maxHealth;
    protected static float health;
    protected static float invincibility = 0.5f;
    protected Collider2D soulFire;

    void Start()
    {
        soulFire = GameObject.FindWithTag("SoulFire").GetComponent<Collider2D>();
    }

    public IEnumerator Movement()
    {
        if(isStopping == false)
        {
            enemy.position = Vector2.MoveTowards(enemy.position, player.position, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(moveTime);
            isStopping = true;
        }
        
        if(isStopping == true)
        {
            enemy.transform.position = enemy.position;
            yield return new WaitForSeconds(stopTime);
            isStopping= false;
        }
    }

    public IEnumerator HealthManager()
    {
        if (isDamaged == false)
        {
            isDamaged = true;
            healthBar.SetMaxHealth(maxHealth, health);
            health -= PlayerController.damage;
            healthBar.SetHealth(health);
            if (health - PlayerController.damage <= 0)
            {
                Death();
            }
            yield return new WaitForSeconds(invincibility);
            isDamaged = false;
        }

    }

    public void Death()
    {

    }

    //not working
    public IEnumerator Dash(bool canDash, bool isDashing, Transform enemy, Transform player, float dashSpeed, float dashTime, float dashCoolDown)
    {
        canDash = false;
        isDashing = true;
        if (isDashing)
        {
            enemy.position = Vector2.MoveTowards(enemy.position, player.position, dashSpeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

}
