using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public static Transform player;
    protected float moveSpeed;
    protected float moveTime;
    protected float stopTime;
    protected bool isStopping = false;

    [Header("Dash")]
    protected bool isDashing;
    protected bool canDash;
    protected float dashSpeed;
    protected float dashTime;
    protected float dashCoolDown;

    [Header("Look")]
    protected GameObject rotatePoint;
    protected GameObject warning;
    protected GameObject attack;
    protected bool canLook;

    [Header("Health")]
    protected HealthBar healthBar;
    protected bool isDamaged;
    protected float maxHealth;
    protected float health;
    protected float invincibility = 0.5f;

    [Header("Attacks")]
    protected float attackDistance;
    protected bool attacking;
    protected float distance;
    protected float warningTime = 0.5f;

    void Start()
    {

    }


    //movement
    public void Movement()
    {
        if (isStopping == false && isDashing == false)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            if(attacking == true)
            {
                rb.velocity = new Vector2 (0, 0);
            }
            else if (attacking == false)
            {
                Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized;
                rb.velocity = (pos * moveSpeed);
            }
        }
    }

    //faces player
    public void Look()
    {
        if (canLook == true)
        {
            Vector3 dist = player.position - transform.position;
            float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rotatePoint.transform.rotation = Quaternion.Slerp(transform.rotation, q, 180);
        }
    }


    //Navagates damage dealt
    public IEnumerator HealthManager(float damage)
    {
        if (isDamaged == false)
        {
            isDamaged = true;
            healthBar.SetMaxHealth(maxHealth, health);
            health -= damage;
            healthBar.SetHealth(health);
            if (health - damage <= 0)
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
    public IEnumerator Dash()
    {
        if (canDash == true)
        {
            canDash = false;
            isDashing = true;
            canLook = false;
            if (isDashing)
            {
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized;
                rb.velocity = (pos * dashSpeed);
            }
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            yield return new WaitForSeconds(dashCoolDown);
            canLook = true;
            canDash = true;
        }
    }

}
