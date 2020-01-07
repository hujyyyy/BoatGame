using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostLogic : MonoBehaviour
{
    public float healthpoint;//0-100
    public float boostpoint;//0-100

    [SerializeField]
    private float damageByCanno;

    private SpriteRenderer spriteRenderer;
    private float timestart;
    [SerializeField]
    private float damageFlashTime;

    // Start is called before the first frame update
    void Start()
    {
        healthpoint = 100;
        boostpoint = 100;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.time - timestart > damageFlashTime) {
            spriteRenderer.color = new Color(1, 1, 1);
        }
    }

    public void loseHealth() {
        healthpoint -= damageByCanno;
        damageflash();
    }

    public void gainHealth() {
        healthpoint = Mathf.Min(100, healthpoint + damageByCanno);
    }

    private void damageflash() {
        timestart = Time.time;
        spriteRenderer.color = new Color(0.8f, 0.3f, 0.3f);
    }
}
