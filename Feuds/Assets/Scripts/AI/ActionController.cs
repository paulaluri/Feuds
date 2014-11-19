using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Stance{
	None,
	Aggressive,
	Defensive,
	StandGround,
	Passive,
	Resting,
	Active
}

// An explicit behavior tree with states
public class ActionController : MonoBehaviour {
	public Action[] Actions { get; private set; }
	public Stance CurrentStance;

	private int actionIdx;

	// Use this for initialization
	void Start () {
		CurrentStance = Stance.Aggressive;
	}
	
	// Update is called once per frame
	void Update () {
		if(Actions != null && actionIdx < Actions.Length && Actions[actionIdx].Update()) {
			actionIdx++;
		}
	}

	// Do the actions in order
	public void Do(params Action[] actions) {
		this.Actions = actions;
		this.actionIdx = 0;
		foreach(Action action in actions) {
			action.Start(gameObject);
		}
	}
}
