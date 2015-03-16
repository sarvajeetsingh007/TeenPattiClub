using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeScreenManager : MonoBehaviour {

	public GameObject freePlayButton;
	public GameObject paidPlayButton;
	public GameObject FBButton;
	public GameObject guestButton;
	public GameObject backButton;
	public GameObject titleImage;
	public GameObject modelImage;

	public void freePlayButtonClicked()
	{
		freePlayButton.SetActive (false);
		paidPlayButton.SetActive (false);
		FBButton.SetActive (true);
		guestButton.SetActive (true);
		backButton.SetActive (true);
	}

	public void backFromFreePlayClicked()
	{
		freePlayButton.SetActive (true);
		paidPlayButton.SetActive (true);
		FBButton.SetActive (false);
		guestButton.SetActive (false);
		backButton.SetActive (false);
	}

	public void paidPlayButtonClicked()
	{
		Application.LoadLevel ("SignUpScene");
	}

	public void guestButtonClicked()
	{
		Application.LoadLevel ("LobbyScene");
	}

	public void facebookButtonClicked()
	{
		Application.LoadLevel ("LobbyScene");
	}
}
