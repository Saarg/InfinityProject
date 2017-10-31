using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : StateMachineBehaviour {

	protected Enemy enemy;

	abstract override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
	abstract override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
	abstract override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
