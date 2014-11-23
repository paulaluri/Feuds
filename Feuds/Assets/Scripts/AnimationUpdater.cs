using UnityEngine;
using System.Collections;

public class AnimationUpdater : MonoBehaviour {
	private Animator anim;
	private CombatController attacker;
	private NavMeshAgent agent;

	private int speedIdx;
	private int isDeadIdx;
	private int inCombatIdx;
	private int randAttack;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		attacker = GetComponent<CombatController> ();
		agent = GetComponent<NavMeshAgent> ();

		speedIdx = Animator.StringToHash ("speed");
		isDeadIdx = Animator.StringToHash ("is_dead");
		inCombatIdx = Animator.StringToHash ("is_combat");
		randAttack = Animator.StringToHash("rand");
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat (speedIdx, agent.velocity.magnitude);
		anim.SetBool (isDeadIdx, attacker.isDead);
		anim.SetBool (inCombatIdx, attacker.inCombat);

		Random.seed = Mathf.RoundToInt(Time.deltaTime * 1000);
		int rand = Random.Range (0, 2);
		anim.SetInteger (randAttack, rand);
	}
}
