using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTxt : MonoBehaviour
{
    [SerializeField]
    private string format = "F1";

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = GetComponentInParent<Slider>().value.ToString(format);
        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float val) {

        text.text = val.ToString(format);
    }


}
