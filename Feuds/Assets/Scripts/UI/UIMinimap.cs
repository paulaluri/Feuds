using UnityEngine;
using System.Collections;

public class UIMinimap : MonoBehaviour {
	public Rect minimapRect; //-->>GUI Position
	public GameObject map; //-->terrain
	public GUIStyle style;
	public Diamond positionDiamond;
	
	public int UIHeight = 192;
	
	public Vector2 leftup;
	public Vector2 rightdown;
	public Vector2 leftdown;
	public Vector2 rightup;
	public Vector2 center;
	
	// Use this for initialization
	void Start () {
		minimapRect = new Rect(Screen.width-175,Screen.height-175,172,172);
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
		
		/*
		positionRect.xMin = ( 1f*(leftup.x-CameraMove.leftMost)/(CameraMove.GetWidth()) ) * minimapRect.width + minimapRect.xMin; 
		positionRect.xMax = ( 1f*(rightdown.x-CameraMove.leftMost)/(CameraMove.GetWidth()) ) * minimapRect.width + minimapRect.xMin; 
		positionRect.yMin = ( -1f*(leftup.z-CameraMove.topMost)/(CameraMove.GetHeight()) ) * minimapRect.height + minimapRect.yMax;
		positionRect.yMax = ( -1f*(rightdown.z-CameraMove.topMost)/(CameraMove.GetHeight()) ) * minimapRect.height + minimapRect.yMax;
		*/
		
	}
	
	void SetPositionRect(){
		
		leftup = WorldPointToMinimap(Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)));
		rightdown = WorldPointToMinimap(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height-UIHeight,0)));
		leftdown = WorldPointToMinimap(Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height-UIHeight,0)));
		rightup = WorldPointToMinimap(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0))); 
		
		leftup = offset(leftup);
		rightdown = offset(rightdown);
		leftdown = offset(leftdown);
		rightup = offset(rightup);
			
		
		positionDiamond = new Diamond(leftup,rightdown,leftdown,rightup);
	}
	
	Vector2 getPositionRelatedToScreen(int x,int y){
		Vector2 ret = new Vector2();
		ret.x = (x+100)/200.0f;
		ret.y = (y+30)/100.0f;
		return ret;
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
		GUI.depth = 1;
		
		//GUI.Box(new Rect(leftup.x,leftup.y,2,2),"");
		//GUI.Box(new Rect(leftdown.x,leftdown.y,2,2),"");
		//GUI.Box(new Rect(rightup.x,rightup.y,2,2),"");
		//GUI.Box(new Rect(rightdown.x,rightdown.y,2,2),"");
		
		center = WorldPointToMinimap(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2.0f,(Screen.height-UIHeight)/2.0f,0)));
		print (center);
		GUI.Box(new Rect(center.x,center.y,2,2),"");
	}
	
	Vector2 FromMouseToGUIPosition(Vector2 mP){
		Vector2 ret = mP;
		ret.y = Screen.height - mP.y;
		return ret;
	}
	
	public Vector2 WorldPointToMinimap(Vector3 point){
		Vector2 ans = new Vector2();
		ans.x = ( 1f*(point.x-CameraMove.leftMost)/(CameraMove.GetWidth()) ) * minimapRect.width + minimapRect.xMin; 
		ans.y = ( -1f*(point.z-CameraMove.topMost)/(CameraMove.GetHeight()) ) * minimapRect.height + minimapRect.yMax;
		return ans;
	}
	
	public Vector2 offset(Vector2 point){
		Vector2 ret = new Vector2();
		ret.x = (point.x-minimapRect.x+10)*(minimapRect.width/(minimapRect.width+20f)) + minimapRect.x;
		ret.y = (point.y-minimapRect.y+10)*(minimapRect.width/(minimapRect.height+20f)) + minimapRect.y;
		return ret;
	}
}
