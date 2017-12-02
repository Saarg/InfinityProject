using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMode : GameMode {

	public TriggerEvent start;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public void StartDungeon() {
		allowCoop = false;
	}

	public void AllowCoop() {
		allowCoop = true;
	}
}
