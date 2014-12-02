using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Command{
	Attack,
	Move,
    UseSkill,
	None
}

public enum Stance{
	Aggressive,
	Defensive,
	StandGround,
	Passive,
	None
}

// An explicit behavior tree with states
public class ActionController : MonoBehaviour {
	public LayerMask attackables;

	public NavMeshAgent agent;
	public CombatController myCombat;
	public Vector3 position;

	public CombatController targetCombat;

	private Action[] CommandActions = new Action[] {
		new Sequencer(new Pursue(), new Rotate(), new Attack(), new Move()),
		new Move(),
        new UseSkill(),
		new Idle()
	};
	private Action[] IdleAction = new Action[] {
		new Sequencer(new GetTarget (), new Pursue (), new Rotate(), new Attack ()),
		new Sequencer(new Selector(new GetTarget(), new Move()),
		              new Sequencer(new HasTarget(), new Pursue(), new Rotate(), new Attack())),
		new Sequencer(new GetTarget(), new Rotate(), new Attack()),
		new Idle()
	}; 

	public Stance CurrentStance;
	public Command CurrentCommand;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		myCombat = GetComponent<CombatController> ();
		position = transform.position;
		targetCombat = null;

		CurrentCommand = Command.None;

		foreach(Action action in CommandActions) {
			action.Start(gameObject);
		}
		foreach(Action action in IdleAction) {
			action.Start(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(CommandActions[(int)CurrentCommand].Update()) {
			CurrentCommand = Command.None;
			IdleAction[(int)CurrentStance].Update();
		}
	}

	// Do the actions in order
	public void Attack(GameObject g) {
		CurrentCommand = Command.Attack;
		targetCombat = g.GetComponent<CombatController> ();
	}

	public void MoveTo(Vector3 pos) {
		CurrentCommand = Command.Move;
		position = pos;
	}

    public void UseSkill()
    {
        CurrentCommand = Command.UseSkill;
    }

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		enabled = networkView.isMine;
		//Let the GameManager add the character itself
		GameManager.characters [gameObject.layer - 10].Add (gameObject);
	}
}
