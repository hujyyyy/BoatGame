using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{

    private CanvasGroup canvasGroup;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.gameover) return;
        if (Input.GetKeyUp(KeyCode.Escape)|| Input.GetKeyUp(KeyCode.P))
        {
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false; 
                canvasGroup.blocksRaycasts = false; 
                canvasGroup.alpha = 0f;
                gameManager.Instance.pauseEffect(true);

            }
            else
            {
                canvasGroup.interactable = true; 
                canvasGroup.blocksRaycasts = true; 
                canvasGroup.alpha = 1f;
                gameManager.Instance.pauseEffect(false);

            }
        }
    }
}
