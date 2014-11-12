using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	// For each character
	public float hp;
	public float attackPower;
	public float speed;
	public string characterName;
	public int side;
	public int type;
	public int currentBehavior;
	
	Vector3 target;
	
	public const int MOVE = 1;
	public const int ATTACK = 2;
	public const int STAY = 3;
	
	NavMeshAgent nA;
	
	// Use this for initialization
	void Start () {
		nA = GetComponent<NavMeshAgent>();
		animation.CrossFade("Guard_looking_around");
		target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Debug.DrawLine(transform.position,target);
			BehaviorSet(MOVE,target);
		}
		if(currentBehavior == ATTACK){
			
		}
		else if(nA.velocity.magnitude>3f){
			animation.CrossFade("Guard_run");
		}
		else if(nA.velocity.magnitude>0.1f){
			animation.CrossFade("Guard_walk");
		}
		else {
			animation.CrossFade("Guard_looking_around");
		}
	}
	
	//Behavior will have priority...
	//The higher priority will take over the lower one
	//For now, the highest is "Player-command" --> 100
	//Attacking enemy in range is 50(Type of enemy?)
	public void BehaviorSet(int command,Vector3 target){
		currentBehavior = command;
		switch(command){
		case MOVE:
			nA.SetDestination(target);
			break;
		case ATTACK:
			break;
		case STAY:
			break;
		}
	}
}
