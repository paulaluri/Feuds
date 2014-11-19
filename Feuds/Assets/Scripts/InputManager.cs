using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	public GameManager gameManager;
	public List<GameObject> selectedCharacters;

	//Selected characters attack
	public void Attack(GameObject target){
		foreach (GameObject g in selectedCharacters) {
			g.GetComponent<ActionController>().Do(new Attack(target));
		}
	}

	//Selected characters move to position
	public void MoveTo(Vector3 target){
		foreach (GameObject g in selectedCharacters) {
			g.GetComponent<ActionController>().Do(new MoveTo(target));
		}
	}

	//Selected characters change stance
	public void SetStance(Stance stance){
		foreach (GameObject g in selectedCharacters) {
			g.GetComponent<ActionController>().CurrentStance = stance;
		}
	}

	//TODO: Not implemented yet
	//Selected characters set skill
	public void SetSkill(){
		foreach (GameObject g in selectedCharacters) {
			//g.GetComponent<ActionController>()
		}
	}

	//Allow UI to select characters
	public void SelectCharacters(List<GameObject> characters){
		selectedCharacters = characters;
	}
}
