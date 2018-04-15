﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneSwitch : MonoBehaviour {
	[SerializeField]
	private bool isWaiting;
	[SerializeField]
	private bool isLight;
	[SerializeField]
	private float translate_x = 76.5f;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private Slider slider;
	[SerializeField]
	private float timer = 0.0f;
	[SerializeField]
	private AudioManager audioManager;
	// Use this for initialization
	void Start () {
		isWaiting = true;
		isLight = true;
		player = this.gameObject;
		slider = GameObject.Find("Slider").GetComponent<Slider>();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWaiting) {
			StartCoroutine ("WaitTenSeconds");
		} else 
		{
			if(Input.GetButtonDown("Jump"))
			{
				isWaiting = true;
				float prevposx = player.transform.position.x;
				if (isLight)
					player.transform.position = new Vector3 (translate_x + prevposx, transform.position.y, 0.0f);
				else if (!isLight)
					player.transform.position = new Vector3(-translate_x + prevposx,transform.position.y,0.0f);
				isLight = !isLight;

				//Switch songs
				audioManager.Play("SceneSwitchSound");
				audioManager.ToggleMuteSong("DarkworldTheme", isLight);
				audioManager.ToggleMuteSong ("OverworldTheme", !isLight);
			}
		}
	}

	IEnumerator WaitTenSeconds()
	{
		for (int i = 0; i < 1000; i++) {
			yield return new WaitForSeconds (0.01f);
			slider.value = (i+1)/100;
		}
		isWaiting = false;
		StopCoroutine ("WaitTenSeconds");
	}
}