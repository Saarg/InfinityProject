using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "AI/Actions/Attack")]
public class AttackAction : AIAction
{
	public override void Act(StateController controller)
	{
		Attack (controller);
	}

	private void Attack(StateController controller)
	{
		RaycastHit hit;

		Debug.DrawRay (controller.sight.position, controller.sight.forward.normalized * controller.enemySpecs.sightRange, Color.green);

		if (Physics.SphereCast (controller.sight.position, 2, controller.sight.forward, out hit, controller.enemySpecs.attackRange) && hit.collider.CompareTag ("Player"))
		{
			if (controller.CheckIfCountDownElapsed (controller.enemySpecs.attackRate))
			{
				//fire at player
				//controller.enemy.Attack();
			}
		}
	}
}

