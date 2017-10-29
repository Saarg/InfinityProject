using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "AI/State")]
public class State : ScriptableObject
{
	public AIAction[] actions;
	public AITransition[] transitions;
	public Color sceneGuizmoColor = Color.grey;

	public void UpdateState(StateController controller)
	{
		DoActions(controller);
		CheckTransitions (controller);
	}

	private void DoActions (StateController controller)
	{
		for (int i = 0; i < actions.Length; i++)
		{
			actions[i].Act(controller);
		}
	}

	//evaluate each decision of the State and store it in decisionSucceeded
	private void CheckTransitions(StateController controller)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool decisionSucceeded = transitions [i].decision.Decide (controller);

			if (decisionSucceeded) {
				controller.TransitionToState (transitions [i].trueState);
			} else {
				controller.TransitionToState (transitions [i].falseState);
			}
		}

	}
}