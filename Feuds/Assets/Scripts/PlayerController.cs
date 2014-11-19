using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public static List<GameObject> characters;
	public Vector3 mouseStartPosition;
	public bool mouseDown;
	public Rect selectionRect;
	public GUIStyle style;
	// Use this for initialization
	void Start () {
		characters = new List<GameObject>();
		characters.AddRange(GameObject.FindGameObjectsWithTag("Character"));
		Debug.Log(characters.Count);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			mouseStartPosition = Input.mousePosition;
			mouseDown = true;
			
			//To do
			//ClearSelect()
		}
		if(Input.GetMouseButtonUp(0)){
			mouseDown = false;
			
		}
		
		if(mouseDown){
			selectionRect.xMin = mouseStartPosition.x < Input.mousePosition.x? mouseStartPosition.x: Input.mousePosition.x;
			selectionRect.xMax = mouseStartPosition.x > Input.mousePosition.x? mouseStartPosition.x: Input.mousePosition.x;
			selectionRect.yMin = Camera.main.pixelHeight - (mouseStartPosition.y > Input.mousePosition.y? mouseStartPosition.y: Input.mousePosition.y);
			selectionRect.yMax = Camera.main.pixelHeight - (mouseStartPosition.y < Input.mousePosition.y? mouseStartPosition.y: Input.mousePosition.y);
			
			characters.RemoveAll(item => item == null);
			foreach(GameObject c in characters){
				Vector3 screenPos = Camera.main.WorldToScreenPoint(c.transform.position);
				screenPos.y = Camera.main.pixelHeight -screenPos.y;
				//print (""+screenPos+" - "+Input.mousePosition);
				if(selectionRect.Contains(screenPos)){
					Debug.Log("!");
					c.animation.CrossFade("Guard_Dying");
					//To do
					//Select()
				}
			}
		}
	}
	
	void OnGUI () {
		// Make a background box
		if(mouseDown)GUI.Box(selectionRect, "");
		foreach(GameObject c in characters){
			Vector3 screenPos = Camera.main.WorldToScreenPoint(c.transform.position);
			GUI.Box(new Rect(screenPos.x,Camera.main.pixelHeight -screenPos.y,1,1),"");
		}
	}
}
