using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Patrol", menuName = "AI/Actions/Patrol")]
public class PatrolAction : AIAction
{
	public override void Act(StateController controller)
	{
		Patrol(controller);
	}

	private void Patrol(StateController controller)
	{
		//enemy wander around waiting to see the player
		Vector3 point = Random.insideUnitSphere * controller.enemySpecs.speed;
		point.y = 0f;

//		Transform t = new Transform ();
//		t.position = point;
//		controller.enemy._target = t;
	}
}