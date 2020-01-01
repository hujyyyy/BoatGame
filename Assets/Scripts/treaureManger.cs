using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treaureManger : MonoBehaviour
{
    [SerializeField] static int max_treasure_count = 5;
    [SerializeField] List<GameObject> treasureUI = new List<GameObject>();
    private int treasure_count;
    // Start is called before the first frame update
    void Start()
    {
        treasure_count = 0;
    }

    public void getNewTreasure() {
        if (treasure_count < max_treasure_count) { 
            treasureUI[treasure_count++].SetActive(false);
            treasureUI[treasure_count].SetActive(true);
        }
    }
}
