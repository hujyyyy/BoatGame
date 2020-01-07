using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostUIManger : MonoBehaviour
{
    private HealthBoostLogic m_healthboost;
    private Slider m_slider;
    // Start is called before the first frame update
    void Start()
    {
        m_healthboost = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBoostLogic>();
        m_slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        m_slider.value = m_healthboost.boostpoint;
    }
}
