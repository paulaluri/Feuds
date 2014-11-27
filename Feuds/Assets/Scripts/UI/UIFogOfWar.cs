using UnityEngine;
using System.Collections;

public class UIFogOfWar : MonoBehaviour {
	
	public static float visionLength = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject enemy in GameManager.characters[GameManager.other]){
			bool check = false;
			foreach(GameObject player in GameManager.characters[GameManager.player]){
				if((enemy.transform.position - player.transform.position).magnitude <= visionLength){
					EnableRenderer(enemy);
					check = true;
					break;
				}
			}
			if(!check){
				DisableRenderer(enemy);
			}
		}
	}
	
	public static void DisableRenderer(GameObject character){
		Renderer[] renderers = character.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers){
			r.enabled = false;
		}
		if(character.renderer != null){
			character.renderer.enabled = false;
		}
	}
	
	public static void EnableRenderer(GameObject character){
		Renderer[] renderers = character.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers){
			r.enabled = true;
		}
		if(character.renderer != null){
			character.renderer.enabled = true;
		}
	}
}
