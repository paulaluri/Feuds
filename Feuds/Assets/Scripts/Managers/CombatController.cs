using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Damage {
	public Damage(float physical, float magic) {
		this.physical = physical;
		this.magic = magic;
	}

	public float physical;
	public float magic;
	public float total { get { return Mathf.Max(physical,0) + Mathf.Max(magic,0); } }

	public static Damage operator+(Damage atk, Damage bonus) {
		return new Damage (atk.physical + bonus.physical, atk.magic + bonus.magic);
	}

	public static Damage operator-(Damage atk, Damage def) {
		return new Damage (atk.physical - def.physical, atk.magic - def.magic );
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

	public Stat(float current, float max) {
		this.current = current;
		this.max = max;
	}

	public static Stat operator+(Stat a, Stat b) {
		return new Stat (a.current + b.current, a.max + b.max);
	}
	
	public static Stat operator-(Stat a, Stat b) {
		return new Stat (a.current - b.current, a.max - b.max );
	}
	
	public static Stat operator*(Stat a, float scalar) {
		return new Stat (scalar * a.current, scalar * a.max);
	}
	
	public static Stat operator*(float scalar, Stat a) {
		return new Stat (scalar * a.current, scalar * a.max);
	}
}

public abstract class Buff {
	public Stat ttl;
	protected abstract void Apply();
	protected abstract void Remove();
	public void Update() {
		if(!Expired()) {
			ttl.current += Time.deltaTime;
			if(Expired()) {
				Remove();
			}
		}
	}
	public bool Expired() {
		return ttl.current >= ttl.max;
	}
}

public class DamageBuff : Buff {
	Damage value;
	Damage mod;

	public DamageBuff(Damage value, Damage mod, float duration) {
		this.value = value;
		this.mod = mod;
		this.ttl = new Stat (0, duration);
		Apply ();
	}

	protected override void Apply() {
		value += mod;
	}

	protected override void Remove() {
		value -= mod;
	}
}

public class StatBuff : Buff {
	Stat value;
	Stat mod;
	
	public StatBuff(Stat value, Stat mod, float duration) {
		this.value = value;
		this.mod = mod;
		this.ttl = new Stat (0, duration);
		Apply ();
	}
	
	protected override void Apply() {
		value += mod;
	}
	
	protected override void Remove() {
		value -= mod;
	}
}

public class CombatController : MonoBehaviour {
	public Stat Health;
	public Stat MovSpeed;
	public Stat AtkSpeed;
	public Stat SkillSpeed;
	public Damage Attack;
	public Damage Defense;
    public float skillValue;
    public float Radius;
	public Class Class;

	public bool isDead { get {return Health.current <= 0;} }
	public bool inCombat = false;
	public bool inSkill = false;
	public bool attackedThisFrame = false;

	public bool hasAttackBoost = false;
	public bool hasDefenseBoost = false;
	public bool hasResistBoost = false;

	private List<Buff> buffs;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		buffs = new List<Buff> ();
		agent = GetComponent<NavMeshAgent> ();
		AtkSpeed.current = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(inCombat) {
			AtkSpeed.current += Time.deltaTime;
		}
		else {
			AtkSpeed.current = 0.0f;
		}
		SkillSpeed.current += Time.deltaTime;

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

		for(int i = buffs.Count - 1; i >= 0; i--) {
			buffs[i].Update();
			if(buffs[i].Expired()) {
				buffs.RemoveAt(i);
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
		if(networkView.isMine) {
			Health.current -= (atk - Random.Range (0.0f, 1.0f) * Defense).total;
		}
		else {
			networkView.RPC("TakeDamageN",RPCMode.OthersBuffered,atk.physical,atk.magic);
		}
	}

	[RPC]
	public void TakeDamageN(float physical, float magic) {
		Damage atk = new Damage (physical,magic);
		TakeDamage (atk);
	}

	public bool CanAttack(CombatController other) {
		return !isDead && !other.isDead && (other.transform.position - transform.position).sqrMagnitude < Radius*Radius;
	}

	public bool CanUseSkill(){
		return SkillSpeed.current >= SkillSpeed.max;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref Health.current);
		stream.Serialize (ref Health.max);
	}

	public void SetBonus(float atkScale, float physicalDef, float magicDef, bool broadcast = true) {
		Attack *= atkScale;
		Defense.physical += physicalDef;
		Defense.magic += magicDef;

		hasAttackBoost = atkScale > 1;
		hasDefenseBoost = physicalDef > 0;
		hasResistBoost = magicDef > 0;
	}

	[RPC]
	public void AddBuff(string buff, float v1, float v2, float duration) {
		if(networkView.isMine) {
			switch(buff) {
				case "Health":
					buffs.Add(new StatBuff(Health,new Stat(v1,v2),duration));
					break;
				case "MovSpeed":
					buffs.Add(new StatBuff(MovSpeed,new Stat(v1,v2),duration));
					break;
				case "AtkSpeed":
					buffs.Add(new StatBuff(AtkSpeed,new Stat(v1,v2),duration));
					break;
				case "SkillSpeed":
					buffs.Add(new StatBuff(SkillSpeed,new Stat(v1,v2),duration));
					break;
				case "Attack":
					buffs.Add(new DamageBuff(Attack,new Damage(v1,v2),duration));
					break;
				case "Defense":
					buffs.Add(new DamageBuff(Defense,new Damage(v1,v2),duration));
					break;
				default:
					break;
			}
		}
		else {
			networkView.RPC("AddBuff",RPCMode.OthersBuffered,buff,v1,v2,duration);
		}
	}
}
