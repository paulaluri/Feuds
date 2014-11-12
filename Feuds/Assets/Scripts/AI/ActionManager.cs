using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// An explicit behavior tree with states
public class ActionManager : MonoBehaviour {
	public Action[] Actions { get; private set; }
	private int actionIdx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(actionIdx < Actions.Length && Actions[actionIdx].Update()) {
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
