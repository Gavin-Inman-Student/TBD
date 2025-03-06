using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public static bool firstUpgrade = false;

    protected static string upgrade1;
    protected static string upgrade2;
    protected static string upgrade3;

    static string selected;

    [Header("Upgrades Bool")]
    public static bool bodyOfSteel = false;
    public static bool biggerSoul1 = false;
    public static bool biggerSoul2 = false;

    public static string[] upgrades1Array = {"BodyOfSteel", "StrongerSoul1", "StrongerSoul2", "StrongerSoul3", "BiggerSoul1", "BiggerSoul2"};

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

        PlayerController.levelScene.SetActive(false);
        PlayerController.bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
    }

    public void UpgradeTwo()
    {

        selected = upgrade2;


        UpgradeSwitch();

        PlayerController.levelScene.SetActive(false);
        PlayerController.bars.SetActive(true);
        Time.timeScale = 1.0f;
        firstUpgrade = true;
    }

    public void UpgradeThree()
    {

        selected = upgrade3;

        UpgradeSwitch();


        PlayerController.levelScene.SetActive(false);
        PlayerController.bars.SetActive(true);
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
                PlayerController.maxEssence += 10;
                PlayerController.essenceBar.SetMax(PlayerController.maxEssence, 0);
                break;

            case ("BiggerSoul2"):
                PlayerController.maxEssence += 10;
                break;
        }
    }

}
