using UnityEngine;
using System.Collections;

public class UIMinimap : MonoBehaviour {
	public Rect minimapRect; //-->>GUI Position
	public GameObject map; //-->terrain
	// Use this for initialization
	void Start () {
		minimapRect = new Rect(Screen.width-175,Screen.height-175,172,172);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Input.mousePosition);
		if(minimapRect==null){
			return;
		}
		Vector2 guiPosition = FromMouseToGUIPosition(Input.mousePosition);
		if(minimapRect.Contains(guiPosition)){
			
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
	}
	
	Vector2 FromMouseToGUIPosition(Vector2 mP){
		Vector2 ret = mP;
		ret.y = Screen.height - mP.y;
		return ret;
	}
}
