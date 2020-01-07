using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameSettings : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public class settings
    {
        public bool osc_enable;
        //SETTINGS for the PLAYER
        //how fast boosting could be
        public float boostingScale;
        //turning speed scaler
        public float turnSpeed;
        //straight movement speed scaler
        public float movingSpeed;
        //rowing animation speed(just affect the animation)
        public float rowingSpeed;

        //SETTINGS for the ENEMIES
        //player will likely to be attacked only when it's within the range
        //enemies only stop attacking when player is out of this range
        public float attacking_radius;
        //player will surely be attacked within this range
        public float attacking_radius_close;
        //player will be attack when within the larger radius and has a noise level higher than this value
        //noise level = rowingDis/distToPlayer*k(k=2without OSC, k=1 with OSC)
        public float noise_threshold;

    }


    public settings game_settings = new settings
    {
        osc_enable = true,

        boostingScale = 0,
        turnSpeed = 0,
        movingSpeed = 0,
        rowingSpeed = 0,

        attacking_radius = 0,
        attacking_radius_close = 0,
        noise_threshold = 0,
    };

    private settings default_game_settings;

    private void Awake()
    {



        default_game_settings = new settings
        {
            osc_enable = false,

            boostingScale = 3.0f,
            turnSpeed = 1.0f,
            movingSpeed = 1.0f,
            rowingSpeed = 0.5f,

            attacking_radius = 2,
            attacking_radius_close = 1,
            noise_threshold = 2,
        };


    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //UseDefaultSettings();
        Load();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(JsonUtility.ToJson(game_settings));
        if (Input.GetKeyUp(KeyCode.BackQuote))
        {
            if (canvasGroup.interactable) {
                gameManager.Instance.pauseEffect(true);
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;

                Activate_settings(game_settings);
            }
            else {

                gameManager.Instance.pauseEffect(false);
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1;

            }
        }
    }

    public void Save() {
        string settings_json = JsonUtility.ToJson(game_settings);
        File.WriteAllText(Application.dataPath + "/settings.json", settings_json);
    }

    private void Load() {
        if (File.Exists(Application.dataPath + "/settings.json")) {
            string loaded_settings = File.ReadAllText(Application.dataPath + "/settings.json");
            game_settings = JsonUtility.FromJson<settings>(loaded_settings);
            UpdateSliderVals();
        }
        else {
            UseDefaultSettings();
        }
    }

    public void Activate_settings(settings m_settings) {
        gameManager.Instance.osc_enable = m_settings.osc_enable;

        if (m_settings.osc_enable)
        {
            PlayerControllerOSC pc = FindObjectOfType<PlayerControllerOSC>();
            pc.boostingScale = m_settings.boostingScale;
            pc.turnSpeed = m_settings.turnSpeed;
            pc.movingSpeed = m_settings.movingSpeed;
            pc.rowingSpeed = m_settings.rowingSpeed;
        }
        else {
            PlayerController pc = FindObjectOfType<PlayerController>();
            pc.boostingScale = m_settings.boostingScale;
            pc.turnSpeed = m_settings.turnSpeed;
            pc.movingSpeed = m_settings.movingSpeed;
            pc.rowingSpeed = m_settings.rowingSpeed;
        }
        foreach(EnemyAI enemy in FindObjectsOfType<EnemyAI>()) {
            enemy.attacking_radius = m_settings.attacking_radius;
            enemy.attacking_radius_close = m_settings.attacking_radius_close;
            enemy.noise_threshold = m_settings.noise_threshold;
        }


    }

    public void UseDefaultSettings() {
        game_settings = JsonUtility.FromJson<settings>(JsonUtility.ToJson(default_game_settings));
        Activate_settings(default_game_settings);
        UpdateSliderVals();
    }

    public void UpdateSliderVals() {
        foreach (SliderVal sliderval in GetComponentsInChildren<SliderVal>())
        {
            sliderval.UpdateSliderVal();
        }
    }

    public void SetOSC(bool val) { game_settings.osc_enable = val; }
    public void SetBS(float val) { game_settings.boostingScale = Mathf.Round(val*10f)/10f; }
    public void SetMS(float val) { game_settings.movingSpeed = Mathf.Round(val * 10f) / 10f; }
    public void SetRS(float val) { game_settings.rowingSpeed = Mathf.Round(val * 10f) / 10f; }
    public void SetTS(float val) { game_settings.turnSpeed = Mathf.Round(val * 10f) / 10f; }
    public void SetATKR(float val) { game_settings.attacking_radius = Mathf.Round(val * 10f) / 10f; }
    public void SetATKRC(float val) { game_settings.attacking_radius_close = Mathf.Round(val * 10f) / 10f; }
    public void SetNT(float val) { game_settings.noise_threshold = Mathf.Round(val * 10f) / 10f; }

}
