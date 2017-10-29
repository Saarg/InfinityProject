using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveState", menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : AIDecision {

	public override bool Decide(StateController controller)
	{
		bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
		return chaseTargetIsActive;
	}
}
