using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject buyChipsScreen;
	public GameObject optionScreen;

	public void backButtonClicked()
	{
		Application.LoadLevel ("LobbyScene");
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
		
//		RectTransform transform = option.GetComponent <RectTransform>();
//		transform.localPosition = new Vector2 (0.0f, 0.0f);
//		transform.anchorMax = new Vector2 (0.95f, 0.95f);
//		transform.anchorMin = new Vector2 (0.05f, 0.05f);
	}
}
