using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayMode{
	SinglePlayer,
	MultiPlayer
}

public class GameManager : MonoBehaviour {
	public static List<GameObject>[] characters = new List<GameObject>[] {
		new List<GameObject>(),
		new List<GameObject>()
	};
	public static int player;
	public static int other { get { return player ^ 1; } }
	public static int playerLayer { get { return player + 10; } }
	public static int otherLayer { get { return other + 10; } }
	public static PlayMode mode;
	public static int round;
	public static int maxRound;
	public static GameMode game;
	
	// Use this for initialization
	void Start () {
		InitializeRound();
		player = Convert.ToInt32(!(Network.peerType == NetworkPeerType.Disconnected || Network.isServer));
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
		GameManager.characters[0].AddRange(GameObject.FindGameObjectsWithTag("Player1"));
		GameManager.characters[1].AddRange(GameObject.FindGameObjectsWithTag("Player2"));
	}
	
	void checkRoundEnd(){
		//int winner = game.winner;
		int winner = -1;
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
