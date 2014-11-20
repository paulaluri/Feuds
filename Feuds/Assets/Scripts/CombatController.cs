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

	public static Damage operator-(Damage atk, Damage def) {
		return new Damage (Mathf.Max (atk.physical - def.physical, 0.0f), Mathf.Max (atk.magic - def.magic, 0.0f));
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
	public float Radius;
	public Class Class;

	public bool isDead { get {return Health.current <= 0;} }
	public bool inCombat;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		AtkSpeed.current = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		AtkSpeed.current += Time.deltaTime;
		agent.speed = MovSpeed.current;
	}


	public void DoDamage(CombatController other) {
		AtkSpeed.current = 0.0f;
		other.TakeDamage (Random.Range(1.0f, 1.5f) * Attack);
	}
	
	public void TakeDamage(Damage atk) {
		Health.current -= (atk - Random.Range (1.0f, 1.5f) * Defense).total * Random.Range (0.0f, 1.0f);
	}

	public bool CanAttack(CombatController other) {
		return AtkSpeed.current >= AtkSpeed.max && (other.transform.position - transform.position).sqrMagnitude < Radius*Radius;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref Health.current);
	}
}
