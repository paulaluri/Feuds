using UnityEngine;
using System.Collections;

public abstract class Action {
	protected ActionController ac;

	public void Start(GameObject g) {
		ac = g.GetComponent<ActionController> ();
		start (g);
	}

	// takes the gameobject performing the action
	protected virtual void start(GameObject g) { return; }
	// return true if action is done
	public abstract bool Update();
}

public class Sequencer : Action {
	private Action[] actions;

	public Sequencer(params Action[] actions) {
		this.actions = actions;
	}

	protected override void start(GameObject g) {
		foreach(Action action in actions) {
			action.Start(g);
		}
	}

	public override bool Update() {
		foreach(Action action in actions) {
			if(!action.Update()) {
				return false;
			}
		}
		return true;
	}
}

public class Selector : Action {
	private Action[] actions;
	
	public Selector(params Action[] actions) {
		this.actions = actions;
	}
	
	protected override void start(GameObject g) {
		foreach(Action action in actions) {
			action.Start(g);
		}
	}
	
	public override bool Update() {
		foreach(Action action in actions) {
			if(action.Update()) {
				return true;
			}
		}
		return false;
	}
}

public class Idle : Action {
	public override bool Update() {
		return true;
	}
}