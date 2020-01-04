using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //is the boat in boosting mode or not
    private bool isBoosting;
    [SerializeField] private float boostingScale = 3;
    private HealthBoostLogic m_heathboost;

    //steering left(-1) t0 right(1)
    private float steering;
    //rowing rate in Hz/rows per second
    private float rowingRate;
    //distance per rowing
    [SerializeField] private float rowingDis;

    private Vector3 pose;

    private float turnSpeed = 1.0f;
    private Rigidbody rb;

    private Animator m_animator;
    private bool isdead;
    

    // Start is called before the first frame update
    void Start()
    {
        m_heathboost = GetComponent<HealthBoostLogic>();
        isBoosting = false;
        rowingRate = 1;
        rb = GetComponent<Rigidbody>();
        pose = Vector3.zero;
        m_animator = GetComponent<Animator>();
        isdead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isdead) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        m_animator.SetFloat("rowing", verticalInput);
        m_animator.SetFloat("steering", horizontalInput);

        float straightMovement = Input.GetAxis("Horizontal") * rowingRate * Time.fixedDeltaTime;

        //Debug.Log(transform.rotation.eulerAngles);
        float angle = horizontalInput * 1 + transform.rotation.eulerAngles.y;
        //if (horizontalInput != 0)
        //{
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, angle,0), Time.fixedDeltaTime * turnSpeed);
        //}
        transform.rotation = Quaternion.Euler(90, angle, 0);


        //Debug.Log(horizontalInput);
        //Debug.Log(transform.rotation.eulerAngles);

        pose.z = Mathf.Cos(Mathf.PI * transform.rotation.eulerAngles.y / 180);
        pose.x = Mathf.Sin(Mathf.PI * transform.rotation.eulerAngles.y / 180);

        float scale = 1;
        if (isBoosting&& verticalInput>0) scale *= boostingScale;

        if (verticalInput != 0) { 
            rb.AddForce(scale * rowingDis * pose * verticalInput);
        }
    }

    private void Update()
    {
        if (!isdead)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (m_heathboost.boostpoint > 0)
                {
                    isBoosting = true;
                    m_heathboost.boostpoint -= 0.2f;
                }
                else isBoosting = false;
            }
            else
            {
                isBoosting = false;
                m_heathboost.boostpoint = Mathf.Min(100, m_heathboost.boostpoint + 0.3f);
            }
        }

        if (m_heathboost.healthpoint < 0) {
            isdead = true;
            m_animator.SetBool("isDead", true);
        }
    }

 
}
