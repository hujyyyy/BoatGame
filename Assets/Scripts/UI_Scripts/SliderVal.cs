using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVal : MonoBehaviour
{
    [HideInInspector]
    public enum SliderType { 
        BoostingScale,
        TurningSpeed,
        MovingSpeed,
        RowingSpeed,
        AttackRadius,
        AttackRadiusClose,
        NoiseThreshold
    
    }

    public SliderType sliderType;
    private GameSettings gameSettings;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        gameSettings = GetComponentInParent<GameSettings>();

        slider = GetComponent<Slider>();
        //UpdateSliderVal();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameSettings.game_settings.boostingScale);
    }

    public void UpdateSliderVal() {
        if(gameSettings==null) gameSettings = GetComponentInParent<GameSettings>();
        if(slider==null) slider = GetComponent<Slider>();
        switch (sliderType)
        {

            case (SliderType.BoostingScale):
                slider.value = gameSettings.game_settings.boostingScale;
                break;
            case (SliderType.TurningSpeed):
                slider.value = gameSettings.game_settings.turnSpeed;
                break;
            case (SliderType.MovingSpeed):
                slider.value = gameSettings.game_settings.movingSpeed;
                break;
            case (SliderType.RowingSpeed):
                slider.value = gameSettings.game_settings.rowingSpeed;
                break;
            case (SliderType.AttackRadius):
                slider.value = gameSettings.game_settings.attacking_radius;
                break;
            case (SliderType.AttackRadiusClose):
                slider.value = gameSettings.game_settings.attacking_radius_close;
                break;
            case (SliderType.NoiseThreshold):
                slider.value = gameSettings.game_settings.noise_threshold;
                break;
            default:
                break;

        }
    }
}
