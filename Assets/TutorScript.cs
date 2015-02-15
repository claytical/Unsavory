﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorScript : MonoBehaviour {
	public List<GameObject> trays = new List<GameObject>();
	public GameObject recipe;
	public GameObject spawningArea;
	public GameObject orderHopper;
	public GameObject plate;
	private int trayIndex = 0;
	private RecipeScript currentRecipe;
	public string tutorialEnd;
	bool tutorialFinished = false;

	// Use this for initialization
	void Start () {
		Vector3 recipePosition = orderHopper.transform.position;
		recipePosition.x = 0;
		GameObject newRecipe =(GameObject) Instantiate(recipe, recipePosition, Quaternion.identity);
		newRecipe.transform.parent = spawningArea.transform;
		currentRecipe = recipe.GetComponent<RecipeScript>();
		GetComponentInChildren<Text>().text = "Let's make a " + currentRecipe.name + "!\n" + trays[trayIndex].GetComponent<AddIngredientScript>().tutorialLesson;
		trays[trayIndex].GetComponent<Animator>().SetBool("jiggling", true);
		for (int i = 0; i < trays.Count; i++) {
			trays[i].GetComponent<BoxCollider2D>().enabled = false;
		}
		trays[trayIndex].GetComponent<BoxCollider2D>().enabled = true;
		plate.GetComponent<BoxCollider2D>().enabled = false;
		plate.GetComponent<PlateScript>().tutorialMode = true;
		Time.timeScale = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void finishTutorial() {
		for (int i = 0; i < trays.Count; i++) {
			trays[i].GetComponent<BoxCollider2D>().enabled = true;
		}
		//TODO: Start Current Order Script Up
		orderHopper.GetComponent<CurrentOrderScript>().spawnMoreOrders = true;
		Time.timeScale = 1.0f;
		Destroy (gameObject);

	}

	public void advanceTutorial() {

		trays[trayIndex].GetComponent<Animator>().SetBool("jiggling", false);
		trayIndex++;
		if (trayIndex < trays.Count) {
			GetComponentInChildren<Text>().text = trays[trayIndex].GetComponent<AddIngredientScript>().tutorialLesson;
			trays[trayIndex].GetComponent<Animator>().SetBool("jiggling", true);
			trays[trayIndex].GetComponent<BoxCollider2D>().enabled = true;
		}
		else {
			Debug.Log("Done Advancing");
			GetComponentInChildren<Text>().text = tutorialEnd;
			plate.GetComponent<BoxCollider2D>().enabled = true;

		}


	}
}