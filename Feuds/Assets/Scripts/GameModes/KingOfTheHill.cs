using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingOfTheHill : GameMode {

	public float Radius;
	public float Speed;

	public GameObject Attack;
	public GameObject Defense;

	public Color Good;
	public Color Bad;

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

	public int[] unitCount = new int[2] {0,0};

	// Use this for initialization
	void Start () {
		percent = 0.0f;
		players = 1 << attackerLayer | 1 << defenderLayer;

		Attack.renderer.material.color = (attacker == GameManager.player ? Good : Bad);
		Defense.renderer.material.color = (attacker == GameManager.other ? Good : Bad);
	}
	
	// Update is called once per frame
	void Update () {
		if(networkView.isMine) {

			if(unitCount[defender] == 0 && unitCount[attacker] > 0) {
				percent = Mathf.Min(Speed * Time.deltaTime + percent,100.0f);
			}
		}
		Vector3 scale = new Vector3 (percent / 100.0f, 1.0f, percent / 100.0f);
		Attack.transform.localScale = scale;
	}

	void OnTriggerEnter(Collider other) {
		unitCount [other.gameObject.layer - 10]++;
	}

	void OnTriggerExit(Collider other) {
		unitCount [other.gameObject.layer - 10]--;
	}

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		GameManager.game = this;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref percent);
	}
}
