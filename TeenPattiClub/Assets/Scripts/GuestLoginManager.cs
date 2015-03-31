using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuestLoginManager : MonoBehaviour {

	public InputField nameField;
	public Image profileImage;

	public void loginButtonClicked()
	{
		Debug.Log ("NameField= " + nameField);
		Application.LoadLevel ("LobbyScene");
	}
	
	public void backButtonClicked()
	{
		Application.LoadLevel ("TeenPatti");
	}

	public void picSelectionButtonClicked(Button picBtn)
	{
		Debug.Log ("Button= " + picBtn);
	}

	public void cameraButtonClicked()
	{

	}

	public void photoLibraryButtonClicked()
	{

	}

	public void inputNameButtonClicked()
	{
		
	}
}
