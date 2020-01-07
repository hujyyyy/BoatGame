using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }
    public bool gameover;
    public bool victory;

    public GameObject gameUI;

    public GameObject Player;
    private Animator player_anmi;

    public PostProcessingProfile postProcessing;

    //SETTINGS//

    public bool osc_enable;
    //just incase osc_enable is checked every update frame
    private bool osc_state;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameover = false;
        victory = false;
        SwitchPlayerCntr();
        osc_state = osc_enable;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameUI == null) gameUI = GameObject.FindWithTag("GameUI");
        if (Player == null) Player = GameObject.FindWithTag("Player");
        if (osc_state != osc_enable) {
            osc_state = osc_enable;
            SwitchPlayerCntr();

        }
    }

    public void pauseEffect(bool p)
    {
        if (p) {
            Time.timeScale = 1;
            postProcessing.grain.enabled = false;
            postProcessing.depthOfField.enabled = false;
        }
        else {
            Time.timeScale = 0;
            postProcessing.grain.enabled = true;
            postProcessing.depthOfField.enabled = true;

        }
    }

    public void EndEffect(bool e) {
        if (e) {
            gameUI.SetActive(false);
            postProcessing.vignette.enabled = true;
            postProcessing.chromaticAberration.enabled = true;
        }
        else {
            gameUI.SetActive(true);
            postProcessing.vignette.enabled = false;
            postProcessing.chromaticAberration.enabled = false;
        }
    }

    public void VictoryEffect(bool v) {
        if (v)
        {
            gameUI.SetActive(false);
            postProcessing.chromaticAberration.enabled = true;
        }
        else {
            gameUI.SetActive(true);
            postProcessing.chromaticAberration.enabled = false;
        }

    
    }

    public void enableOSC(bool val) {
        osc_enable = val;
    }

    //player controller needs to be changed with OSC on or off
    public void SwitchPlayerCntr() {
        if (osc_enable)
        {
            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponent<PlayerControllerOSC>().enabled = true;
        }
        else
        {
            Player.GetComponent<PlayerController>().enabled = true;
            Player.GetComponent<PlayerControllerOSC>().enabled = false;
        }
    }
}
