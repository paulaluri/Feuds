using UnityEngine;
using System.Collections;

public class UIInterface : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle buttonStyle;
	public GUIStyle buttonSelectStyle;
	public Texture attack;
	public Texture defense;
	public Texture standground;
	public Texture passive;
	public Texture guard;
	public Texture health;
	public Texture wound;
	public Texture terrain;

	public InputManager inputManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		//Background
		style.border.left = Screen.width;
		style.border.right = 0;
		style.border.top = 1;
		GUI.Label (new Rect(0, Screen.height - 192, Screen.width, 192), "", style);

		if(inputManager.selectedCharacters.Count > 0){
			//Buttons
			GUI.BeginGroup(new Rect(0, Screen.height-192, 300, 192));

			Stance common = inputManager.GetCommonStance ();

			if (common == Stance.Aggressive)
				GUI.Button (new Rect (8, 30, 64, 64), attack, buttonSelectStyle);
			else if (GUI.Button (new Rect (8, 30, 64, 64), attack, buttonStyle))
				inputManager.SetStance (Stance.Aggressive);

			if (common == Stance.Defensive)
				GUI.Button (new Rect (72,30, 64, 64), defense, buttonSelectStyle);
			else if (GUI.Button (new Rect(72,30, 64, 64), defense, buttonStyle))
				inputManager.SetStance (Stance.Defensive);

			if (common == Stance.StandGround)
				GUI.Button (new Rect (136,30, 64, 64), standground, buttonSelectStyle);
			else if (GUI.Button (new Rect(136,30, 64, 64), standground, buttonStyle))
				inputManager.SetStance (Stance.StandGround);

			if (common == Stance.Passive)
				GUI.Button (new Rect (200, 30, 64, 64), passive, buttonSelectStyle);
			else if (GUI.Button (new Rect (200, 30, 64, 64), passive, buttonStyle))
				inputManager.SetStance (Stance.Passive);

			//GUI.Button (new Rect(8,100, 64, 64), "Skill 1");
			//GUI.Button (new Rect(78,100, 64, 64), "Skill 2");
			//GUI.Button (new Rect(148,100, 64, 64), "Skill 3");
			//GUI.Button (new Rect(218,100, 64, 64), "Skill 4");

			GUI.EndGroup();
		}
		//Selections
		GUI.BeginGroup(new Rect(310, Screen.height-192, Screen.width-(300+400), 192));

		for (int i = 0; i < inputManager.selectedCharacters.Count; i++) {
			CombatController combat = inputManager.selectedCharacters[i].GetComponent<CombatController>();

			GUI.DrawTexture(new Rect(8 + 68*i,30, 64, 64), guard);
			GUI.DrawTexture(new Rect(8 + 68*i,30, 64, 4), wound);
			GUI.DrawTexture(new Rect(8 + 68*i,30, (combat.Health.current/combat.Health.max)*64, 4), health);
		}

		GUI.EndGroup();


		//Minimap
		GUI.BeginGroup(new Rect(Screen.width-224, Screen.height-224, 216, 216));
		GUI.DrawTexture(new Rect(0,0,216,216), terrain);
		GUI.EndGroup();
	}
}
