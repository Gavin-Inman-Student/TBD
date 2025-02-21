using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    IEnumerator MeleeAttack(bool attacking, GameObject warning, GameObject attack, float warningTime)
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
