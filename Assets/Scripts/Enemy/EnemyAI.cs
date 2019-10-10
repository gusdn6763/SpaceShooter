using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.PATROL;

    private Transform playerTr;

    private Transform enemyTr;

    private Animator animator;

    private WaitForSeconds ws;

    private MoveAgent moveAgent;

    private EnemyFire enemyFire;

    public float attackDist = 5.0f;
    public float traceDist = 10.0f;

    public bool isDie = false;

    //유니티에서 선언한 피라메터 IsMove 를 hashMove로 가져온다.
    //한번 바꾸면 전부 바꿔야 되서
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");

        if (player != null)
        {
            playerTr = player.GetComponent<Transform>();
        }
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        animator = GetComponent<Animator>();
        enemyFire = GetComponent<EnemyFire>();

        ws = new WaitForSeconds(0.3f);
        
    }


    private void OnEnable()
    {
        StartCoroutine(CheckState());

        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.PATROL:
                    enemyFire.isFire = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    if(enemyFire.isFire == false)
                    {
                        enemyFire.isFire = true;
                    }
            
                    break;
                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;
                    moveAgent.Stop();
                    animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    animator.SetTrigger(hashDie);
                    break;
            }
        }
    }


    IEnumerator CheckState()
    {
        while(!isDie)
        {
            if(state==State.DIE)
            {
                yield break;
            }

            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            
            if (dist<=attackDist)
            {
                state = State.ATTACK;
            }

            else if(dist <=traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }

            yield return ws;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }
}
