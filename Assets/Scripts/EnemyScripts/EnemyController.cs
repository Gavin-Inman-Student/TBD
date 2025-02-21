using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static IEnumerator Movement(Transform enemy, Transform player, float moveSpeed, float moveTime, float stopTime, bool isStopping)
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

    //not working
    IEnumerator Dash(bool canDash, bool isDashing, Transform enemy, Transform player, float dashSpeed, float dashTime, float dashCoolDown)
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
