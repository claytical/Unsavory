﻿using UnityEngine;
using System.Collections;

public class SceneSwitchButtonScript : MonoBehaviour {
	public Transform newCameraPosition;
	private Transform startingPosition;
	private float movementTime;
	private float startTime;
	private bool moving = false;
	private float speed = 1.0f;


	void Update() {
		if (moving) {
			float distanceCovered = (Time.realtimeSinceStartup - startTime) * speed;
			float fracJourney = distanceCovered / movementTime;
			Camera.main.transform.position = Vector2.Lerp (startingPosition.position, newCameraPosition.position, fracJourney);
			Debug.Log("COVERED " + distanceCovered + " MOVETIME " + movementTime);
			if (Vector2.Distance (newCameraPosition.transform.position, Camera.main.transform.position) <= .1) {
				moving = false;
				Debug.Log("Stopped Moving");
			}
		}	
	}

	void OnMouseDown() {
		Debug.Log("DETECTED MOUSE!");
		startingPosition =  Camera.main.transform;
		moving = true;
		startTime = Time.realtimeSinceStartup;
		movementTime = Vector2.Distance(startingPosition.position, newCameraPosition.position);

	}

}
