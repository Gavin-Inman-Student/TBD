using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    bool firstUpgrade = false;

    [Header("Upgrades Name")]
    string upgrade1;
    string upgrade2;
    string upgrade3;

    string selected;


    [Header("Upgrades Bool")]
    public static bool bodyOfSteel = false;

    public static string[] upgrades = {"BodyOfSteel"};

    void Start()
    {
        if (PlayerController.levelWindow == true)
        {
            RandomUpgrade();
        }
    }

    void Update()
    {
        
    }

    public void UpgradeOne()
    {


        selected = upgrade1;

        UpgradeSelection();
    }

    public void UpgradeTwo()
    {


        selected = upgrade2;

        UpgradeSelection();
    }

    public void UpgradeThree()
    {

        selected = upgrade3;

        UpgradeSelection();
    }

    public void RandomUpgrade()
    {
        if (firstUpgrade == false)
        {
            upgrade1 = "BodyOfSteel";
            upgrade2 = "BodyOfSteel";
            upgrade3 = "BodyOfSteel";

        }
        else if (firstUpgrade == true)
        {
            int value = Random.Range(0, upgrades.Length);
            upgrade1 = upgrades[value];

            value = Random.Range(0, upgrades.Length);
            upgrade2 = upgrades[value];

            value = Random.Range(0, upgrades.Length);
            upgrade3 = upgrades[value];
        }
    }

    public void UpgradeSelection()
    {
        switch (selected)
        {
            case ("BodyOfSteel"):
                bodyOfSteel = true;
                break;
        }
    }
}
