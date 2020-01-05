using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
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
        if (gameManager.Instance.gameover&& !gameManager.Instance.victory) {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            gameManager.Instance.EndEffect(true);

            if (Input.GetKey(KeyCode.R))
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                gameManager.Instance.EndEffect(false);
                gameManager.Instance.gameover = false;
                SceneManager.LoadScene("Main");

            }
        }
    }
}
