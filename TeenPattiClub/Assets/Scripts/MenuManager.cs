using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject MenuObject;
	private Menu CurrentMenu;
	// Use this for initialization
	void Start () {


	}

	public void ShowMenu(GameObject obj)
	{
		Menu menu = obj.GetComponent<Menu> ();
		if (CurrentMenu != null)
			CurrentMenu.IsOpen = false;
		print ("MENU=" + menu);
		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}
}
