using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FSMState {

	public Color sightColor;
	private EnemyFSM enemyFSM;

	abstract public void Enter (Enemy enemy);
	abstract public void Execute (Enemy enemy);
	abstract public void Exit (Enemy enemy);

}