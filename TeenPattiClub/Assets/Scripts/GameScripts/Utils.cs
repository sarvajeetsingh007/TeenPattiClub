using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	public static void showToastAlert(string message) {

		Debug.Log ("showToastAlert" + message);
		//Toast.makeText(ctx.getApplicationContext(), message, Toast.LENGTH_SHORT).show();
	}
	
	public static void showToastAlertOnUIThread(string message) {

		Debug.Log ("showToastAlertOnUIThread" + message);
		//Toast.makeText(ctx.getApplicationContext(), message, Toast.LENGTH_SHORT).show();
	}
}
