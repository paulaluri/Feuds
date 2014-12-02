using UnityEngine;
using System.Collections;

public class KingOfTheHill : GameMode {

	public float Radius;
	public float Speed;

	private float percent;
	private LayerMask players;

	public override int Winner {
		get {
			if(percent >= 100.0f) {
				return attacker;
			}
			else if(GameManager.characters[attacker].Count == 0 && GameManager.timeLeft < 290 || GameManager.timeLeft <= 0) {
				return defender;
			}
			else {
				return -1;
			}
		}
	}

	// Use this for initialization
	void Start () {
		percent = 0.0f;
		players = 1 << attackerLayer | 1 << defenderLayer;
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine) {
			Collider[] colliders = Physics.OverlapSphere(transform.position,Radius,players);
			int attackers = 0;
			int defenders = 0;
			foreach(Collider col in colliders) {
				if(col.gameObject.layer == attackerLayer) {
					attackers++;
				}
				else {
					defenders++;
				}
			}
			if(defenders == 0 && attackers > 0) {
				percent += Speed * Time.deltaTime;
			}
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref percent);
	}
}
