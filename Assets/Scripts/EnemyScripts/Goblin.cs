using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MeleeEnemy
{
    [Header("Movement")]
    [SerializeField] Transform enemy;
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveTime;
    [SerializeField] float stopTime;
    [SerializeField] bool isStopping = false;

    void Update()
    {
        StartCoroutine(Movement(enemy, player, moveSpeed, moveTime, stopTime, isStopping));
    }
}
