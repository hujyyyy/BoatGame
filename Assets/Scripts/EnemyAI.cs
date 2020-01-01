﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{ 
    Patrol,
    Attack
}


public class EnemyAI : MonoBehaviour
{
    public EnemyState m_state;
    private NavMeshAgent m_navAgent;
    private int idx_WayPoints;

    private Animator m_anim;

    private GameObject m_player;
    public float attacting_radius = 2;

    public List<GameObject> WayPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_state = EnemyState.Patrol;
        m_navAgent = GetComponent<NavMeshAgent>();
        idx_WayPoints = 0;
        m_navAgent.SetDestination(WayPoints[0].transform.position);

        m_anim = GetComponent<Animator>();

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state) {
            case EnemyState.Patrol:
                m_anim.SetBool("isSailing", true);

                if ((transform.position - m_player.transform.position).magnitude <= attacting_radius) {
                    m_state = EnemyState.Attack;
                    m_navAgent.isStopped = true;
                    break;
                }

                if (m_navAgent.remainingDistance <= 0.05f) {
                    setNextWayPoint();
                }
                break;
            case EnemyState.Attack:
                m_anim.SetBool("isSailing", false);
                if ((transform.position - m_player.transform.position).magnitude > attacting_radius)
                {
                    m_state = EnemyState.Patrol;
                    m_navAgent.isStopped = false;
                    m_navAgent.SetDestination(WayPoints[idx_WayPoints].transform.position);
                    break;
                }
                break;
        
        
        }
        correctRotation();

    }



    void setNextWayPoint() {
        if (idx_WayPoints == WayPoints.Capacity - 1) idx_WayPoints = 0;
        else idx_WayPoints++;

        m_navAgent.SetDestination(WayPoints[idx_WayPoints].transform.position);


    }

    void correctRotation() {
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(transform.position, attacting_radius);
    }
}
