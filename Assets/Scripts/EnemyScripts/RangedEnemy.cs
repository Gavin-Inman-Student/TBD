using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    protected bool isShooting;
    protected Transform spawnPoint;
    protected float sps;

    public IEnumerator RangedAttack(GameObject projectile)
    {
        if (distance <= attackDistance && attacking == false)
        {
            if (isShooting == false)
            {
                attacking = true;
                isShooting = true;
                Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(sps);
                attacking = false;
                isShooting = false;
            }
        }

    }
}
