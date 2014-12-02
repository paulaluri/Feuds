﻿using UnityEngine;
using System.Collections;

public class UICharacter : MonoBehaviour {
	public Texture Health;
	public Texture Wound;
    public Texture Icon;
	public MeshRenderer selection;
	public InputManager inputManager;

	private CombatController combat;

	// Use this for initialization
	void Start () {
		combat = gameObject.GetComponent<CombatController> ();
        selection.enabled = false;
		inputManager = FindObjectOfType<InputManager> ();
	}
	
	// Update is called once per frame
	void Update () {
			if (inputManager.selectedCharacters.Contains (gameObject)) {
			//Draw selection circle
			selection.enabled = true;
            //print(gameObject);
		}
		else {
			selection.enabled = false;
            //print("?" + gameObject);
		}
	}

	void OnGUI(){
		if (!combat.isDead) {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            if (renderers[0].enabled || renderers[renderers.Length-1].enabled)
            {
                Vector3 screen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2.2f, 0));

                GUI.DrawTexture(new Rect(screen.x - 32, Screen.height - screen.y, 64, 4), Wound);
                GUI.DrawTexture(new Rect(screen.x - 32, Screen.height - screen.y, Mathf.Max(1, (combat.Health.current / combat.Health.max) * 64), 4), Health);
            }
        }
	}
}
