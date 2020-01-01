using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    [SerializeField]private float threshold = 5;
    [SerializeField] private float Xlimit;
    [SerializeField] private float Ylimit;

    private Vector3 pos_diff;
    private bool reach_limit;

    Vector3 tmp_pos;
    // Start is called before the first frame update
    void Start()
    {
        pos_diff = target.transform.position - transform.position;
        reach_limit = false;
    }

    // Update is called once per frame
    void Update()
    {
        reach_limit = false;
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > threshold || Mathf.Abs(target.transform.position.y - transform.position.y) > threshold)
        {
            transform.position = target.transform.position - pos_diff;
        }
        else {
            pos_diff = target.transform.position - transform.position;
        }

        tmp_pos = transform.position;
        
        if (transform.position.x <= -Xlimit || transform.position.x >= Xlimit) {
            tmp_pos.x = Mathf.Sign(transform.position.x)*Xlimit;
            reach_limit = true;
        }

        if (transform.position.z <= -Ylimit || transform.position.z >= Ylimit)
        {
            tmp_pos.z = Mathf.Sign(transform.position.z) * Ylimit;
            reach_limit = true;
        }
        if (reach_limit)
        {
            transform.position = tmp_pos;
        }
    }
}
