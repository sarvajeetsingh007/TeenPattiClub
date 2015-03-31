using UnityEngine;
using System.Collections;

public class LobbyManager : MonoBehaviour {

	public GameObject buyChipsScreen;
	public GameObject progressDialog;
	public GameObject optionScreen;
	public GameObject gameScene;

	public void playButtonClicked()
	{
		Debug.Log ("PUBLIC Room");
//		RoomSelectionActivity roomActivity = progressDialog.GetComponent<RoomSelectionActivity> ();
//		roomActivity.joinRoomWithOption (false);
		gameScene.SetActive (true);
	}

	public void privateButtonClicked()
	{
		Debug.Log ("PRIVATE Room");
		RoomSelectionActivity roomActivity = progressDialog.GetComponent<RoomSelectionActivity> ();
		roomActivity.joinRoomWithOption (true);
	}

	public void tournamentButtonClicked()
	{
		Debug.Log ("TOURNAMENT Room");
		Application.LoadLevel ("GameScene");
	}

	public void buyChipsButtonClicked()
	{
		GameObject buyChips = (GameObject)Instantiate(buyChipsScreen);
		buyChips.transform.SetParent (gameObject.transform, false);

//		RectTransform transform = buyChips.GetComponent <RectTransform>();
//		transform.localPosition = new Vector2 (0.0f, 0.0f);
//		transform.anchorMax = new Vector2 (0.95f, 0.95f);
//		transform.anchorMin = new Vector2 (0.05f, 0.05f);
	}

	public void buttonClicked()
	{
		GameObject option = (GameObject)Instantiate(optionScreen);
		option.transform.SetParent (gameObject.transform, false);
		
		RectTransform transform = option.GetComponent <RectTransform>();
//		transform.localPosition = new Vector2 (0.0f, 0.0f);
//		transform.anchorMax = new Vector2 (0.95f, 0.95f);
//		transform.anchorMin = new Vector2 (0.05f, 0.05f);
	}

	public void backButtonClicked()
	{
		Application.LoadLevel ("TeenPatti");
	}
}
