using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class GameItself : MonoBehaviour {
	// Current rooms will be determined by enum?
	enum rooms {entry, end, riddle, key, hallStep1, hallStep2, hallStep3, hallStep4, hallStep5, hallStep6, hallStep7, hallStep8,
	hallStep9, hallStep10, hallStep11, cave1, cave2, cave3, cave4, cave5, cave6, cave7, title};
	string roomText;
	string incorrectText = "";
	int riddleQ = 0;
	bool startKeyPressed = false;
	bool riddleSolved = false;
	bool keyAcquired = false;

	bool grunt1Defeated = false;
	bool grunt2Defeated = false;

	string newLines = "\n\n";
	rooms currentRoom;
	// Goal is to reach end room

	// Unlock a door with key, input pattern for another door

	// Use this for initialization
	public AudioClip zubat_cry;
	public AudioClip paris_cry;
	public AudioClip cave;
	public AudioSource pokemonCries;
	public AudioSource bgMusic;
	void Start () {
		currentRoom = rooms.title;
		bgMusic.clip = cave;
		if (!bgMusic.isPlaying) {
			bgMusic.volume = 0.3f;
			bgMusic.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currentRoom == rooms.riddle) {
			if (!riddleSolved) {
				roomText = "You see a passageway with unknown's forming text in front of it, blocking it. \n\n approach it? (Key N)";
				if (Input.GetKeyDown (KeyCode.N)) {
					startKeyPressed = true;
				}
				if (startKeyPressed) {
					// solve 2 riddles
					if (riddleQ == 0) {
						roomText = incorrectText + "They appear to be forming phrases..." + newLines;
						roomText += "Whose Ash's first pokemon?";
						roomText += newLines + "Pikachu (Key A) \nSquirtle (Key B) \nTodadile (Key C)";

						if (Input.GetKeyDown (KeyCode.B) || Input.GetKeyDown (KeyCode.C)) {
							incorrectText = "The unknown shimmer, then form the phrase 'wrong, try again..." + newLines;
						}
						if (Input.GetKeyDown (KeyCode.A)) {
							incorrectText = "";
							riddleQ = 1;
						}
					} else if (riddleQ == 1) {
						roomText = incorrectText + "They appear to be forming phrases..." + newLines;
						roomText += "Which pokemon was considered for the spot of mascot pokemon before Pikachu?";
						roomText += newLines + "Clefairy (Key A) \nCubone (Key B) \nMeowth (Key C)";

						if (Input.GetKeyDown (KeyCode.B) || Input.GetKeyDown (KeyCode.C)) {
							incorrectText = "The unknown shimmer, then form the phrase 'wrong, try again..." + newLines;
						}
						if (Input.GetKeyDown (KeyCode.A)) {
							incorrectText = "";
							riddleQ = 0;
							startKeyPressed = false;
							riddleSolved = true;
						}
					}
				}
			} else {
				roomText = "You've solved the unknown's riddle in this room, you may now advance";
			}
			// give option to go back into previous room
			roomText += newLines + newLines + "Go back into Unknown Cave (Key S)";
			if (riddleSolved) {
				roomText += "\nGo through cave passageway that was blocked by unknown (Key Q)";
				if (Input.GetKeyDown (KeyCode.Q)) {
					currentRoom = rooms.cave7;
				}
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				currentRoom = rooms.hallStep11;
			}
		} else { // reset all puzzles upon leaving them
			if (currentRoom == rooms.entry) {
				roomText = "As an aspiring pokemon trainer, your burning curiosity and " +
				"desire to discover all 600+ types brings you to the infamous cave of the unknown. Your goal is to make it through" +
				" and discover what secrets this cave is hiding from the world." +
				" \n \nYou take your first step inside and find yourself in a dark passageway";

				roomText += newLines + newLines + "Take a step forward (Key W)";
				if (Input.GetKeyDown (KeyCode.W)) {
					currentRoom = rooms.hallStep1;
					pokemonCries.clip = zubat_cry;
					if (!pokemonCries.isPlaying) {
						pokemonCries.Play();
					}
				}
			} else {
				if (currentRoom == rooms.hallStep1) {
					roomText = "Things seem normal, a few zubat cries can be heard deeper into the cave";
					roomText += newLines + newLines + "Turn right and take a step forward (Key D)";
					roomText += "\nTake a step backward (Key S)";
					if (Input.GetKeyDown (KeyCode.D)) {
						currentRoom = rooms.hallStep2;
						pokemonCries.clip = paris_cry;
						if (!pokemonCries.isPlaying) {
							pokemonCries.Play();
						}
					}
					if (Input.GetKeyDown (KeyCode.S)) {
						currentRoom = rooms.entry;
					}
				} else {
					if (currentRoom == rooms.hallStep2) {
						roomText = "You look to the right and spot a paris. it cries toward you and scuttles away";
						roomText += newLines + newLines + "Take a step forward (Key W)";
						roomText += "\nTake a step backward (Key S)";
						if (Input.GetKeyDown (KeyCode.W)) {
							currentRoom = rooms.hallStep3;
						}
						if (Input.GetKeyDown (KeyCode.S)) {
							currentRoom = rooms.hallStep1;
							pokemonCries.clip = zubat_cry;
							if (!pokemonCries.isPlaying) {
								pokemonCries.Play();
							}
						}
					} else {
						if (currentRoom == rooms.hallStep3) {
							roomText = "This cave looks extensive. You look on your right and spot a passageway opening";
							roomText += newLines + newLines + "Keep moving forward (Key W)";
							roomText += "\nTurn right and walk into the open passageway (Key D)";
							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep4;
							}
							if (Input.GetKeyDown (KeyCode.D)) {
								currentRoom = rooms.cave1;
							}
						} else if (currentRoom == rooms.cave1) {
							roomText = "The cave room is empty, no signs of pokemon nor anything of value in here.";
							roomText += newLines + newLines + "Step backward back into the cave (Key S)";
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep3;
							}
						} else if (currentRoom == rooms.hallStep4) {
							roomText = "On your left appears to be another cave room opening, a couple zubat and geodude cries can be" +
							" heard from inside";
							roomText += newLines + newLines + "Turn left and take a step inside the cave room (Key A)";
							roomText += "\nTake another step foward (Key W)";
							roomText += "\nTake a step backward (Key S)";
							if (Input.GetKeyDown (KeyCode.A)) {
								currentRoom = rooms.cave2;
							}
							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep5;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep3;
							}
						} else if (currentRoom == rooms.cave2) {
							if (!grunt1Defeated) {
								roomText = "Inside you come across a team rocket grunt that appears to be blocking a pass" +
								"ageway to outside the cave. He stares at you menacingly, with a pokeball in hand";
								roomText += newLines + newLines + "Battle the grunt (Key W)";
								roomText += "\nStep back into Unknown Cave (Key S)";
								if (Input.GetKeyDown (KeyCode.W)) {
									grunt1Defeated = true;
								}
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.hallStep4;
								}
							} else {
								roomText = "The defeated rocket grunt has scattered, leaving the passageway to outside open";
								roomText += newLines + newLines + "Walk through the now open passageway(Key W)";
								roomText += "\nStep back into Unknown Cave (Key S)";

								if (Input.GetKeyDown (KeyCode.W)) {
									currentRoom = rooms.key;
								}
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.hallStep4;
								}
							}
						} else if (currentRoom == rooms.key) {
							if (!keyAcquired) {
								roomText = "Outside you notice a couple patches of grass. One patch seems to be shivering violently" +
								", occassionally letting off the cries of a pidgey.";
								roomText += newLines + newLines + "Enter the grass and battle the pigey (Key W)";
								roomText += "\nStep back into the cave room (Key S)";

								if (Input.GetKeyDown (KeyCode.W)) {
									keyAcquired = true;
								}
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.cave2;
								}
							} else {
								roomText = "You entered the grass and successfully beat the pidgey. After it's defeat it dropped a" +
								" key, which you decided to pick up and place in your bag.";
								roomText += newLines + newLines + "Step back into the cave room (Key S)";
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.cave2;
								}
							}
						} else if (currentRoom == rooms.hallStep5) {
							roomText = "You are approached by a wild zubat, which you promptly defeat. Blasted bats.";
							roomText += newLines + newLines + "Take another step forward (Key W)";
							roomText += "\nStep a step backward (Key S)";

							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep6;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep4;
							}
						} else if (currentRoom == rooms.hallStep6) {
							roomText = "There appears to be another cave room entrance here on your right";
							roomText += newLines + newLines + "Take another step forward (Key W)";
							roomText += "\nStep a step backward (Key S)";
							roomText += "\nTurn right and step into the cave room (Key D)";

							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep7;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep5;
							}
							if (Input.GetKeyDown (KeyCode.D)) {
								currentRoom = rooms.cave3;
							}
						} else if (currentRoom == rooms.cave3) {
							roomText = "Nothing but rocks here";
							roomText += newLines + newLines + "Step back into Uknown Cave (Key S)";
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep6;
							}
						} else if (currentRoom == rooms.hallStep7) {
							roomText = "A Machop appears, looking for battle. You defeat it, and notice a cave room on your left." +
							" Uknown Cave" +
							" continues to your right. ";
							roomText += newLines + newLines + "Turn right and take another step forward (Key D)";
							roomText += "\nTurn left and step into the cave room (Key A)";
							roomText += "\nTake a step backward (Key S)";
							if (Input.GetKeyDown (KeyCode.D)) {
								currentRoom = rooms.hallStep8;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep6;
							}
							if (Input.GetKeyDown (KeyCode.A)) {
								currentRoom = rooms.cave4;
							}
						} else if (currentRoom == rooms.cave4) {
							roomText = "Nothing but rocks here again! You catch a meowth snicking towards you in the back, then" +
							" retreating";
							roomText += newLines + newLines + "Step back into Uknown Cave (Key S)";
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep7;
							}
						} else if (currentRoom == rooms.hallStep8) {
							roomText = "Starting to get a little tired, man this cave is long";
							roomText += newLines + newLines + "Take another step forward (Key W)";
							roomText += "\nTake a step backward (Key S)";
							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep9;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep7;
							}
						} else if (currentRoom == rooms.hallStep9) {
							roomText = "A Golbat appears, giving you a small amount of trouble but eventually being defeated." +
							" There appear to be a couple cave rooms around you";
							roomText += newLines + newLines + "Continue deeper into Unkown Cave (Key W)";
							roomText += "\nTurn left and step into the cave room (Key A)";
							roomText += "\nTurn right and step into the other cave room (Key D)";
							roomText += "\nTake a step backward (Key S)";
							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep10;
							}
							if (Input.GetKeyDown (KeyCode.A)) {
								currentRoom = rooms.cave5;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep8;
							}
							if (Input.GetKeyDown (KeyCode.D)) {
								currentRoom = rooms.cave6;
							}
						} else if (currentRoom == rooms.cave5) {
							roomText = "You spot a dunsparce escaping qucikly into the ground using its pudgy tail";
							roomText += newLines + newLines + "Step back into Unknown Cave (Key S)";
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep9;
							}
						} else if (currentRoom == rooms.cave6) {
							roomText = "Nothing of interest in here either. You decide to take a minor break and heal your pokemon";
							roomText += newLines + newLines + "Step back into Unknown Cave (Key S)";
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep9;
							}
						} else if (currentRoom == rooms.hallStep10) {
							roomText = "A couple zubat fly overhead";
							roomText += newLines + newLines + "Take another step forward (Key W)";
							roomText += "\nTake a step backward (Key S)";
							if (Input.GetKeyDown (KeyCode.W)) {
								currentRoom = rooms.hallStep11;
							}
							if (Input.GetKeyDown (KeyCode.S)) {
								currentRoom = rooms.hallStep9;
							}
						} else if (currentRoom == rooms.hallStep11) {
							riddleQ = 0;
							startKeyPressed = false;
							if (!grunt2Defeated) {
								roomText = "To your right a team rocket grunt jumps you and engages you in battle";
								roomText += newLines + newLines + "Defeat the grunt (Key A)";
								if (Input.GetKeyDown (KeyCode.A)) {
									grunt2Defeated = true;
								}
							} else {
								roomText = "The team rocket grunt that jumped you fled, leaving the opening he was blocking" +
									" available";
								roomText += newLines + newLines + "Turn right and enter the now available cave room (Key D)";
								roomText += "\nTake a step backward (Key S)";
								if (Input.GetKeyDown (KeyCode.D)) {
									currentRoom = rooms.riddle;
								}
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.hallStep10;
								}
							}
						} else if (currentRoom == rooms.cave7){
							roomText = "Your pokemon appear to be restless. There's a door with light pouring through the" +
								" keyhole up ahead";
							if (keyAcquired) {
								roomText += newLines + newLines + "Approach the door and try the lock " +
								"with the key you found earlier (Key W)";
								roomText += "\nTake a step backward (Key S)";
								if (Input.GetKeyDown (KeyCode.W)) {
									currentRoom = rooms.end;
								}
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.riddle;
								}	
							} else {
								roomText += newLines + "You tried opening the door but it didn't budge. Appears there's a key" +
									" necessary for opening it, maybe it's in one of the cave rooms passed earlier..";
								roomText += "\nTake a step backward (Key S)";
								if (Input.GetKeyDown (KeyCode.S)) {
									currentRoom = rooms.riddle;
								}
							}
						}else if (currentRoom == rooms.end){
							roomText = "You walk outside and find a patch of grass with a couple of items in the middle of them." +
								" Unknown are dancing around these items happily, and with a squint you see they are comprised of " +
								"a masterball, a stone plate, and a couple hyper potions. " + newLines + "Congratulations, You won!";
						} else if (currentRoom == rooms.title){
							roomText = "Start the game, press spacebar";
							if (Input.GetKeyDown (KeyCode.Space)) {
								currentRoom = rooms.entry;
							}
						}

					}
				}
			}
		}
		GetComponent<Text>().text = roomText;
	}
}
