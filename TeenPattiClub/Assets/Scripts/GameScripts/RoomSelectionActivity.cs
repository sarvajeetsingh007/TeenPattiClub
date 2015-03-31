using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using com.shephertz.app42.gaming.multiplayer.client.SimpleJSON;

public class RoomSelectionActivity : MonoBehaviour, RoomRequestListener, ZoneRequestListener {

	private WarpClient theClient;
	public ProgressDialog progressDialog;
	public GameObject gameView;

	void Awake()
	{
		getWarpInstance();
		DontDestroyOnLoad (gameObject);
	}

	void Start ()
	{
		Debug.Log ("RoomSelectionActivity " + Application.loadedLevelName);

		theClient.AddZoneRequestListener(this);
		theClient.AddRoomRequestListener(this);
	}

	void OnDestroy ()
	{
		Debug.Log ("ROOM SELECTION OnDestroy");
		theClient.RemoveZoneRequestListener(this);
		theClient.RemoveRoomRequestListener(this);
	}

	public void joinRoomWithOption(bool roomIsPrivate)
	{
		string message = "";
		if (roomIsPrivate)
		{
			//PRIVATE ROOM
			message = Constants.JOIN_PRIVATE_ROOM_TEXT;

			createNewTurnRoom (true);
		}
		else
		{
			//PUBLIC ROOM
			message = Constants.JOIN_PUBLIC_ROOM_TEXT;

			//theClient.JoinRoomInRange (Constants.MIN_USER_COUNT, Constants.MAX_USER_COUNT, Constants.MAX_PREFERRED);
			theClient.JoinRoomWithProperties (getTableProperty(false));
		}

		progressDialog.show(message);

	}

	public void joinRoomWithRoomID(string roomID)
	{
		Constants.ROOM_ID = roomID;
		theClient.JoinRoom (roomID);
	}

	private void createNewTurnRoom(bool roomIsPrivate)
	{
		Debug.Log ("createNewTurnRoom: " + roomIsPrivate);
		Constants.ROOM_ADMIN = Constants.userName;
		theClient.CreateTurnRoom (Constants.userName + "_Room", Constants.ROOM_ADMIN, Constants.ROOM_SIZE, getTableProperty(roomIsPrivate), Constants.TURN_TIME);
	}

	private void getWarpInstance()
	{
		try {
			theClient = WarpClient.GetInstance ();
		} catch (UnityException ex) {
			Utils.showToastAlert(Constants.ALERT_INIT_EXEC + " " + ex.Message);
		}
	}

	private void GoToGameView(RoomEvent eventObj)
	{
		// GoToGameView
		//Application.LoadLevel ("GameScene");
		gameView.SetActive (true);
	}

	private Dictionary<string, object> getTableProperty(bool roomIsPrivate)
	{
		Dictionary<string, object> tableProperty = new Dictionary<string, object> ();
		if (roomIsPrivate)
		{
			tableProperty.Add("Room_Type", "Private");
		}
		else
		{
			tableProperty.Add("Room_Type", "Public");
		}

		return tableProperty;
	}


	//RoomRequestListener
	#region RoomRequestListener
	public void onJoinRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onJoinRoomDone : " + eventObj.getResult());
		if(eventObj.getResult()==WarpResponseResultCode.SUCCESS)
		{
			Debug.Log (eventObj.getData().getName());
			Constants.ROOM_ADMIN = eventObj.getData().getRoomOwner();
			theClient.SubscribeRoom (eventObj.getData ().getId ());
		}
		else
		{
			createNewTurnRoom(false);
		}
	}

	public void onSubscribeRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onSubscribeRoomDone : " + eventObj.getResult());
		if(eventObj.getResult() == WarpResponseResultCode.SUCCESS)
		{
			GoToGameView(eventObj);
			RoomData data = eventObj.getData();
			Debug.Log ("ROOMDATA: " + data.getId());
		}
		else
		{
			Debug.Log ("onSubscribeRoomDone Error: " + eventObj.getData ().ToString ());
		}
	}
	
	public void onUnSubscribeRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onUnSubscribeRoomDone : " + eventObj.getResult());
	}
	
	public void onLockPropertiesDone(byte result)
	{
		//Debug.Log ("onLockPropertiesDone : " + result);
	}
	
	public void onUnlockPropertiesDone(byte result)
	{
		//Debug.Log ("onUnlockPropertiesDone : " + result);
	}
	
	public void onLeaveRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onLeaveRoomDone : " + eventObj.getResult());
	}

	public void onGetLiveRoomInfoDone (LiveRoomInfoEvent eventObj)
	{
		//Debug.Log ("onGetLiveRoomInfoDone : " + eventObj.getResult());
	}
	
	public void onSetCustomRoomDataDone (LiveRoomInfoEvent eventObj)
	{
		//Debug.Log ("onSetCustomRoomDataDone : " + eventObj.getResult());
	}
	
	public void onUpdatePropertyDone(LiveRoomInfoEvent eventObj)
	{
		if (WarpResponseResultCode.SUCCESS == eventObj.getResult())
		{
			//Debug.Log ("UpdateProperty event received with success status");
		}
		else
		{
			//Debug.Log ("Update Propert event received with fail status. Status is :" + eventObj.getResult().ToString());
		}
	}

	public void onInvokeRoomRPCDone(RPCEvent evnt)
	{

	}
	#endregion
	
	//ZoneRequestListener
	#region ZoneRequestListener
	public void onDeleteRoomDone (RoomEvent eventObj)
	{
		//Log ("onDeleteRoomDone : " + eventObj.getResult());
	}
	
	public void onGetAllRoomsDone (AllRoomsEvent eventObj)
	{
		//Log ("onGetAllRoomsDone : " + eventObj.getResult());
		for(int i=0; i< eventObj.getRoomIds().Length; ++i)
		{
			//Log ("Room ID : " + eventObj.getRoomIds()[i]);
		}
	}
	
	public void onCreateRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onCreateRoomDone : " + eventObj.getResult());
		if(eventObj.getResult()==WarpResponseResultCode.SUCCESS)
		{
			// if room created successfully
			joinRoomWithRoomID (eventObj.getData().getId());
		}
		else
		{
			Utils.showToastAlert(Constants.ALERT_ROOM_CREATE + " " + eventObj.getResult());
		}
	}
	
	public void onGetOnlineUsersDone (AllUsersEvent eventObj)
	{
		//Log ("onGetOnlineUsersDone : " + eventObj.getResult());
	}
	
	public void onGetLiveUserInfoDone (LiveUserInfoEvent eventObj)
	{
		//Log ("onGetLiveUserInfoDone : " + eventObj.getResult());
	}
	
	public void onSetCustomUserDataDone (LiveUserInfoEvent eventObj)
	{
		//Log ("onSetCustomUserDataDone : " + eventObj.getResult());
	}
	
	public void onGetMatchedRoomsDone(MatchedRoomsEvent eventObj)
	{
		if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
		{
			//Log ("GetMatchedRooms event received with success status");
			foreach (var roomData in eventObj.getRoomsData())
			{
				//Log("Room ID:" + roomData.getId());
			}
		}
	}

	public void onInvokeZoneRPCDone(RPCEvent evnt)
	{

	}
	#endregion
}
