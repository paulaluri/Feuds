﻿using UnityEngine;
using System.Collections;

public class UIInterface : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle buttonStyle;
	public GUIStyle buttonSelectStyle;
	public GUIStyle buttonSpecialStyle;
	public GUIStyle timerStyle;
	public GUIStyle helpText;
	public Texture attack;
	public Texture defense;
	public Texture standground;
	public Texture passive;
	public Texture health;
	public Texture wound;
	public Texture terrain;
	public GUIStyle guardBtn;
	public GUIStyle wizardBtn;
	public GUIStyle archerBtn;

	public InputManager inputManager;
    public GameObject cameras;

	private UISelection uiselection;

	private Rect offsetAggr = new Rect (8, 30, 64, 64);
	private Rect offsetDef = new Rect (72,30, 64, 64);
	private Rect offsetSG = new Rect (136,30, 64, 64);
	private Rect offsetPass = new Rect (200, 30, 64, 64);

	private float btn_y_offset = Screen.height-192;

	// Use this for initialization
	void Start () {
		uiselection = gameObject.GetComponent<UISelection> ();
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
		if(Input.GetKeyDown (KeyCode.A) && uiselection.selectedCharacters.Count == 1)
			inputManager.clickSkill();
	}

	void DrawText(Rect r, string s, GUIStyle g, bool outline, int size, Color c){
		GUIStyle cp = new GUIStyle(g);
		cp.fontSize = size;
		cp.normal.textColor = c;
		
		if(outline){
			Rect br = new Rect(r.x-1, r.y-1, r.width, r.height);
			Rect ur = new Rect(r.x-1, r.y+1, r.width, r.height);
			Rect bl = new Rect(r.x+1, r.y-1, r.width, r.height);
			Rect ul = new Rect(r.x+1, r.y+1, r.width, r.height);
			
			GUIStyle shadow = new GUIStyle(cp);
			shadow.normal.textColor = Color.black;
			
			GUI.Label (br, s, shadow);
			GUI.Label (ur, s, shadow);
			GUI.Label (bl, s, shadow);
			GUI.Label (ul, s, shadow);
		}
		
		GUI.Label (r, s, cp);
	}

	// Update is called once per frame
	void OnGUI () {
		//Background
		style.border.left = Screen.width;
		style.border.right = 0;
		style.border.top = 1;
		GUI.Label (new Rect(0, Screen.height - 192, Screen.width, 192), "", style);
		btn_y_offset = Screen.height-192;

		if(inputManager.selectedCharacters.Count > 0){
			//Buttons
			GUI.BeginGroup(new Rect(0, btn_y_offset, 300, 192));

			Stance common = inputManager.GetCommonStance ();

			if (common == Stance.Aggressive)
				GUI.Button (offsetAggr, attack, buttonSelectStyle);
			else if (GUI.Button (offsetAggr, attack, buttonStyle))
				inputManager.SetStance (Stance.Aggressive);

			if (common == Stance.Defensive)
				GUI.Button (offsetDef, defense, buttonSelectStyle);
			else if (GUI.Button (offsetDef, defense, buttonStyle))
				inputManager.SetStance (Stance.Defensive);

			if (common == Stance.StandGround)
				GUI.Button (offsetSG, standground, buttonSelectStyle);
			else if (GUI.Button (offsetSG, standground, buttonStyle))
				inputManager.SetStance (Stance.StandGround);

			if (common == Stance.Passive)
				GUI.Button (offsetPass, passive, buttonSelectStyle);
			else if (GUI.Button (offsetPass, passive, buttonStyle))
				inputManager.SetStance (Stance.Passive);

			if(uiselection.selectedCharacters.Count == 1){ 
				bool canSkill = uiselection.selectedCharacters[0].GetComponent<CombatController>().CanUseSkill();
				if(canSkill){
					if (GUI.Button(new Rect(8, 94, 64, 64), "", buttonSpecialStyle))
						inputManager.clickSkill();
				}
				else{
					GUI.DrawTexture(new Rect(8, 94, 64, 64), buttonSpecialStyle.active.background);
				}
			}

			GUI.EndGroup();
		}
		//Selections
		float panel_w = Screen.width-(550);
		GUI.BeginGroup(new Rect(310, Screen.height-192, panel_w, 192));
		float px = 0;
		float py = 0;
		float p_total_w = 0;

		for (int i = 0; i < inputManager.selectedCharacters.Count; i++) {
			CombatController combat = inputManager.selectedCharacters[i].GetComponent<CombatController>();

			GUIStyle current_style = guardBtn;
			switch(inputManager.selectedCharacters[i].GetComponent<CombatController>().Class){
				case Class.Guard:
					current_style = guardBtn;
					break;
				case Class.Magician:
					current_style = wizardBtn;
					break;
				case Class.Archer:
					current_style = archerBtn;
					break;
				default:
					current_style = guardBtn;
					break;
			}

			if(px * 68 > (panel_w-68)){
				px = 0;
				py += 68;
			}

            //GUI.DrawTexture(new Rect(8 + 68 * i, 30, 64, 64), inputManager.selectedCharacters[i].GetComponent<UICharacter>().Icon);
            if (GUI.Button(new Rect(8 + 68 * px, 30 + py, 64, 64), "", current_style))
            {
                //Move Camera to that character
                Vector3 pos = inputManager.selectedCharacters[i].transform.position;
                //pos.x += 53;
                //pos.z -= 53;
                //pos.y = cameras.transform.position.y;
                cameras.transform.position = pos;
            }
			GUI.DrawTexture(new Rect(8 + 68*px,30+py, 64, 4), wound);
			GUI.DrawTexture(new Rect(8 + 68*px,30+py, (combat.Health.current/combat.Health.max)*64, 4), health);

			px++;
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
		
		//Helpers Labels
		Vector3 mouse = Input.mousePosition;
		mouse.y = Screen.height - Input.mousePosition.y;
		
		Rect aggr = offsetAggr;
		aggr.y += btn_y_offset;
		Rect def = offsetDef;
		def.y += btn_y_offset;
		Rect sg = offsetSG;
		sg.y += btn_y_offset;
		Rect pass = offsetPass;
		pass.y += btn_y_offset;
		
		Rect fullBtns = new Rect(aggr.x, Screen.height-192+30,256,64); 
		Rect specialBtn = new Rect(aggr.x, fullBtns.y + 64, 64, 64);
		if(fullBtns.Contains(mouse)){
			if(aggr.Contains(mouse)){
				DrawHelpMessage("Aggressive Stance");
			}
			else if(def.Contains(mouse)){
				DrawHelpMessage("Defensive Stance");
			}
			else if(sg.Contains(mouse)){
				DrawHelpMessage("Stand Ground");
			}
			else if(pass.Contains(mouse)){
				DrawHelpMessage("Passive Stance");
			}
		}
		else if(uiselection.selectedCharacters.Count == 1 && specialBtn.Contains(mouse)){
			DrawHelpMessage("Special Attack");
		}
	}

	private void DrawHelpMessage(string s){
		DrawText(new Rect(12, Screen.height-220, Screen.width, 30), s, helpText, true, helpText.fontSize, Color.white);
	}
}
