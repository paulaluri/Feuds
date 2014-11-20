using UnityEngine;
using System.Collections;

public class UIMinimap : MonoBehaviour {
	public Rect minimapRect; //-->>GUI Position
	public GameObject map; //-->terrain
	public GUIStyle style;
	public Rect positionRect;
	public int positionRectWidth;
	public int positionRectHeight;
	// Use this for initialization
	void Start () {
		minimapRect = new Rect(Screen.width-175,Screen.height-175,172,172);
		positionRect = new Rect(10,10,positionRectWidth,positionRectHeight);
	}
	
	// Update is called once per frame
	void Update () {
		if(minimapRect==null){
			return;
		}
		SetPositionRect();
		if(Input.GetMouseButtonDown(0)){
			Vector2 guiPosition = FromMouseToGUIPosition(Input.mousePosition);
			if(minimapRect.Contains(guiPosition)){
				
			}
		}
	}
	
	void SetPositionRect(){
		Ray ray = Camera.main.ScreenPointToRay (new Vector3(Screen.width/2.0,Screen.height/2.0);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			
		}    
	}
	
	void Initialize(Rect r){
		minimapRect = r;
	}
	
	void OnGUI(){
		if(minimapRect==null){
			return;
		}
		//Debug.Log("I");
		Vector2 guiPosition = FromMouseToGUIPosition(Input.mousePosition);
		if(minimapRect.Contains(guiPosition)){
			GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y,10,10),"");
		}
		GUI.Box(positionRect,"",style);
	}
	
	Vector2 FromMouseToGUIPosition(Vector2 mP){
		Vector2 ret = mP;
		ret.y = Screen.height - mP.y;
		return ret;
	}
}
