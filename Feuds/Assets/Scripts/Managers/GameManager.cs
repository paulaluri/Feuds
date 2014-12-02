using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayMode{
	SinglePlayer,
	MultiPlayer
}

public class GameManager : MonoBehaviour {
	public static List<GameObject>[] characters;
	public static int player;
	public static int other { get { return player ^ 1; } }
	public static int playerLayer { get { return player + 10; } }
	public static int otherLayer { get { return other + 10; } }
	public static int winner;
	public static int[] wins;
	public static PlayMode mode;
	public static Stat Rounds;
	public static GameMode game;
	public static bool gameStarted;
	public static float timeLeft;

	public float Duration;

	// Use this for initialization
	void Start () {
		StartRound (Duration);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameStarted && winner < 0) {
			timeLeft -= Time.deltaTime;
			if(networkView.isMine) {
				checkRoundEnd();
			}
		}
		else if(winner >= 0) {
			//load stat screen
		}
	}


	public static void StartGame() {
		characters = new List<GameObject>[] {
			new List<GameObject>(),
			new List<GameObject>()
		};
		player = Convert.ToInt32(!(Network.peerType == NetworkPeerType.Disconnected || Network.isServer));
		wins = new int[2] {0,0};
	}

	public static void StartRound(float Duration) {
		timeLeft = Duration;
		winner = -1;
	}
	
	void checkRoundEnd(){
		winner = game.Winner;
		if(winner >= 0) {
			wins[winner]++;
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref timeLeft);
		stream.Serialize (ref winner);
		stream.Serialize (ref wins [0]);
		stream.Serialize (ref wins [1]);
		stream.Serialize (ref Rounds.current);
		stream.Serialize (ref Rounds.max);
	}
}
