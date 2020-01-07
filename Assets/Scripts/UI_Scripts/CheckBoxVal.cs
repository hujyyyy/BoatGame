using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxVal : MonoBehaviour
{
    private Toggle toogle;
    // Start is called before the first frame update
    void Start()
    {
        toogle = GetComponent<Toggle>();
        toogle.isOn = gameManager.Instance.osc_enable;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
