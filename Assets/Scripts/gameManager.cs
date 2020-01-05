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

    }

    // Update is called once per frame
    void Update()
    {
        if (gameUI == null) gameUI = GameObject.FindWithTag("GameUI");
        if (Player == null) Player = GameObject.FindWithTag("Player");
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


}
