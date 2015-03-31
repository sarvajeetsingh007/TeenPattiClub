using UnityEngine;
using System.Collections;

public class PopUpManager : MonoBehaviour {

	public GameObject LeaderBoardScreen;
	public GameObject GiftScreen;
	public GameObject ProfileScreen;
	public GameObject ShopScreen;
	public GameObject SettingScreen;
	public GameObject chatScreen;

	public void shopButtonClicked()
	{
		ShopScreen.SetActive (true);
	}
	
	public void settingButtonClicked()
	{
		SettingScreen.SetActive (true);
	}
	
	public void leaderBoardButtonClicked()
	{
		LeaderBoardScreen.SetActive (true);
	}
	
	public void profileButtonClicked()
	{
		ProfileScreen.SetActive (true);
	}
	
	public void chatButtonClicked()
	{
		chatScreen.SetActive (true);
	}
	
	public void giftButtonClicked()
	{
		GiftScreen.SetActive (true);
	}

	public void rateButtonClicked(GameObject screenObj)
	{
		screenObj.SetActive (false);
	}

	public void closeButtonClicked(GameObject screenObj)
	{
		screenObj.SetActive (false);
	}

	public void logOutButtonClicked()
	{
		Application.LoadLevel ("TeenPatti");
	}
}
