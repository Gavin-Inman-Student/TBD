using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform player;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashCoolDown;

    [Header("Attack")]
    [SerializeField] float stopDistance;
    [SerializeField] bool attacking = false;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject attack;
    [SerializeField] float warningTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        StartCoroutine(Dash());
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= stopDistance)
        {

            StartCoroutine(Attack());
            
        }
    }
    void Movement()
    {
        if (attacking == false && isDashing == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            
        }
        
    }
    IEnumerator Dash()
    {
        if (canDash == true && attacking == false)
        {
            canDash = false;
            isDashing = true;
            if(isDashing)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, dashSpeed * Time.deltaTime);
            }
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }     
    }

    

    IEnumerator Attack()
    {
        if (attacking == false && isDashing == false)
        {
            attacking = true;
            warning.SetActive(true);
            yield return new WaitForSeconds(warningTime);
            warning.SetActive(false);
            attack.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            attack.SetActive(false);
            yield return new WaitForSeconds(1);
            attacking = false;
        }
    }
}
