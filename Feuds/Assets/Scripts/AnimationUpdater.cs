using UnityEngine;
using System.Collections;

public class AnimationUpdater : MonoBehaviour {
	private Animator anim;
	private ActionController actionController;

	private int speedIdx;
	private int isDeadIdx;
	private int inCombatIdx;
	private int inSkillIdx;

	private float speed = 0.0f;
	private bool isDead = false;
	private bool inCombat = false;
	private bool inSkill = false;
	public Vector3 targetPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		actionController = GetComponent<ActionController> ();

		speedIdx = Animator.StringToHash ("speed");
		isDeadIdx = Animator.StringToHash ("is_dead");
		inCombatIdx = Animator.StringToHash ("is_combat");
		//inSkillIdx = Animator.StringToHash ("is_skill");
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine) {
			speed = actionController.agent.velocity.magnitude;
			isDead = actionController.myCombat.isDead;
			inCombat = actionController.myCombat.inCombat;
			inSkill = actionController.myCombat.inSkill;
			if(actionController.targetCombat) {
				targetPos = actionController.targetCombat.transform.position;
			}
		}

		anim.SetFloat (speedIdx, speed);
		anim.SetBool (isDeadIdx, isDead);
		anim.SetBool (inCombatIdx, inCombat);
		//anim.SetBool (inSkillIdx, inSkill);
	}

	[RPC]
	public void UseSkill(){
		anim.SetTrigger ("use_skill");
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref speed);
		stream.Serialize (ref isDead);
		stream.Serialize (ref inCombat);
		//stream.Serialize (ref inSkill);
		stream.Serialize (ref targetPos);
	}
}
