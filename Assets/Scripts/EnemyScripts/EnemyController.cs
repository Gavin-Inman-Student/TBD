using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    protected Transform player;
    protected float moveSpeed;
    protected float moveTime;
    protected float stopTime;
    protected bool isStopping = false;

    [Header("Dash")]
    protected static bool isDashing;
    protected static bool canDash;
    protected static float dashSpeed;
    protected static float dashTime;
    protected static float dashCoolDown;

    [Header("Look")]
    protected GameObject rotatePoint;
    protected GameObject warning;
    protected GameObject attack;
    protected bool canLook;
    protected Vector3 flatPos;

    [Header("Health")]
    protected HealthBar healthBar;
    protected bool isDamaged;
    protected float maxHealth;
    public float health;
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
            flatPos = player.position - transform.position;
            float angle = Mathf.Atan2(flatPos.y, flatPos.x) * Mathf.Rad2Deg + 90;
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
