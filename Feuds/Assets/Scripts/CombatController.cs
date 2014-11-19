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

public class CombatController : MonoBehaviour {
	public float Health;
	public float MaxHealth;
	public float Speed;
	public Damage Attack;
	public Damage Defense;
	public Class Class;

	public bool isDead { get {return Health <= 0;} }
	public bool inCombat;

	// Use this for initialization
	void Start () {
		Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void DoDamage(CombatController other) {
		other.TakeDamage (Random.Range(1.0f, 1.5f) * Attack);
	}
	
	void TakeDamage(Damage atk) {
		Health -= (atk - Random.Range (1.0f, 1.5f) * Defense).total * Random.Range (0.0f, 1.0f);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref Health);
	}
}
