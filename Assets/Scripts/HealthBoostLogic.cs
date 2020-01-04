using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostLogic : MonoBehaviour
{
    public float healthpoint;//0-100
    public float boostpoint;//0-100

    [SerializeField]
    private float damageByCanno;

    // Start is called before the first frame update
    void Start()
    {
        healthpoint = 100;

        boostpoint = 100;
    }

    public void loseHealth() {
        healthpoint -= damageByCanno;
    }
}
