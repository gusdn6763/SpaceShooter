using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    private Animator animator;
    private NavMeshAgent agent;
    private Transform enemyTr;

    public int nextIdx;

    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;
    private float damping = 1.0f;

    private bool _patrolling;

    private Vector3 _traceTarget;

    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if(_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }

    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }

    public float speed
    {
        get { return agent.velocity.magnitude; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.speed = patrolSpeed;

        var group = GameObject.Find("WayPoint");

        if (group!=null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);

            nextIdx = Random.Range(0, wayPoints.Count);
        }
        //enemy.AI 에서 정찰 상태인데 지금 스타트 함수가 먼저 실행되서 정찰 프로퍼티를 실행 해줘야함
        this.patrolling = true;
    }
    void MoveWayPoint()
    {
        if (agent.isPathStale)
        {
            return;
        }
        animator.SetBool("IsMove", true);
        agent.destination = wayPoints[nextIdx].position;

        agent.isStopped = false;
    }

    void TraceTarget(Vector3 pos)
    {
        if(agent.isPathStale)
        {
            return;
        }
        agent.destination = pos;
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(agent.isStopped==false)
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
        if(! _patrolling)
        {
            return;
        }

        if(agent.velocity.sqrMagnitude>=0.2f*0.2f && agent.remainingDistance<0.5f)
        {
            nextIdx = Random.Range(0, wayPoints.Count);

            MoveWayPoint();
        }
    }
}
