using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : PlayerController
{

    [Header("Upgrades Controller")]
    public static bool firstUpgrade = false;

    protected static string upgrade1;
    protected static string upgrade2;
    protected static string upgrade3;

    new static string selected;

    [Header("Upgrades Bool")]
    public static bool bodyOfSteel = false;
    public static bool biggerSoul1 = false;
    public static bool biggerSoul2 = false;
    public static bool strongerSoul1 = false;
    public static bool resilient1 = false;
    public static bool resilient2 = false;


    [Header("Upgrades")]
    public static string[] upgrades1Array = {"BodyOfSteel", "StrongerSoul1", "BiggerSoul1", "BiggerSoul2", "Resilient1", "Resilient2" };

    static List<string> upgrades1List = new List<string>();


    private void Start()
    {
        for(int i = 0; i < upgrades1Array.Length; i++)
        {
            upgrades1List.Add(upgrades1Array[i]);
        }
    }
    void OnEnable()
    {
        UpgradeSelection();
        UpgradeFilter();
    }

    public void UpgradeOne()
    {  
        selected = upgrade1;

        UpgradeSwitch();
        UpgradeFilter();

        levelScene.SetActive(false);
        bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
    }

    public void UpgradeTwo()
    {

        selected = upgrade2;


        UpgradeSwitch();
        UpgradeFilter();

        levelScene.SetActive(false);
        bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
    }

    public void UpgradeThree()
    {

        selected = upgrade3;

        UpgradeSwitch();
        UpgradeFilter();

        levelScene.SetActive(false);
        bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
    }

    public static void UpgradeSelection()
    {      
        RandomUpgrade();

        GameObject.Find("UpgradeOne").GetComponentInChildren<TextMeshProUGUI>().SetText(upgrade1);
        GameObject.Find("UpgradeTwo").GetComponentInChildren<TextMeshProUGUI>().SetText(upgrade2);
        GameObject.Find("UpgradeThree").GetComponentInChildren<TextMeshProUGUI>().SetText(upgrade3);
    }

    public static void RandomUpgrade()
    {
        if (firstUpgrade == false)
        {
            upgrade1 = "BodyOfSteel";
            upgrade2 = "BodyOfSteel";
            upgrade3 = "BodyOfSteel";

        }
        else if (firstUpgrade == true)
        {
            int value = Random.Range(0, upgrades1List.Count);
            upgrade1 = upgrades1List[value];

            value = Random.Range(0, upgrades1List.Count);
            upgrade2 = upgrades1List[value];

            value = Random.Range(0, upgrades1List.Count);
            upgrade3 = upgrades1List[value];
        }
    }

    static void UpgradeFilter()
    {
        switch (selected)
        {
            case ("BodyOfSteel"):
                if (bodyOfSteel == true)
                {
                    upgrades1List.Remove("BodyOfSteel");
                    break;
                }
                else
                    break;

            case ("BiggerSoul1"):
                if (biggerSoul1 == true)
                {
                    upgrades1List.Remove("BiggerSoul1");
                    break;
                }
                else
                    break;

            case ("BiggerSoul2"):
                if (biggerSoul2 == true)
                {
                    upgrades1List.Remove("BiggerSoul2");
                    break;
                }
                else
                    break;

            case ("StrongerSoul1"):
                if (strongerSoul1 == true)
                {
                    upgrades1List.Remove("StrongerSoul1");
                    break;
                }
                else
                    break;

            case ("Resilient1"):
                if (resilient1 == true)
                {
                    upgrades1List.Remove("Resilient1");
                    break;
                }
                else
                    break;

            case ("Resilient2"):
                if (resilient2 == true)
                {
                    upgrades1List.Remove("Resilient2");
                    break;
                }
                else
                    break;
        }
    }

    static void UpgradeSwitch()
    {
        switch (selected)
        {
            case ("BodyOfSteel"):
                bodyOfSteel = true;
                break;

            case ("BiggerSoul1"):
                biggerSoul1 = true;
                maxEssence += 10;
                essenceBar.SetMax(maxEssence, soulEssence);
                break;

            case ("BiggerSoul2"):
                biggerSoul2 = true;
                maxEssence += 10;
                essenceBar.SetMax(maxEssence, soulEssence);
                break;

            case ("StrongerSoul1"):
                strongerSoul1 = true;
                regenAmmount += 1;
                essenceBar.SetMax(maxEssence, soulEssence);
                break;

            case ("Resilient1"):
                resilient1 = true;
                maxHealth += 10;
                health += 10;
                healthBar.SetMax(maxEssence, soulEssence);
                break;

            case ("Resilient2"):
                resilient2 = true;
                maxHealth += 10;
                health += 10;
                healthBar.SetMax(maxEssence, soulEssence);
                break;
        }
    }

}
