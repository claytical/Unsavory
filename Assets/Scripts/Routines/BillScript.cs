﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BillScript : MonoBehaviour {
	public int id;
	public int amount;
	public string title;
	public string effect;
	public bool overdue = false;
	public Text titleUI;
	public Text amountUI;
	public Text dueUI;
	public Button payButton;
	private int money;
	// Use this for initialization
	void Start () {
		titleUI.text = title;
		amountUI.text = "$" + amount.ToString();
		int[] dueDates = PlayerPrefsX.GetIntArray("due");
		int currentDay = PlayerPrefs.GetInt("current level");
		money = PlayerPrefs.GetInt ("money");
		if (money > amount) {
//			payButton.enabled = true;
		}
		else {
			payButton.enabled = false;
		}
		if (dueDates[id] > currentDay) {
			//due in a bit
			dueUI.text = "Due in " + ( dueDates[id] - currentDay).ToString() + " days";
		}
		else if (dueDates[id] == currentDay) {
			//due today
			dueUI.text = "Due Today";
		}
		else {
			//overdue
			dueUI.text = "OVERDUE!";
		}

//		GetComponentInChildren<Text>().text = amount.ToString();
//		GetComponentInChildren<Text>().text = amount.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		if (money > amount) {
			payButton.enabled = true;
		}
		else {
		//	payButton.enabled = false;
		}

	}

	public void payBill() {
		money = PlayerPrefs.GetInt ("money");

		if (money - amount < 0) {
		//TODO: Visual update for not being able to pay the bill
			Debug.Log("Have : " + money + " need " + amount);
		}
		else {
			//Pay Amount Required
			Debug.Log ("Paying bill");
			money = money - amount;
			PlayerPrefs.SetInt("money", money);
//			moneyUI.text = money.ToString();
			GameObject player = GameObject.Find("/Player");
			player.GetComponent<PlayerScript>().moneyUI.text = money.ToString();
			//Set Due Date 30 Days Out
			int[] dueDates = PlayerPrefsX.GetIntArray("due");
			dueDates[id] = dueDates[id] + 30;
			Debug.Log("Setting due date to " + dueDates[id]);
			PlayerPrefsX.SetIntArray("due", dueDates);
			

			//Remove Effect From Preferences
			List<string> effects = new List<string>();
			effects.AddRange(PlayerPrefsX.GetStringArray("effects"));
			effects.RemoveAll(s => s == effect);
			PlayerPrefsX.SetStringArray("effects", effects.ToArray());
			Destroy (this.gameObject);
		}
	}

}