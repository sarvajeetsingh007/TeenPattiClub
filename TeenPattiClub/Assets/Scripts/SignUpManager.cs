using UnityEngine;
using System.Collections;

public class SignUpManager : MonoBehaviour {

	public void signUpButtonClicked()
	{
		Application.LoadLevel ("LobbyScene");
	}

	public void backButtonClicked()
	{
		Application.LoadLevel ("TeenPatti");
	}
}
