using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using com.shephertz.app42.gaming.multiplayer.client.SimpleJSON;

public class GameController : MonoBehaviour, ConnectionRequestListener {

	private Text nameEditText;
	private WarpClient theClient;
	public ProgressDialog progressDialog;
//	private string authData = "";

	void Awake()
	{
		Debug.Log ("GameController");
		try {
			WarpClient.initialize(Constants.APP_KEY, Constants.HOST_NAME, Constants.PORT_NO);
			WarpClient.setRecoveryAllowance(Constants.RECCOVERY_ALLOWANCE_TIME);
			theClient = WarpClient.GetInstance();
		} catch (UnityException ex) {
			Utils.showToastAlert(Constants.ALERT_INIT_EXEC + " " + ex.Message);
		}
	}

	// Use this for initialization
	void Start () {
		//Debug.Log ("Start");
		theClient.AddConnectionRequestListener (this);
		onPlayGameClicked ();
	}

	void OnDestroy ()	{
		Debug.Log ("GameCONT OnDestroy");
		theClient.RemoveConnectionRequestListener (this); 
	}
	
	public void onPlayGameClicked()
	{
		Constants.userName = @"SarvajeetSingh";
		loginToAppWarp(Constants.userName, "");
	}

//	public void onFacebookProfileRetreived(boolean success) {
//		//		Log.d("onFacebookProfileRetreived", ""+success);
//		if(progressDialog!=null){
//			progressDialog.dismiss();
//			progressDialog = null;
//		}
//		if(success){
//			// do success logic
//			Log.d("UserContext.AccessToken", UserContext.AccessToken);
//			Log.d("UserContext.MyUserName", UserContext.MyUserName);
//			try {
//				JSONObject data = new JSONObject();
//				data.put("token", UserContext.AccessToken);
//				loginToAppWarp(UserContext.MyUserName, data.toString());
//			} catch (Exception e) {
//				Utils.showToastAlert("onFacebookProfileRetreived"+ " " + e.toString());
//			}
//		}
//	}
	
	private void loginToAppWarp(string name, string authData){

		Debug.Log ("loginToAppWarp: " + name);
		theClient.Connect(name, authData);
		progressDialog.show("connecting to appwarp...");
		progressDialog.setCancelable(true);
	}
	
	//ConnectionRequestListener
	#region ConnectionRequestListener
	public void onConnectDone(ConnectEvent eventObj)
	{
		if( !ReferenceEquals(null, progressDialog) ) //if(progressDialog!=null)
		{
			progressDialog.dismiss();
			progressDialog = null;
		}

		Debug.Log ("GAMECONTROLLER onConnectDone : " + eventObj.getResult());
		if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
		{
			Debug.Log ("SUCCESS");
			// SUCCESS
		}
		else if (eventObj.getResult() == WarpResponseResultCode.BAD_REQUEST)
		{
			theClient.Disconnect();
		}
		else
		{
			Utils.showToastAlert(Constants.ALERT_CONN_FAIL + " " + eventObj.getResult());
		}
	}
	
	public void onInitUDPDone(byte res)
	{
	}
	
	public void onDisconnectDone(ConnectEvent eventObj)
	{
		Debug.Log("onDisconnectDone : " + eventObj.getResult());
		if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
		{
			
		}
		else
		{
			Utils.showToastAlertOnUIThread(Constants.ALERT_ERR_DISCONN);
		}
	}
	#endregion

}
