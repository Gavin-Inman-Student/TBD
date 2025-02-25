using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PlayerController
{
    //Player Controller
    [SerializeField] GameObject knight;
    [SerializeField] GameObject pWisp;
    [SerializeField] Transform wispT;
    [SerializeField] Transform knightt;
    private void Start()
    {
        //The two player chars
        soulKnight = knight;
        wisp = pWisp;

        //sets Intial health and essence
        soulEssence = maxEssence;
        health = maxHealth;
        canRegen = true;

        //sets transforms so player switch at current loction
        knightT = knightt;
        soulT = wispT;

        SoulKnight.canAttack = true;
    }

    void Update()
    {
        
        //controlls which player
        StartCoroutine(Swap());
       
    }
}
