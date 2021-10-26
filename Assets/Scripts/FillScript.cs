using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillScript : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    [SerializeField] private GameObject fillBar;
    [SerializeField] private GameObject fill;
    void Start()
    {
        slider = fillBar.GetComponent<Slider>();
    }

  

    public void setSliderValue(int value)
    {
        slider.value = value;
    }

    public int getMaxValue()
    {
        return (int)slider.maxValue;
    }

    public int getSliderValue()
    {
        return (int)slider.value;
    }
    // Update is called once per frame
    void Update()
    {
        if(slider.value >= 25 && slider.value < 50.0f)
        {
            fill.GetComponent<RawImage>().color = Color.yellow;
        }
        else if(slider.value >= 50.0f && slider.value < 100f)
        {
            fill.GetComponent<RawImage>().color = Color.magenta;
        }
        else if(slider.value >= 100.0f)
        {
            fill.GetComponent<RawImage>().color = Color.red;
        }
        else
        {
            fill.GetComponent<RawImage>().color = Color.green;
        }
    }
}
