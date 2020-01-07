using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    public float speed;
    public Vector3 dir;
    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=  (dir * speed * Time.deltaTime);
    }

    public void setDir(Vector3 thedir) {
        dir.x = thedir.x;
        dir.y = thedir.y;
        dir.z = thedir.z;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") {
            FindObjectOfType<AudioManager>().play("getShot");
            other.gameObject.GetComponent<HealthBoostLogic>().loseHealth();
        }
        if(other.tag=="Player"||other.tag=="Island"||other.tag=="Boundary") Destroy(gameObject);
    }


}
