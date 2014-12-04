﻿using UnityEngine;
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

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.ready) {
			Vector3 spawnLocation;
			
			if(GameManager.player == GameManager.Rounds.current % 2) {
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
				Vector3 pos = 6 * Random.insideUnitSphere;
				pos.y = 0;
				GameObject character = Network.Instantiate(typePrefab,spawnLocation + pos,Quaternion.identity,0) as GameObject;
				CombatController combat = character.GetComponent<CombatController>();

				float atkScale = unit.BoostAttack ? BonusDamageScale : 1.0f;
				float physicalDef = unit.BoostDefense ? BonusDefense : 0.0f;
				float magicDef = unit.BoostResist ? BonusResist : 0.0f;

				combat.SetBonus(atkScale,physicalDef,magicDef);
			}
			Destroy(gameObject);
		}
	}
}
