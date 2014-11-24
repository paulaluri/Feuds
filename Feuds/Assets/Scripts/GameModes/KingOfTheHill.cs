using UnityEngine;
using System.Collections;

public class KingOfTheHill : GameMode {
	public float Radius;
	public float Speed;
	public LayerMask Player1;
	public LayerMask Player2;

	private float percent;
	private LayerMask players;

	public override int Winner {
		get {
			if(percent >= 100.0f) {
				return 1;
			}
			else if(GameManager.playerCharacters.Count == 0) {
				return 2;
			}
			else {
				return 0;
			}
		}
	}

	// Use this for initialization
	void Start () {
		percent = 0.0f;
		players = Player1 | Player2;
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine) {
			Collider[] colliders = Physics.OverlapSphere(transform.position,Radius);
			int attackers = 0;
			int defenders = 0;
			foreach(Collider col in colliders) {
				if(col.gameObject.layer == Player1) {
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
