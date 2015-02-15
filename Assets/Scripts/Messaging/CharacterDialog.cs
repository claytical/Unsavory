﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using SimpleJSON;


public class CharacterDialog : MonoBehaviour {
	public TextAsset dialog;
	public string key;
	public Text dialogBox;
	private int dialogIndex = 0;
	private int selectedIndex;
	private JSONNode jsonDialog;
	public GameObject advanceButton;
	public GameObject homeButton;
	public GameObject retryButton;
	public GameObject quitButton;
	public GameObject trays;

	// Use this for initialization
	void Start () {
		int currentLevel = PlayerPrefs.GetInt("current level");
		if (currentLevel == 1) {
			//first day of work
			Debug.Log("First Day of Work!");
		}
		else {
			//return to work
			Debug.Log("Returning to work");
		}
		string response_key = key + "_response";
		jsonDialog = JSON.Parse(dialog.ToString());
		selectedIndex = Random.Range(0, jsonDialog[key].Count);
		Debug.Log("There are " + jsonDialog[key].Count + " potential messages.");
		dialogBox.text = jsonDialog[key][selectedIndex][dialogIndex];
		Text responseText = advanceButton.GetComponentInChildren<Text>();
		responseText.text = jsonDialog[response_key][selectedIndex][dialogIndex];
		homeButton.SetActive(false);
		retryButton.SetActive(false);
		quitButton.SetActive(false);
	}
	public void enableTrays() {
		trays.SetActive(true);
	}

	public void reappear() {
		GetComponent<Animator>().SetBool("finishedTalking", false);
		GetComponent<Animator>().SetBool("talkAgain", true);
		
	}

	
	public void advanceScript() {
		if (dialogIndex >= jsonDialog[key][selectedIndex].Count - 1) {
			//finished script
			GetComponentInChildren<Animator>().SetBool("finishedTalking", true);
			advanceButton.SetActive(false);
			Camera.main.GetComponent<CameraSequencing>().enableGamePlay();

		}
		else {
			dialogIndex++;
			string response_key = key + "_response";

			dialogBox.text = jsonDialog[key][selectedIndex][dialogIndex];
				Text responseText = advanceButton.GetComponentInChildren<Text>();
				responseText.text = jsonDialog[response_key][selectedIndex][dialogIndex];
		}
	}

	public void changeSpeechKey(string _key) {
		key = _key;
		dialogIndex = 0;
		selectedIndex = Random.Range(0, jsonDialog[key].Count);
		Debug.Log("There are " + jsonDialog[key].Count + " potential messages.");
		dialogBox.text = jsonDialog[key][selectedIndex][dialogIndex];
		string response_key = _key + "_response";
		Text responseText = homeButton.GetComponentInChildren<Text>();
		responseText.text = jsonDialog[response_key][selectedIndex][dialogIndex];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}