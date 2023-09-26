using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdle : StateMachineBehaviour
{
    [SerializeField] private float _minIdleTime, _maxIdleTime;
    private float _idleTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _idleTimer = Random.Range(_minIdleTime, _maxIdleTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _idleTimer -= Time.deltaTime;

        if (_idleTimer <= 0)
        {
            animator.SetBool("IsWalking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
