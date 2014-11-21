using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	public List<GameObject> selectedCharacters;

	//Selected characters attack
	public void Attack(GameObject target){
		foreach (GameObject g in selectedCharacters) {
			g.GetComponent<ActionController>().Attack(target);
		}
	}

	//Selected characters move to position
	public void MoveTo(Vector3 target){
		foreach (GameObject g in selectedCharacters) {
			g.GetComponent<ActionController>().MoveTo(target);
		}
	}

	//Selected characters change stance
	public void SetStance(Stance stance){
		foreach (GameObject g in selectedCharacters) {
			g.GetComponent<ActionController>().CurrentStance = stance;
		}
	}

	//Return common stance or Stance.None
	public Stance GetCommonStance(){
		Stance initStance = Stance.None;

		for(int i = 0; i < selectedCharacters.Count; i++) {
			/*if(selectedCharacters[i]==null)
				Debug.Log ("selectedChar is null");
			else if(selectedCharacters[i].GetComponent<ActionController>() == null)
				Debug.Log ("No Action Controller");*/

			Stance charStance = selectedCharacters[i].GetComponent<ActionController>().CurrentStance;

			if(i == 0)
				initStance = charStance;
			else{
				if(charStance != initStance)
					return Stance.None;
			}
		}

		return initStance;
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
