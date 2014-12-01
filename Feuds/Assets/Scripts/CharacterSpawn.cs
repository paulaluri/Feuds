using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawn : MonoBehaviour {

	public GameObject[] GuardPrefab;
	public GameObject[] ArcherPrefab;
	public GameObject[] WizardPrefab;
	
	public float BonusDamageScale;
	public float BonusDefense;
	public float BonusResist;

	public List<LoungeCharacter> units;

	private bool ready = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(ready && Application.loadedLevelName.StartsWith ("game")) {
			Vector3 spawnLocation;
			
			if(GameManager.player == GameManager.round % 2) {
				spawnLocation = GameObject.Find("AttackSpawn").transform.position;
			}
			else {
				spawnLocation = GameObject.Find("DefenseSpawn").transform.position;
			}
			
			foreach(LoungeCharacter unit in units) {
				GameObject typePrefab;
				switch (unit.Type) {
				case CharacterType.Guard:
					typePrefab = GuardPrefab[GameManager.player];
					break;
				case CharacterType.Archer:
					typePrefab = ArcherPrefab[GameManager.player];
					break;
				default:
					typePrefab = WizardPrefab[GameManager.player];
					break;
				}
				Vector3 pos = 10 * Random.insideUnitSphere;
				pos.y = 0;
				GameObject character = Network.Instantiate(typePrefab,spawnLocation + pos,Quaternion.identity,0) as GameObject;
				CombatController combat = character.GetComponent<CombatController>();

				if(unit.BoostAttack) {
					combat.Attack *= BonusDamageScale;
				}
				if(unit.BoostDefense) {
					combat.Defense.physical += BonusDefense;
				}
				if(unit.BoostResist) {
					combat.Defense.magic += BonusResist;
				}
			}
			Destroy(gameObject);
		}
	}

	public void Ready() {
		networkView.RPC ("OtherReady",RPCMode.OthersBuffered);
	}

	[RPC]
	public void OtherReady() {
		ready = true;
	}
}
