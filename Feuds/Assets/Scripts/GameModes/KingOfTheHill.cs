using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingOfTheHill : GameMode {
	
	public float Speed;

	public GameObject Attack;
	public GameObject Defense;

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

		Attack.renderer.material.color = (attacker == GameManager.player ? Good : Bad);
		Defense.renderer.material.color = (attacker == GameManager.other ? Good : Bad);

		msg = FindObjectOfType<UIMessage> ();
	}
	
	// Update is called once per frame
	void Update () {
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
				percent = Mathf.Min(Speed * Time.deltaTime + percent,100.0f);
			}
		}
		Vector3 scale = new Vector3 (percent / 100.0f, 1.0f, percent / 100.0f);
		Attack.transform.localScale = scale;
	}

	void OnTriggerEnter(Collider other) {
		units [other.gameObject.layer - 10].Add (other);
		if(GameManager.player == defender
			&& units[attacker].Count == 1 && units[defender].Count == 0) {
			msg.Add("Base under attack!");
		}
	}

	void OnTriggerExit(Collider other) {
		Debug.Log (LayerMask.LayerToName (other.gameObject.layer));
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
