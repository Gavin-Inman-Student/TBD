using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : EnemyController
{

    protected GameObject projectile;
    protected Transform spawnPoint;
    public IEnumerator RangedAttack()
    {
        if (distance <= attackDistance && attacking == false)
        {
            attacking = true;
            canLook = false;
            warning.SetActive(true);
            yield return new WaitForSeconds(warningTime);
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(0.2f);
            attack.SetActive(false);
            canLook = true;
            yield return new WaitForSeconds(0.2f);
            attacking = false;
        }

    }
}
