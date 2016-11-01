using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextAdventureClass : MonoBehaviour {
	public string currentRoom;
	public string myText;

	// variables to store possible room connections
	private string room_up, room_down, room_left, room_right;

	// Use this for initialization
	void Start () {
		// change text to read 'we ran our scene, nice'
		myText = "You are in the";
		currentRoom = "entry";
	}
	
	// Update is called once per frame
	void Update () {
		room_up = room_right = room_left = room_down = "nil";
		if (currentRoom == "entry") {
			room_up = "hallway";
			myText = "You are in the " + currentRoom;
		} else if (currentRoom == "hallway") {
			room_right = "kitchen";
			room_down = "entry";
			myText = "You are in the " + currentRoom;
		} else if (currentRoom == "kitchen") {
			room_left = "hallway";
			myText = "You are in the " + currentRoom;
		} else {
			myText = "you fell into a void cause why not.";
		}

		if (room_up != "nil") {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				currentRoom = room_up;
			}
		}
		if (room_down != "nil") {
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				currentRoom = room_down;
			}
		}
		if (room_left != "nil") {
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				currentRoom = room_left;
			}
		}
		if (room_right != "nil") {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				currentRoom = room_right;
			}
		}

		GetComponent<Text>().text = myText;
	}
}
