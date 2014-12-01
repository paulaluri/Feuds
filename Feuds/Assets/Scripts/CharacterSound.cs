using UnityEngine;
using System.Collections;

public class CharacterSound : MonoBehaviour {
	public AudioSource clash;

	public void Start(){
	}

	// Use this for initialization
	public void PlaySound(){
		clash.Play ();
	}
}
