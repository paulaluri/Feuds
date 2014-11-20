﻿using UnityEngine;
using System.Collections;

public class UICharacter : MonoBehaviour {
	public Texture Health;
	public Texture Wound;
	public MeshRenderer selection;
	public InputManager inputManager;

	private CombatController combat;

	// Use this for initialization
	void Start () {
		combat = gameObject.GetComponent<CombatController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inputManager.selectedCharacters.Contains (gameObject)) {
			//Draw selection circle
			selection.enabled = true;
		}
		else {
			selection.enabled = false;
		}
	}

	void OnGUI(){
		Vector3 screen = Camera.main.WorldToScreenPoint (transform.position + new Vector3 (0, 2.2f, 0));

		GUI.DrawTexture(new Rect(screen.x - 32,Screen.height - screen.y, 64, 4), Wound);
		GUI.DrawTexture(new Rect(screen.x - 32,Screen.height - screen.y, (Mathf.Max (0, combat.Health.current)/combat.Health.max)*64, 4), Health);
	}
}
