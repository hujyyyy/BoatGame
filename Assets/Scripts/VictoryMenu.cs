using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
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
        if (gameManager.Instance.victory)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            gameManager.Instance.VictoryEffect(true);

            if (Input.GetKey(KeyCode.R))
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                gameManager.Instance.VictoryEffect(false);
                gameManager.Instance.gameover = false;
                gameManager.Instance.victory = false;
                SceneManager.LoadScene("Main");

            }
        }
    }
}
