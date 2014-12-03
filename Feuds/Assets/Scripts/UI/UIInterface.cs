using UnityEngine;
using System.Collections;

public class UIInterface : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle buttonStyle;
	public GUIStyle buttonSelectStyle;
	public GUIStyle buttonSpecialStyle;
	public GUIStyle timerStyle;
	public Texture attack;
	public Texture defense;
	public Texture standground;
	public Texture passive;
	public Texture health;
	public Texture wound;
	public Texture terrain;

	public InputManager inputManager;
    public GameObject cameras;

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		if(Input.GetKeyDown (KeyCode.Q))
			inputManager.SetStance(Stance.Aggressive);
		if(Input.GetKeyDown (KeyCode.W))
			inputManager.SetStance(Stance.Defensive);
		if(Input.GetKeyDown (KeyCode.E))
			inputManager.SetStance(Stance.StandGround);
		if(Input.GetKeyDown (KeyCode.R))
			inputManager.SetStance(Stance.Passive);
		if(Input.GetKeyDown (KeyCode.A))
			inputManager.clickSkill();
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

            if (GUI.Button(new Rect(8, 94, 64, 64), "", buttonSpecialStyle))
                inputManager.clickSkill();

			GUI.EndGroup();
		}
		//Selections
		GUI.BeginGroup(new Rect(310, Screen.height-192, Screen.width-(300+400), 192));

		for (int i = 0; i < inputManager.selectedCharacters.Count; i++) {
			CombatController combat = inputManager.selectedCharacters[i].GetComponent<CombatController>();

            //GUI.DrawTexture(new Rect(8 + 68 * i, 30, 64, 64), inputManager.selectedCharacters[i].GetComponent<UICharacter>().Icon);
            if (GUI.Button(new Rect(8 + 68 * i, 30, 64, 64), inputManager.selectedCharacters[i].GetComponent<UICharacter>().Icon))
            {
                //Move Camera to that character
                Vector3 pos = inputManager.selectedCharacters[i].transform.position;
                pos.x += 53;
                pos.z -= 53;
                pos.y = cameras.transform.position.y;
                cameras.transform.position = pos;
            }
            GUI.DrawTexture(new Rect(8 + 68*i,30, 64, 4), wound);
			GUI.DrawTexture(new Rect(8 + 68*i,30, (combat.Health.current/combat.Health.max)*64, 4), health);
		}

		GUI.EndGroup();


		//Minimap
		GUI.BeginGroup(new Rect(Screen.width-224, Screen.height-224, 216, 216));
		GUI.DrawTexture(new Rect(0,0,216,216), terrain);
		GUI.EndGroup();

		//Timer
		int t = Mathf.RoundToInt(GameManager.timeLeft);
		int mins = t / 60;
		int secs = t % 60;
		string min_s = mins.ToString ();
		string sec_s = secs < 10 ? "0" + secs.ToString () : secs.ToString ();
		string final_time = min_s + ":" + sec_s;
		float X_PADDING = 15;
		float Y_PADDING = 15;

		GUIContent c = new GUIContent (final_time);
		Vector2 time_size = timerStyle.CalcSize (c);
		float time_width = time_size.x;
		float line_height = time_size.y;

		GUIStyle shadow_style = new GUIStyle (timerStyle);
		shadow_style.normal.textColor = Color.black;

		GUI.Label (new Rect(Screen.width-(time_width + (-1+X_PADDING)), -1+Y_PADDING, time_width, line_height), final_time, shadow_style);
		GUI.Label (new Rect(Screen.width-(time_width + (1+X_PADDING)), -1+Y_PADDING, time_width, line_height), final_time, shadow_style);
		GUI.Label (new Rect(Screen.width-(time_width + (-1+X_PADDING)), 1+Y_PADDING, time_width, line_height), final_time, shadow_style);
		GUI.Label (new Rect(Screen.width-(time_width + (1+X_PADDING)), 1+Y_PADDING, time_width, line_height), final_time, shadow_style);

		GUI.Label (new Rect(Screen.width-(time_width+X_PADDING), Y_PADDING, time_width, line_height), final_time, timerStyle);

	}
}
