using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "AI/Actions/Chase")]
public class ChaseAction : AIAction {

	public override void Act(StateController controller)
	{
		Chase (controller);
	}

	private void Chase(StateController controller)
	{
		//chase target
		//controller.enemy.destination = controller.chaseTarget.position;
	}
}
