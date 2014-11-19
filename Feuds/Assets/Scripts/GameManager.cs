using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayMode{
	SinglePlayer,
	MultiPlayer
}

public class GameManager : MonoBehaviour {
	public List<GameObject> playerCharacters;
	public List<GameObject> enemyCharacters;
	public PlayMode mode;
	public int round;
	public int maxRound;
	
	public const int AI = 1;
	public const int NETWORK = 2;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Round-End Condition
		checkRoundEnd();
	}
	
	void InitializeGameManager(int maxRound, PlayMode mode){
		this.maxRound = maxRound;
		this.mode = mode;
	}
	
	//Might not needed in the future
	void InitializeRound(List<GameObject> playerCharacters){
		this.playerCharacters = playerCharacters;
		enemyCharacters = null;
	}
	
	void checkRoundEnd(){
		if(playerCharacters.Count == 0){
			//LOSE
		}
		if(enemyCharacters.Count == 0){
			//WIN
		}
		if(round == maxRound){
			//Do something?
		}
	}
}
