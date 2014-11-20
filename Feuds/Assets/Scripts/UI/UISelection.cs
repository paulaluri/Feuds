using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UISelection : MonoBehaviour {
	public List<GameObject> characters;
	public List<GameObject> selectedCharacters;
	public Vector3 mouseStartPosition;
	public bool mouseDown;
	public Rect selectionRect;
	public GUIStyle style;
	
	public int bottom_threshold;
	
	public InputManager inputManager;
	
	public const int LEFT_CLICK = 0;
	public const int RIGHT_CLICK = 1;
	// Use this for initialization
	void Start () {
		characters = new List<GameObject>();
		Initialize(GameManager.playerCharacters);
		bottom_threshold = 192;
	}
	
	// Update is called once per frame
	void Update () {
		if(characters == null || characters.Count==0){
			//Not Initialize yet
			return;
		}
		if (Input.mousePosition.y<bottom_threshold){
			return;
		}
		if(Input.GetMouseButtonDown(LEFT_CLICK)){
			mouseStartPosition = Input.mousePosition;
			mouseDown = true;
			
			//To do
			//ClearSelect()
			selectedCharacters.Clear();
			//Send it somewhere?
			inputManager.SelectCharacters(selectedCharacters);
		}
		if(Input.GetMouseButtonUp(LEFT_CLICK)){
			mouseDown = false;
			
		}
		
		if(Input.GetMouseButtonUp(RIGHT_CLICK)){
			//right click
			if(selectedCharacters.Count==0){
				return ;
			}
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {

				if(GameManager.enemyCharacters.Contains(hit.collider.gameObject)){
					//Enemy!!!
					//Attack
					print ("Attack : "+hit.collider.gameObject.name + " at "+hit.point);
					inputManager.Attack(hit.collider.gameObject);
				}
				else {
					//Move to this position
					print ("Move To : "+hit.point);
					inputManager.MoveTo(hit.point);
				}
			}    
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
					//To do
					//Select()
					print (c.name);
					if(!selectedCharacters.Contains(c)){
						selectedCharacters.Add(c);
						//Send it somewhere?
						inputManager.SelectCharacters(selectedCharacters);
					}
				}
				else {
					if(selectedCharacters.Contains(c)){
						selectedCharacters.Remove(c);
						//Send it somewhere?
						inputManager.SelectCharacters(selectedCharacters);
					}
				}
			}
		}
	}
	
	void OnGUI () {
		// Make a background box
		if(mouseDown)GUI.Box(selectionRect, "");
	
	}
	
	public void Initialize(List<GameObject> c){
		characters = c;
		selectedCharacters = new List<GameObject>();
	}
}
