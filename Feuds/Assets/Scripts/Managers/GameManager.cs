using UnityEngine;
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
		if(gameStarted && winner < 0) {
			if(networkView.isMine) {
				timeLeft -= Time.deltaTime;
				checkRoundEnd();
			}
			if(TimeAlerts.Count > 0 && TimeAlerts[0] > timeLeft) {
				TimeSpan time = TimeSpan.FromSeconds(TimeAlerts[0]);
				msg.Add(string.Format("Only {0:D}:{1:D2} left!",time.Minutes,time.Seconds), 10.0f, true);
				TimeAlerts.RemoveAt(0);
			}
		}
		else if(winner >= 0) {
			//EndRound(winner);
		}
	}


	public static void StartGame() {
		Rounds = new Stat ();
		Rounds.current = 0;
		Rounds.max = 6;

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

		if(Network.isServer) {

			wins [winner]++;
			winners [Mathf.RoundToInt(Rounds.current)] = winner;
			Rounds.current++;

			GameManager.winner = -1;

			networkView.RPC("EndRound",RPCMode.OthersBuffered,winner);
		}

		Application.LoadLevel (SceneGameover);
	}

	void EndGame() {
		Destroy (gameObject);
	}

	void OnLevelWasLoaded(int level) {
		if(Application.loadedLevelName.StartsWith("game")) {
			Ready();
			msg = FindObjectOfType<UIMessage>();
		}
	}

	// This is probably way overboard, but hopefully delta compression is
	// fast and efficient, and this will give the server complete authority
	// over these variables. If too slow, modify EndRound
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		stream.Serialize (ref timeLeft);
		stream.Serialize (ref winner);
		stream.Serialize (ref wins [0]);
		stream.Serialize (ref wins [1]);
		for(int i = 0; i < winners.Length; i++) {
			stream.Serialize(ref winners[i]);
		}
		stream.Serialize (ref Rounds.current);
		stream.Serialize (ref Rounds.max);
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
