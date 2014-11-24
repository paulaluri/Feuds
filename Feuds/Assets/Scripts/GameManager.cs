﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayMode{
	SinglePlayer,
	MultiPlayer
}

public class GameManager : MonoBehaviour {
	public static List<GameObject> playerCharacters;
	public static List<GameObject> enemyCharacters;
	public static PlayMode mode;
	public static int round;
	public static int maxRound;
	public static GameMode game;

	// Use this for initialization
	void Start () {
		InitializeRound();
	}
	
	// Update is called once per frame
	void Update () {
		//Round-End Condition
		checkRoundEnd();
	}
	
	public static void InitializeGameManager(int maxRound, PlayMode mode){
		GameManager.maxRound = maxRound;
		GameManager.mode = mode;
	}
	
	//Might not needed in the future
	public static void InitializeRound(){
		GameManager.playerCharacters = new List<GameObject>();
		GameManager.playerCharacters.AddRange(GameObject.FindGameObjectsWithTag("Character"));
		GameManager.enemyCharacters = new List<GameObject>();
		GameManager.enemyCharacters.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
	}
	
	void checkRoundEnd(){
		int winner = game.Winner;
		if(winner == 0){
			//no one yet
		}
		if(winner == 1){
			//player 1
		}
		if(winner == 2){
			//player 2
		}
	}
}
