using UnityEngine;
using System.Collections;

public class UICharacter : MonoBehaviour {
	public Texture Health;
	public Texture Wound;
    public Texture Icon;
	public Texture attackBoost;
	public Texture defenseBoost;
	public Texture resistBoost;
	public MeshRenderer selection;
	public InputManager inputManager;
	public GUIStyle text_style;
    public bool rendering;
	private bool selected = false;
	public int currentControlGroup = -1;

	private CombatController combat;

	// Use this for initialization
	void Start () {
		combat = gameObject.GetComponent<CombatController> ();
        selection.enabled = false;
		inputManager = FindObjectOfType<InputManager> ();
        rendering = true;
	}
	
	// Update is called once per frame
	void Update () {
			if (inputManager.selectedCharacters.Contains (gameObject)) {
			//Draw selection circle
			selection.enabled = true;
			selected = true;
            //print(gameObject);
		}
		else {
			selection.enabled = false;
			selected = false;
            //print("?" + gameObject);
		}
	}

	void OnGUI(){
		if (!combat.isDead) {
            if (rendering)
            {
				if(Camera.main){
                	Vector3 screen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2.2f, 0));

					float x = screen.x - 32;
					float y = Screen.height - screen.y;

                	GUI.DrawTexture(new Rect(x, y, 64, 4), Wound);
                	GUI.DrawTexture(new Rect(x, y, Mathf.Max(1, (combat.Health.current / combat.Health.max) * 64), 4), Health);

					//Display control group
					if(currentControlGroup >= 0){
						DrawText(new Rect(x, y - 12, 64, 12), currentControlGroup.ToString(), text_style, true, 12, Color.white);
					}

					//Display boost icons
					if(selected){
						float x_pos = x;
						if(combat.hasAttackBoost){
							GUI.DrawTexture(new Rect(x_pos, y-12, 10, 10), attackBoost);
							x_pos += 12;
						}
						if(combat.hasDefenseBoost){
							GUI.DrawTexture(new Rect(x_pos, y-12, 10, 10), defenseBoost);
							x_pos += 12;
						}
						if(combat.hasResistBoost){
							GUI.DrawTexture(new Rect(x_pos, y-12, 10, 10), resistBoost);
						}
					}
				}
            }
        }
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
}
