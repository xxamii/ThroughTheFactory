using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWalk : StateMachineBehaviour
{
    [SerializeField] private float _minWalkTime, _maxWalkTime;
    private float _walkTimer;
    private SoldierMovement _movement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _movement = animator.GetComponent<SoldierMovement>();
        _walkTimer = Random.Range(_minWalkTime, _maxWalkTime);

        _movement.StartWalking();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _walkTimer -= Time.deltaTime;

        if (_walkTimer <= 0)
        {
            animator.SetBool("IsWalking", false);
            _movement.StopWalking();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
