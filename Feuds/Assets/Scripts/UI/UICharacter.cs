using UnityEngine;
using System.Collections;

public class UICharacter : MonoBehaviour {
	public Texture Health;
	public Texture Wound;
	public MeshRenderer selection;
	public InputManager inputManager;

	// Use this for initialization
	void Start () {
	
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
}
