using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyDisplay : MonoBehaviour {

	[Header("default color")]
	[SerializeField]
	private ColorBlock def;
	[Header("selected color")]
	[SerializeField]
	private ColorBlock selected;
	 
	[SerializeField]
	private Button easy;
	[SerializeField]
	private Button normal;
	[SerializeField]
	private Button hard;
	[SerializeField]
	private Button insane;

	void Update () {
		switch (GameMode.difficulty) {
		case Difficulty.Easy:
			easy.colors = selected;
			normal.colors = def;
			hard.colors = def;
			insane.colors = def;
			break;
		case Difficulty.Normal:
			easy.colors = def;
			normal.colors = selected;
			hard.colors = def;
			insane.colors = def;
			break;
		case Difficulty.Hard:
			easy.colors = def;
			normal.colors = def;
			hard.colors = selected;
			insane.colors = def;
			break;
		case Difficulty.Insane:
			easy.colors = def;
			normal.colors = def;
			hard.colors = def;
			insane.colors = selected;
			break;
		}
	}
}
