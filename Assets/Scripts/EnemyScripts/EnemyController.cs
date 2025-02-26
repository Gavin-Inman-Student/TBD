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

    [Header("Look")]
    protected GameObject rotatePoint;
    protected GameObject warning;
    protected GameObject attack;

    [Header("Health")]
    protected HealthBar healthBar;
    protected bool isDamaged;
    protected float maxHealth;
    public float health;
    protected float invincibility = 0.5f;

    void Start()
    {

    }


    //movement
    public void Movement()
    {
         enemy.position = Vector2.MoveTowards(enemy.position, player.position, moveSpeed * Time.deltaTime);
    }

    //faces player
    public void Look()
    {
        
    }


    //Navagates damage dealt
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

    //Destroys Object
    public void Death()
    {
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
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
