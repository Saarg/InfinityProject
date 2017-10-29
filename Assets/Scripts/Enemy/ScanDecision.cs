using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScanDecision", menuName = "AI/Decisions/Scan")]
public class ScanDecision : AIDecision {

	public override bool Decide(StateController controller)
	{
		bool noEnemyInSight = Scan (controller);
		return noEnemyInSight;
	}

	private bool Scan(StateController controller)
	{
		//stop moving	
		//turning aroung, searching for player
		controller.transform.Rotate(0, controller.enemySpecs.searchTurningSpeed * Time.deltaTime, 0);
		return controller.CheckIfCountDownElapsed (controller.enemySpecs.searchDuration);
	}
}
