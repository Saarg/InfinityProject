using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FSMState {

	public Color sightColor;

	abstract public void Enter ();
	abstract public void Execute (Enemy enemy);
	abstract public void Exit ();

}