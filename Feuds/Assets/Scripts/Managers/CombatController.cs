using UnityEngine;
using System.Collections;

[System.Serializable]
public class Damage {
	public Damage(float physical, float magic) {
		this.physical = physical;
		this.magic = magic;
	}

	public float physical;
	public float magic;
	public float total { get { return Mathf.Max(physical + magic,0.0f); } }

	public static Damage operator+(Damage atk, Damage bonus) {
		return new Damage (atk.physical + bonus.physical, atk.magic + bonus.magic);
	}

	public static Damage operator-(Damage atk, Damage def) {
		return new Damage (Mathf.Max (atk.physical - def.physical, 0.0f), Mathf.Max (atk.magic - def.magic, 0.0f));
	}

	public static Damage operator*(Damage atk, float scalar) {
		return new Damage (scalar * atk.physical, scalar * atk.magic);
	}

	public static Damage operator*(float scalar, Damage atk) {
		return new Damage (scalar * atk.physical, scalar * atk.magic);
	}
}

public enum Class {
	Guard,Archer,Magician
}

[System.Serializable]
public class Stat {
	public float current;
	public float max;
}

public class CombatController : MonoBehaviour {
	public Stat Health;
	public Stat MovSpeed;
	public Stat AtkSpeed;
	public Damage Attack;
	public Damage Defense;
    public float skillCD;
    public float startCD;
    public float skillValue;
    public float Radius;
	public Class Class;

	public bool isDead { get {return Health.current <= 0;} }
	public bool inCombat = false;
	public bool inSkill = false;
	public bool attackedThisFrame = false;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		AtkSpeed.current = 0.0f;
        startCD = -skillCD;
	}
	
	// Update is called once per frame
	void Update () {
		if(inCombat) {
			AtkSpeed.current += Time.deltaTime;
		}
		else {
			AtkSpeed.current = 0.0f;
		}
		agent.speed = MovSpeed.current;
		if(!attackedThisFrame) {
			inCombat = false;
		}
		attackedThisFrame = false;

		if(isDead) {
			enabled = false;
			collider.enabled = false;
			agent.enabled = false;
			GetComponent<ActionController>().enabled = false;
			GameManager.characters[gameObject.layer-10].Remove(gameObject);
			if(gameObject.layer != GameManager.playerLayer) {
				FindObjectOfType<UIMessage>().Add("Enemy unit killed!");
			}
		}
	}


	public void DoDamage(CombatController other) {
		if(AtkSpeed.current > AtkSpeed.max) {
			AtkSpeed.current = 0.0f;
			other.TakeDamage (Random.Range(1.0f, 1.5f) * Attack);
		}
	}
	
	public void TakeDamage(Damage atk) {
		Health.current -= (atk - Random.Range (1.0f, 1.5f) * Defense).total;
	}

	public bool CanAttack(CombatController other) {
		return !isDead && !other.isDead && (other.transform.position - transform.position).sqrMagnitude < Radius*Radius;
	}

	public bool CanUseSkill(){
		return !(Time.time - startCD < skillCD);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref Health.current);
	}

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		if(!networkView.isMine) {
			NetworkView view = gameObject.AddComponent<NetworkView> ();
			view.observed = this;
			view.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
			view.viewID = Network.AllocateViewID();
			networkView.RPC("WatchHealth",RPCMode.OthersBuffered,view.viewID);
		}
	}

	[RPC]
	public void SetBonus(float atkScale, float physicalDef, float magicDef, bool broadcast = true) {
		Attack *= atkScale;
		Defense.physical += physicalDef;
		Defense.magic += magicDef;
		if(broadcast) {
			networkView.RPC("SetBonus",RPCMode.OthersBuffered,atkScale,physicalDef,magicDef,false);
		}
	}

	[RPC]
	void WatchHealth(NetworkViewID id) {
		NetworkView view = gameObject.AddComponent<NetworkView> ();
		view.observed = this;
		view.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		view.viewID = id;
	}
}
