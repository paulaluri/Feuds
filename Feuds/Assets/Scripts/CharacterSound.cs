using UnityEngine;
using System.Collections;

public class CharacterSound : MonoBehaviour {
	public AudioSource clash;

	public void Start(){
		Animator a = new Animator();

	}

	// Use this for initialization
	public void PlaySound(){
		clash.Play ();
	}
}
