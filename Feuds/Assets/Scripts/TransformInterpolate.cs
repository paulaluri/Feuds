using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransformInterpolate : MonoBehaviour {
	public double delay;

	internal struct State {
		public Vector3 pos;
		public Quaternion rot;
		public double timestamp;
	}

	private State lastState;
	private Queue<State> states = new Queue<State>();

	// Use this for initialization
	void Start () {
		lastState.pos = transform.position;
		lastState.rot = transform.rotation;
		lastState.timestamp = Network.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(!networkView.isMine) {
			double currentTime = Network.time;
			double interpolatedTime = currentTime - delay;
			while(states.Count > 1 && states.Peek().timestamp < interpolatedTime) {
				lastState = states.Dequeue();
			}
			if(states.Count > 0) {
				State state = states.Peek();
				float t = (float)((interpolatedTime - lastState.timestamp)/(state.timestamp - lastState.timestamp));
				transform.position = Vector3.Lerp(lastState.pos,state.pos,t);
				transform.rotation = Quaternion.Lerp(lastState.rot,state.rot,t);
			}
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if(stream.isWriting) {
			Vector3 pos = transform.position;
			Quaternion rot = transform.rotation;
			stream.Serialize(ref pos);
			stream.Serialize(ref rot);
		}
		else {
			Vector3 pos = Vector3.zero;
			Quaternion rot = Quaternion.identity;
			stream.Serialize(ref pos);
			stream.Serialize(ref rot);

			State state;
			state.pos = pos;
			state.rot = rot;
			state.timestamp = info.timestamp;
			states.Enqueue(state);
		}
	}
}
