using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ShowValute : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        ReadSliderValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadSliderValue()
    {
        float sliderValue = slider.value;
        gameObject.transform.GetComponent<TextMeshProUGUI>().text = sliderValue + "%";
    }
}
