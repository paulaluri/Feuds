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
    public bool rendering;
	private bool selected = false;

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
}
