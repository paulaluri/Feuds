﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	
	public static Stat Rounds;
	public static bool gameStarted;
	public static float timeLeft;

	public static bool meReady = false;
	public static bool otherReady = false;
	public static bool ready { get { return meReady && otherReady; } }

	public static int winner;
	public static int[] wins;
	public static int[] winners;

	public static GameMode game;

	public static int player;
	public static int other { get { return player ^ 1; } }
	public static int playerLayer { get { return player + 10; } }
	public static int otherLayer { get { return other + 10; } }

	public static List<GameObject>[] characters;

	public float Duration;
	public GameObject gameModePrefab;
	public List<float> TimeAlerts;
	public UIMessage msg;
	public String SceneGameover;

	private float syncTimeLeft;

	// Use this for initialization
	void Awake () {
		StartGame ();
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(ready && !gameStarted) {
			StartRound();
		}
		if(gameStarted) {
			if(winner < 0) {
				timeLeft -= Time.deltaTime;
				if(networkView.isMine) {
					checkRoundEnd();
				}
				if(TimeAlerts.Count > 0 && TimeAlerts[0] > timeLeft) {
					TimeSpan time = TimeSpan.FromSeconds(TimeAlerts[0]);
					syncTimeLeft = timeLeft;
					if(!msg) {
						msg = FindObjectOfType<UIMessage>();
					}
					else {
						msg.Add(string.Format("Only {0:D}:{1:D2} left!",time.Minutes,time.Seconds), 10.0f, true);
						TimeAlerts.RemoveAt(0);
					}
				}
			}
			else {
				EndRound(winner);
			}
		}
	}


	public static void StartGame() {
		Rounds = new Stat (0,6);

		player = Convert.ToInt32(!Network.isServer);
		wins = new int[2] {0,0};
		winners = Enumerable.Repeat (-1, Mathf.RoundToInt(Rounds.max)).ToArray ();

		characters = new List<GameObject>[] {
			new List<GameObject>(),
			new List<GameObject>()
		};

	}

	void StartRound( ) {
		gameStarted = true;
		timeLeft = Duration;
		syncTimeLeft = timeLeft;
		winner = -1;
		if(Network.isServer) {
			Network.Instantiate(gameModePrefab,gameModePrefab.transform.position,Quaternion.identity,0);
		}
	}

	void checkRoundEnd(){
		if(game) {
			winner = game.Winner;
		}
	}

	[RPC]
	void EndRound(int winner) {

		characters [0].Clear ();
		characters [1].Clear ();

		gameStarted = false;
		game = null;

		meReady = false;
		otherReady = false;

		GameManager.winner = winner;
		wins [winner]++;
		winners [Mathf.RoundToInt(Rounds.current)] = winner;
		Rounds.current++;

		if(Network.isServer) {
			networkView.RPC("EndRound",RPCMode.OthersBuffered,winner);
		}

		Application.LoadLevel (SceneGameover);
	}

	public void EndGame() {
		Network.Disconnect ();
		Destroy (gameObject);
	}

	void OnLevelWasLoaded(int level) {
		if(Application.loadedLevelName.StartsWith("game")) {
			Ready();
		}
		if (Application.loadedLevelName.EndsWith("LOBBY")) {
			EndGame();
		}
	}

	// This is probably way overboard, but hopefully delta compression is
	// fast and efficient, and this will give the server complete authority
	// over these variables. If too slow, modify EndRound
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref syncTimeLeft);
		if(stream.isReading) {
			timeLeft = syncTimeLeft;
		}
	}

	void Ready() {
		meReady = true;
		networkView.RPC ("OtherReady", RPCMode.OthersBuffered);
	}

	[RPC]
	void OtherReady() {
		otherReady = true;
	}
}
