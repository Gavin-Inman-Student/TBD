using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    [Header("MeleeAttack")]
    protected bool attacking;
    protected float warningTime = 2f;
    protected float distance;

    void Start()
    {
        distance = 10f;
    }

    void Update()
    {
        
    }

    public IEnumerator MeleeAttack()
    {
        if (distance <= 1f && attacking == false)
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
