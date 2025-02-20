using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceBar : MonoBehaviour
{

    public Slider slider;

    public void SetEssence(float essence)
    {
        slider.value = essence;
    }

    public void SetMaxEssence(float maxEssence, float essence)
    {
        slider.maxValue = maxEssence;
        slider.value = essence;
    }

}

