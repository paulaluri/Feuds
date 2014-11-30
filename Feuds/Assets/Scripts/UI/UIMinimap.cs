using UnityEngine;
using System.Collections;

public class UIMinimap : MonoBehaviour {
	public Diamond minimapDiamond; //-->>GUI Position
	public GameObject map; //-->terrain
	public GUIStyle style;
	public Diamond positionDiamond;
	public GameObject cameras;
	
	public int UIHeight = 192;

	public Vector2 center;
	
	public bool mouseDown = false;
	
	// Use this for initialization
	void Start () {
		Vector2 leftcorner=new Vector2(Screen.width-224,Screen.height-224);
		minimapDiamond = new Diamond(new Vector2(leftcorner.x+108,leftcorner.y),new Vector2(leftcorner.x+108,leftcorner.y+216-50),
		                             new Vector2(leftcorner.x+25,leftcorner.y+108-25),new Vector2(leftcorner.x+216-25,leftcorner.y+108-25));
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && minimapDiamond.Contains(FromMouseToGUIPosition(Input.mousePosition)))
        {
			mouseDown = true;
		}
		if(Input.GetMouseButtonUp(0)){
			mouseDown = false;
		}
		if(mouseDown){
			Vector2 guiPosition = FromMouseToGUIPosition(Input.mousePosition);
			if(minimapDiamond.Contains(guiPosition)){
				//transport camera to that position
				//get ratio first
				Vector2 relativePos = GetRelativePosFromPoint(guiPosition);
				//print (relativePos);
				Vector3 newCameraPos = new Vector3(0,cameras.transform.position.y,0);
				newCameraPos.x = relativePos.x*CameraMove.GetWidth()+CameraMove.leftMost;
				newCameraPos.z = relativePos.y*CameraMove.GetHeight()+CameraMove.topMost;
				cameras.transform.position = newCameraPos;
			}
		}
		
		//print (FromMouseToGUIPosition(Input.mousePosition));
		
		center = GetGUICenterPoint();
		//print (center);
		
		
	}
	
	Vector2 GetRelativePosFromPoint(Vector2 point){
		Vector2 tmp = new Vector2();
		tmp.x = point.x - minimapDiamond.left.x;
		tmp.y = point.y - minimapDiamond.top.y;
		tmp.x *=Mathf.Sqrt(2)/minimapDiamond.GetLength();
		tmp.y *=Mathf.Sqrt(2)/minimapDiamond.GetLength();
		
		Vector2 ans = new Vector2();
		ans.x = (tmp.x+tmp.y-1)/2f;
		ans.y = (tmp.x-tmp.y+1)/2f;
		return ans;
	}
	
	Vector2 GetGUICenterPoint(){
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2.0f,(Screen.height-UIHeight)/2.0f,0));
		Vector2 relativePos = new Vector2();
		relativePos.x = (worldPoint.x-CameraMove.leftMost)/CameraMove.GetWidth();
		relativePos.y = (worldPoint.z-CameraMove.topMost)/CameraMove.GetHeight();
		Vector2 center = new Vector2();
		center.x = minimapDiamond.left.x+(relativePos.x+relativePos.y)*minimapDiamond.GetLength()/Mathf.Sqrt(2);
		center.y = minimapDiamond.top.y+(relativePos.x + 1-relativePos.y)*minimapDiamond.GetLength()/Mathf.Sqrt(2);
		return center;
	}
	
	Vector2 getPositionRelatedToScreen(int x,int y){
		Vector2 ret = new Vector2();
		ret.x = (x+100)/200.0f;
		ret.y = (y+30)/100.0f;
		return ret;
	}
	
	
	void OnGUI(){
		//Debug.Log("I");
		Vector2 guiPosition = FromMouseToGUIPosition(Input.mousePosition);
		if(minimapDiamond.Contains(guiPosition)){
			//GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y,10,10),"");
		}
		
		GUI.Box(new Rect(center.x,center.y,5,5),"");
		
	}
	
	Vector2 FromMouseToGUIPosition(Vector2 mP){
		Vector2 ret = mP;
		ret.y = Screen.height - mP.y;
		return ret;
	}
}
