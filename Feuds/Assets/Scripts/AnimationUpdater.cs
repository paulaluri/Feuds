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

	private float speed = 0.0f;
	private bool isDead = false;
	private bool inCombat = false;
	private bool inSkill = false;

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
		if(networkView.isMine) {
			speed = agent.velocity.magnitude;
			isDead = attacker.isDead;
			inCombat = attacker.inCombat;
			inSkill = attacker.inSkill;
		}

		anim.SetFloat (speedIdx, speed);
		anim.SetBool (isDeadIdx, isDead);
		anim.SetBool (inCombatIdx, inCombat);
		anim.SetBool (inSkillIdx, inSkill);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref speed);
		stream.Serialize (ref isDead);
		stream.Serialize (ref inCombat);
		stream.Serialize (ref inSkill);
	}
}
