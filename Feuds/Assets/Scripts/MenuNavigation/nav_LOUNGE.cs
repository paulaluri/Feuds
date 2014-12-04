using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CharacterType{
	Guard,
	Archer,
	Wizard
}

public class LoungeCharacter{
	public CharacterType Type{get; set;}
	public bool BoostAttack{get; set;}
	public bool BoostDefense{get; set;}
	public bool BoostResist{get; set;}
}

public class nav_LOUNGE : MonoBehaviour {
	public string SceneLobby;
	public string SceneGame;
	public GUIStyle menu_btn;
	public GUIStyle menu_text;

	public GUIStyle guard_btn;
	public GUIStyle archer_btn;
	public GUIStyle wizard_btn;
	public GUIStyle std_btn;
	public GUIStyle pushed_btn;

	public Texture boost_attack;
	public Texture boost_defense;
	public Texture boost_resist;
	public Texture delete;

	public CharacterSpawn Spawner;

	private List<LoungeCharacter> chars = new List<LoungeCharacter>();

	public int RESOURCES = 1500;
	public int COST_GUARD = 300;
	public int COST_ARCHER = 400;
	public int COST_WIZARD = 450;
	public int COST_B_ATTACK = 50;
	public int COST_B_DEFENSE = 65;
	public int COST_B_RESIST = 75;

	private int init_resources = 0;

	void Start() {
		GameManager.gameStarted = false;
	}

	void Awake(){
		init_resources = RESOURCES;
	}

