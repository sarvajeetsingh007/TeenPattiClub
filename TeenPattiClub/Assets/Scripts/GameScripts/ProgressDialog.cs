using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressDialog : MonoBehaviour {

	public Button cancelButton;
	public Text text;

	public void show (string msg)
	{
		gameObject.SetActive (true);
		cancelButton.gameObject.SetActive (false);
		text.text = msg;
		Debug.Log (msg);
	}

	public void setCancelable(bool flag)
	{
		cancelButton.gameObject.SetActive (flag);
	}

	public void dismiss ()
	{
		gameObject.SetActive (false);
		cancelButton.gameObject.SetActive (false);
	}

	public void cancelButtonClicked()
	{
		gameObject.SetActive (false);
		cancelButton.gameObject.SetActive (false);
	}
}
