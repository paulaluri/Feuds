using UnityEngine;
using System.Collections;

public class AnimationUpdater : MonoBehaviour {
	private Animator anim;
	private CombatController attacker;
	private NavMeshAgent agent;

	private int speedIdx;
	private int isDeadIdx;
	private int inCombatIdx;
	private int inSkillIdx;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		attacker = GetComponent<CombatController> ();
		agent = GetComponent<NavMeshAgent> ();

		speedIdx = Animator.StringToHash ("speed");
		isDeadIdx = Animator.StringToHash ("is_dead");
		inCombatIdx = Animator.StringToHash ("is_combat");
		inSkillIdx = Animator.StringToHash ("is_skill");
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat (speedIdx, agent.velocity.magnitude);
		anim.SetBool (isDeadIdx, attacker.isDead);
		anim.SetBool (inCombatIdx, attacker.inCombat);
		anim.SetBool (inSkillIdx, attacker.inSkill);
	}
}