	// Update is called once per frame
	void OnGUI(){
		//Back and Confirm
		if(GUI.Button(new Rect(Screen.width-354, Screen.height-58, 172, 48), "Back", menu_btn))
			Application.LoadLevel (SceneLobby);
		if(GUI.Button(new Rect(Screen.width-182, Screen.height-58, 172, 48), "Start Battle", menu_btn)) {
			StartGame();
		}

		//Resources Left
		GUI.color = new Color(1,216f/255f,0);
		GUI.Label(new Rect(40, 60, 200, 20), "Resources left: " + init_resources.ToString(), menu_text);
		GUI.color = new Color(255, 255, 255);

		//Add items
		GUI.BeginGroup(new Rect(40, 100, 264, 192));
		if(GUI.Button(new Rect(0, 0, 64, 64), "", guard_btn) && init_resources >= COST_GUARD){
			LoungeCharacter lc = new LoungeCharacter();
			lc.Type = CharacterType.Guard;
			lc.BoostAttack = lc.BoostDefense = lc.BoostResist = false;
			chars.Add(lc);
			init_resources -= COST_GUARD;
		}
		GUI.Label (new Rect(68,0,200,64), "Guardsmen", menu_text);

		if(GUI.Button(new Rect(0, 64, 64, 64), "", archer_btn) && init_resources >= COST_ARCHER){
			LoungeCharacter lc = new LoungeCharacter();
			lc.Type = CharacterType.Archer;
			lc.BoostAttack = lc.BoostDefense = lc.BoostResist = false;
			chars.Add(lc);
			init_resources -= COST_ARCHER;
		}
		GUI.Label (new Rect(68,64,200,64), "Archer", menu_text);

		if(GUI.Button(new Rect(0, 128, 64, 64), "", wizard_btn) && init_resources >= COST_WIZARD){
			LoungeCharacter lc = new LoungeCharacter();
			lc.Type = CharacterType.Wizard;
			lc.BoostAttack = lc.BoostDefense = lc.BoostResist = false;
			chars.Add(lc);
			init_resources -= COST_WIZARD;
		}
		GUI.Label (new Rect(68,128,200,64), "Wizard", menu_text);

		GUI.color = new Color(1,216f/255f,0);
		GUI.Label (new Rect(68,16,200,64), "[" + COST_GUARD.ToString() + "]", menu_text);
		GUI.Label (new Rect(68,80,200,64), "[" + COST_ARCHER.ToString() + "]", menu_text);
		GUI.Label (new Rect(68,144,200,64), "[" + COST_WIZARD.ToString() + "]", menu_text);
		GUI.color = new Color(255, 255, 255);
		
		GUI.EndGroup();
		
		//Attack icon
		GUI.BeginGroup(new Rect(40, 300, 264, 192));
		GUI.DrawTexture(new Rect(0, 0, 64, 64), boost_attack);
		GUI.Label (new Rect(68,24,200,64), "Attack boost", menu_text);
		GUI.DrawTexture(new Rect(0, 64, 64, 64), boost_defense);
		GUI.Label (new Rect(68,88,200,64), "Armor boost", menu_text);
		GUI.DrawTexture(new Rect(0, 128, 64, 64), boost_resist);
		GUI.Label (new Rect(68,152,200,64), "Magic resistance boost", menu_text);
		GUI.EndGroup();

		//Character List
		GUI.BeginGroup(new Rect(Screen.width/2 - 160, 40, 320, 800));

		for(int i=0; i<chars.Count;i++){
			Texture tex = guard_btn.normal.background;
			if(chars[i].Type == CharacterType.Archer)
				tex = archer_btn.normal.background;
			else if(chars[i].Type == CharacterType.Wizard)
				tex = wizard_btn.normal.background;

			//Main identifier
			GUI.DrawTexture(new Rect(0, 74*i, 64, 64), tex);

			//Attack button
			if(chars[i].BoostAttack && GUI.Button(new Rect(64, 74*i, 64, 64), boost_attack, pushed_btn)){
				chars[i].BoostAttack = false;
				init_resources += COST_B_ATTACK;
			}
			else if(!chars[i].BoostAttack && GUI.Button(new Rect(64, 74*i, 64, 64), boost_attack, std_btn) && init_resources >= COST_B_ATTACK){
				chars[i].BoostAttack = true;
				init_resources -= COST_B_ATTACK;
			}

			//Defense button
			if(chars[i].BoostDefense && GUI.Button(new Rect(128, 74*i, 64, 64), boost_defense, pushed_btn)){
				chars[i].BoostDefense = false;
				init_resources += COST_B_DEFENSE;
			}
			else if(!chars[i].BoostDefense && GUI.Button(new Rect(128, 74*i, 64, 64), boost_defense, std_btn) && init_resources >= COST_B_DEFENSE){
				chars[i].BoostDefense = true;
				init_resources -= COST_B_DEFENSE;
			}

			//Resist button
			if(chars[i].BoostResist && GUI.Button(new Rect(192, 74*i, 64, 64), boost_resist, pushed_btn)){
				chars[i].BoostResist = false;
				init_resources += COST_B_RESIST;
			}
			else if(!chars[i].BoostResist && GUI.Button(new Rect(192, 74*i, 64, 64), boost_resist, std_btn) && init_resources >= COST_B_RESIST){
				chars[i].BoostResist = true;
				init_resources -= COST_B_RESIST;
			}

			GUI.color = new Color(255,216,0);
			GUI.Label (new Rect(64, 74*i+40, 64, 64), "[" + COST_B_ATTACK.ToString() + "]", menu_text);
			GUI.Label (new Rect(128, 74*i+40, 64, 64), "[" + COST_B_DEFENSE.ToString() + "]", menu_text);
			GUI.Label (new Rect(192, 74*i+40, 64, 64), "[" + COST_B_RESIST.ToString() + "]", menu_text);
			GUI.color = new Color(255, 255, 255);

			if(GUI.Button(new Rect(256, 74*i, 64, 64), delete, std_btn)){
				if(chars[i].BoostAttack)
					init_resources += COST_B_ATTACK;
				if(chars[i].BoostDefense)
					init_resources += COST_B_DEFENSE;
				if(chars[i].BoostResist)
					init_resources += COST_B_RESIST;

				switch(chars[i].Type){
					case CharacterType.Guard:
						init_resources += COST_GUARD;
						break;
					case CharacterType.Archer:
						init_resources += COST_ARCHER;
						break;
					case CharacterType.Wizard:
						init_resources += COST_WIZARD;
						break;
					default:
						break;
				}

				chars.RemoveAt(i);
			}
		}

		GUI.EndGroup();
	}

	// network instantiate characters, load scene
	void StartGame() {
		Spawner.units = chars;
		Application.LoadLevel (SceneGame);

	}
}
