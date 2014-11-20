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
	public Action[] CommandActions { get; private set; }
	private Action[] IdleActions;
	private Action[] DummyActions = new Action[0];

	private Stance _currentStance;
	public Stance CurrentStance {
		get { return _currentStance; }
		set {
			IdleActions = DummyActions;
			_currentStance = value;
		}
	}
	
	// Use this for initialization
	void Start () {
		CommandActions = DummyActions;
		CurrentStance = Stance.Aggressive;
	}
	
	// Update is called once per frame
	void Update () {
		if(DoActions (CommandActions)) {
			if(DoActions (IdleActions)) {
				switch (CurrentStance) {
					case Stance.Aggressive:
					case Stance.Defensive:
					case Stance.StandGround:
						break;
					default:
						break;
				}
			}
		}
	}

	private bool DoActions(Action[] Actions) {
		foreach(Action action in Actions) {
			if(!action.Update())
				return false;
		}
		return true;
	}

	// Do the actions in order
	public void Do(params Action[] actions) {
		this.CommandActions = actions;
		foreach(Action action in CommandActions) {
			action.Start(gameObject);
		}
	}
}
