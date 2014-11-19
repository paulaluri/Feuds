using UnityEngine;
using System.Collections;

public class UIInterface : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle buttonStyle;
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

		//Buttons
		GUI.BeginGroup(new Rect(0, Screen.height-192, 300, 192));

		Stance common = inputManager.GetCommonStance ();

		//if (common == Stance.Aggressive)
			//Aggressive
		//else
		     GUI.Button (new Rect (8, 30, 64, 64), attack, buttonStyle);
		GUI.Button (new Rect(78,30, 64, 64), defense, buttonStyle);
		GUI.Button (new Rect(148,30, 64, 64), standground, buttonStyle);
		GUI.Button (new Rect(218,30, 64, 64), passive, buttonStyle);

		//GUI.Button (new Rect(8,100, 64, 64), "Skill 1");
		//GUI.Button (new Rect(78,100, 64, 64), "Skill 2");
		//GUI.Button (new Rect(148,100, 64, 64), "Skill 3");
		//GUI.Button (new Rect(218,100, 64, 64), "Skill 4");

		GUI.EndGroup();

		//Selections
		GUI.BeginGroup(new Rect(310, Screen.height-192, Screen.width-(300+400), 192));

		GUI.DrawTexture(new Rect(8,30, 64, 64), guard);
		GUI.DrawTexture(new Rect(8,30, 64, 4), wound);
		GUI.DrawTexture(new Rect(8,30, 48, 4), health);

		GUI.DrawTexture(new Rect(76,30, 64, 64), guard);
		GUI.DrawTexture(new Rect(76,30, 64, 4), wound);
		GUI.DrawTexture(new Rect(76,30, 22, 4), health);

		GUI.EndGroup();


		//Minimap
		GUI.BeginGroup(new Rect(Screen.width - 178, Screen.height-178, 192, 192));
		GUI.DrawTexture(new Rect(3,3,172,172), terrain);
		GUI.EndGroup();
	}
}
