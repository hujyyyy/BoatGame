using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOSC : MonoBehaviour
{
    public Receiving receiving;

    [SerializeField] private GameObject booster;
    [SerializeField] private float boostingScale = 3;
    [SerializeField] private float boostingConsume;
    [SerializeField] private float boostingRecover;
    private HealthBoostLogic m_heathboost;


    //steering left(-1) t0 right(1)
    private float steering;
    //rowing rate in Hz/rows per second
    private float rowingRate;
    //distance per rowing
    [HideInInspector]
    public float rowingDis;
    //is the boat in boosting mode or not
    private bool isBoosting;

    private Vector3 pose;

    public float turnSpeed = 1.0f;
    public float movingSpeed = 1.0f;
    public float rowingSpeed = 0.5f;

    private Rigidbody rb;

    private Animator m_animator;
    private Animator m_booster_anim;
    private bool isdead;

    private bool deathCheckFlag;//can only be changed once

    // Start is called before the first frame update
    void Start()
    {
        m_heathboost = GetComponent<HealthBoostLogic>();
        rb = GetComponent<Rigidbody>();
        pose = Vector3.zero;
        m_animator = GetComponent<Animator>();
        m_booster_anim = booster.GetComponent<Animator>();
        isdead = false;
        deathCheckFlag = true;

        steering = 0;
        rowingRate = 0;
        rowingDis = 0;
        isBoosting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isdead) return;

        m_animator.SetFloat("rowing", rowingRate);
        m_animator.speed = rowingRate * rowingSpeed;
        if (m_animator.speed < 0.01f) m_animator.speed = 1;
        m_animator.SetFloat("steering", steering);
        m_booster_anim.SetBool("isBoosting", isBoosting);


        //Debug.Log(transform.rotation.eulerAngles);
        float angle = steering * turnSpeed + transform.rotation.eulerAngles.y;
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
        if (isBoosting && rowingRate > 0) scale *= boostingScale;

        if (rowingRate != 0)
        {
            rb.AddForce(scale * rowingDis * pose * rowingRate);
        }
    }

    private void Update()
    {

        if (!isdead)
        {
            ReceivingFromOSC();
            if (isBoosting)
            {
                if (m_heathboost.boostpoint > 0)
                {
                    m_heathboost.boostpoint -= boostingConsume;
                }

            }
            else
            {
                m_heathboost.boostpoint = Mathf.Min(100, m_heathboost.boostpoint + boostingRecover);
            }

            if (m_heathboost.healthpoint <= 0)
            {
                isdead = true;
                m_animator.SetBool("isDead", true);
            }
        }
        else
        {
            if (m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "sunk" && deathCheckFlag)
            {
                gameManager.Instance.gameover = true;
                deathCheckFlag = false;
            }
        }
    }
    private void ReceivingFromOSC() {
        steering = receiving.steering;
        if (Mathf.Abs(steering) < 0.1f) steering = 0;
        rowingRate = receiving.rowingRate;
        rowingDis = receiving.disPerRow;
        isBoosting = receiving.isBoosting;
    }

}
