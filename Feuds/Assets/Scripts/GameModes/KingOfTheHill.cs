using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingOfTheHill : GameMode {
	
	public float Speed;

	public Color Good;
	public Color Bad;

	public UIMessage msg;

	private float percent;

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

	public List<Collider>[] units = new List<Collider>[2] {
		new List<Collider>(),
		new List<Collider>()
	};

	// Use this for initialization
	void Start () {
		attacker = Mathf.RoundToInt(GameManager.Rounds.current) % 2;

		percent = 0.0f;

		renderer.material.SetColor("_Col0", attacker == GameManager.player ? Good : Bad);
		renderer.material.SetColor("_Col1", attacker == GameManager.player ? Bad : Good);
	}
	
	// Update is called once per frame
	void Update () {
		if(!msg) {
			msg = FindObjectOfType<UIMessage> ();
		}
		for(int i = 0; i < units.Length; i++) {
			for(int j = units[i].Count - 1; j >= 0; j--) {
				if(!units[i][j].enabled) {
					units[i].RemoveAt(j);
					if(i == defender && units[defender].Count == 0 && units[attacker].Count > 0) {
						msg.Add("Base under attack!");
					}
				}
			}
		}
		if(networkView.isMine) {

			if(units[defender].Count == 0 && units[attacker].Count > 0) {
				percent += Speed * Time.deltaTime;
			}
		}
		renderer.material.SetFloat ("_Fill", percent / 100.0f);
	}

	void OnTriggerEnter(Collider other) {
		units [other.gameObject.layer - 10].Add (other);
		if(GameManager.player == defender
			&& units[attacker].Count == 1 && units[defender].Count == 0) {
			msg.Add("Base under attack!");
		}
	}

	void OnTriggerExit(Collider other) {
		units [other.gameObject.layer - 10].Remove(other);
		if(GameManager.player == defender
		   && other.gameObject.layer == defenderLayer
		   && units[defender].Count == 0 && units[attacker].Count > 0) {
			msg.Add("Base under attack!");
		}
	}

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		GameManager.game = this;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref percent);
	}
}
