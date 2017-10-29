using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "AI/Decisions/Look")]
public class LookDecision : AIDecision {
	

	override public bool Decide(StateController controller)
	{
		bool targetVisible = Look (controller);
		return targetVisible;
	}

	private bool Look(StateController controller)
	{
		RaycastHit hit;

		Debug.DrawRay (controller.sight.position, controller.sight.forward.normalized * controller.enemySpecs.sightRange, Color.green);

		if (Physics.SphereCast (controller.sight.position, 5, controller.sight.forward, out hit, controller.enemySpecs.sightRange) && hit.collider.CompareTag ("Player")) {
			controller.chaseTarget = hit.transform;
			return true;
		} else {
			return false;
		}
	}
}
