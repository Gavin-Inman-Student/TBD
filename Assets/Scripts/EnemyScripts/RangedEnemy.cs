using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    protected bool isShooting;
    protected bool isRunning;
    protected bool isFollowing;
    protected Transform spawnPoint;
    protected float sps;
    protected float followDistance;

    public IEnumerator RangedAttack(GameObject projectile)
    {
        if (isRunning == false && attacking == false)
        {
            if (isShooting == false)
            {
                attacking = true;
                isShooting = true;
                Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                attacking = false;
                yield return new WaitForSeconds(sps);
                isShooting = false;
            }
        }
    }

    public void Run()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (distance <= attackDistance)
        {
            if (attacking == true)
            {
                rb.velocity = new Vector2(0, 0);
            }
            else if (attacking == false)
            {
                isRunning = true;
                Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized * -1;
                rb.velocity = (pos * moveSpeed);
            }
        }
        else if (isDashing == false && distance >= attackDistance)
        {
            rb.velocity = Vector2.zero * Time.deltaTime;
            isRunning = false;
        }
    }

    public void Follow()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (distance >= followDistance)
        {
            isFollowing = true;
            Vector2 pos = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y)).normalized;
            rb.velocity = (pos * moveSpeed);
        }
        else if (distance <= followDistance)
        {
            isFollowing = false;
        }
    }
}
