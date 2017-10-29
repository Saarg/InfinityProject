using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITransition
{
	public AIDecision decision;
	public State trueState;  //if the decision return true
	public State falseState; //if the decision return false
}

