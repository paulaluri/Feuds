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
	private Action[] CommandActions;
	private Action IdleAction;
	private Action[] DummyActions = new Action[0];
	private LayerMask EnemyLayerMask;

	private Stance _currentStance;
	public Stance CurrentStance {
		get { return _currentStance; }
		set {
			IdleAction = null;
			_currentStance = value;
		}
	}
	
	// Use this for initialization
	void Start () {
		CommandActions = DummyActions;
		CurrentStance = Stance.Aggressive;
		if(gameObject.layer == LayerMask.NameToLayer("Player1")) {
			EnemyLayerMask = LayerMask.GetMask("Player2");
		}
		else {
			EnemyLayerMask = LayerMask.GetMask("Player1");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(DoActions (CommandActions)) {
			CommandActions = DummyActions;
			if(IdleAction == null || IdleAction.Update()) {
				switch (CurrentStance) {
					case Stance.Aggressive:
					case Stance.Defensive:
					case Stance.StandGround:
						Collider[] enemies = Physics.OverlapSphere(transform.position,30.0f,EnemyLayerMask);
						if(enemies.Length > 0) {
							float d = float.MaxValue;
							GameObject closest = null;
							foreach(Collider enemy in enemies) {
								float nd = (transform.position - enemy.transform.position).magnitude;
								if(nd < d) {
									d = nd;
									closest = enemy.gameObject;
								}
							}
							IdleAction = new Attack(closest,CurrentStance != Stance.StandGround);
							IdleAction.Start(gameObject);
						}
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
		IdleAction = null;
		this.CommandActions = actions;
		foreach(Action action in CommandActions) {
			action.Start(gameObject);
		}
	}
}
