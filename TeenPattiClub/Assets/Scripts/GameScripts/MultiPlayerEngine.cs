using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;
using com.shephertz.app42.gaming.multiplayer.client.SimpleJSON;

public class MultiPlayerEngine : MonoBehaviour, ConnectionRequestListener, RoomRequestListener, TurnBasedRoomListener, NotifyListener {

	private WarpClient theClient;
	public ProgressDialog progressDialog;

	// Game Data
	private string roomId;
	private int GAME_STATUS;
	private int CARD_STATUS;
	private bool isUserTurn = false;

	private List<PlayingCard> USER_CARD = new List<PlayingCard>();

	private string serverMessage = "";

	public void Log(string msg)
	{
		serverMessage = serverMessage + "\n" + msg;
	}

	void Awake()
	{
		getWarpInstance();
	}
	
	void Start ()
	{
		Debug.Log ("MULTIPLAYER ENGINE");
		roomId = Constants.ROOM_ID;
		CARD_STATUS = Constants.BLIND;
		GAME_STATUS = Constants.STOPPED;
		theClient.AddConnectionRequestListener(this);
		theClient.AddRoomRequestListener(this);
		theClient.AddTurnBasedRoomRequestListener(this);
		theClient.AddNotificationListener(this);
		progressDialog.show("Waiting to start game");
		progressDialog.setCancelable(true);
	}
	
	void OnDestroy ()
	{
		Debug.Log ("ENGINE DESTROYED");
		theClient.RemoveTurnBasedRoomRequestListener(this);
		theClient.RemoveRoomRequestListener(this);

		theClient.RemoveConnectionRequestListener(this);
		theClient.RemoveNotificationListener(this);
	}
	
	private void getWarpInstance()
	{
		try {
			theClient = WarpClient.GetInstance ();
		} catch (UnityException ex) {
			Utils.showToastAlert(Constants.ALERT_INIT_EXEC + " " + ex.Message);
		}
	}
	
	public void SendMove(string move)
	{
		if(!isUserTurn)
		{
			Utils.showToastAlert(Constants.ALERT_INV_MOVE);
			return;
		}
		else
		{
			string moveData = (Constants.userName + ": " + move);
			//JSONNode node = JSONNode.Parse (moveData);

			theClient.sendMove(moveData);
		}
	}
	
	private void handleMessage(int code, string data)
	{
		if(code==Constants.RESULT_USER_LEFT)
		{
			showNotificationDialog(data);
		}
		else if(code==Constants.RESULT_GAME_OVER)
		{
			showResultDialog(data);
		}
	}
	
//	private void handleGamePause(string username){
//		// handleGamePause
//		if(gameStatusDialog==null){
//			gameStatusDialog = new Dialog(this);
//		}
//		gameStatusDialog.setContentView(R.layout.custom_dialog);
//		gameStatusDialog.setTitle(Constants.SERVER_NAME);
//		TextView text = (TextView) gameStatusDialog.findViewById(R.id.dialogText);
//		text.setText("This Game is stopped by "+username +" because all user's are" +
//		             " not online this time.");
//		Button dialogButton = (Button) gameStatusDialog.findViewById(R.id.dialogOKButton);
//		dialogButton.setOnClickListener(new OnClickListener() {
//			@Override
//			public void onClick(View v) {
//				Utils.showToastAlert("Game Paused");
//			}
//		});
//		gameStatusDialog.show();
//	}

	private void handleGameStop(string username)
	{
		//Handle Game Stopped by server
	}
	
	private void showNotificationDialog(string message)
	{

	}
	
	private void showResultDialog(string data)
	{

	}

	public void onCardSeeButtonPressed ()
	{
		CARD_STATUS = Constants.SEEN;
		//PlayingCard[] cards = USER_CARD.ToArray();

	}

	public void onBackPressed()
	{
		handleLeave();
	}
	
	private void handleLeave()
	{
		Debug.Log("handleLeave called");
		if(roomId!=null){
			theClient.LeaveRoom (roomId);
			roomId = null;
		}

//		theClient.Disconnect();
	}

//	public void onGameStarted(string sender, string rId, string nextTurn)
//	public void onGameStopped(string sender, string roomId)
//	public void onMoveCompleted(MoveEvent move)	
//	public void onChatReceived (ChatEvent eventObj)

	//#region ConnectionRequestListener
	public void onConnectDone(ConnectEvent eventObj)
	{
		Debug.Log ("ENGINE onConnectDone : " + eventObj.getResult() + eventObj.ToString());
	}
	
	public void onInitUDPDone(byte res)
	{
		
	}
	
	public void onDisconnectDone(ConnectEvent eventObj)
	{
		Debug.Log("onDisconnectDone : " + eventObj.getResult());
	}
	//#endregion
	
	//RoomRequestListener
	//#region RoomRequestListener
	public void onSubscribeRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onSubscribeRoomDone : " + eventObj.getResult());
	}
	
	public void onUnSubscribeRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onUnSubscribeRoomDone : " + eventObj.getResult());
	}
	
	public void onJoinRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onJoinRoomDone: " + eventObj);	
	}
	
	public void onLockPropertiesDone(byte result)
	{
		Debug.Log ("onLockPropertiesDone : " + result);
	}
	
	public void onUnlockPropertiesDone(byte result)
	{
		Debug.Log ("onUnlockPropertiesDone : " + result);
	}
	
	public void onLeaveRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onLeaveRoomDone : " + eventObj.getResult());
	}
	
	public void onGetLiveRoomInfoDone (LiveRoomInfoEvent eventObj)
	{
		Debug.Log ("onGetLiveRoomInfoDone : " + eventObj.getResult());
	}
	
	public void onSetCustomRoomDataDone (LiveRoomInfoEvent eventObj)
	{
		Debug.Log ("onSetCustomRoomDataDone : " + eventObj.getResult());
	}
	
	public void onUpdatePropertyDone(LiveRoomInfoEvent eventObj)
	{
		if (WarpResponseResultCode.SUCCESS == eventObj.getResult())
		{
			Debug.Log ("UpdateProperty event received with success status");
		}
		else
		{
			Debug.Log ("Update Propert event received with fail status. Status is :" + eventObj.getResult().ToString());
		}
	}

	public void onInvokeRoomRPCDone(RPCEvent evnt)
	{
		Debug.Log ("onInvokeRoomRPCDone : " + evnt.ToString());
	}
	//#endregion
	
	//TurnBasedRoomListener
	//#region TurnBasedRoomListener
	public void onSendMoveDone(byte result)
	{
		Debug.Log ("onSendMoveDone : " + result);
	}
	
	public void onStartGameDone(byte result)
	{
		Debug.Log ("onStartGameDone : " + result);
	}
	
	public void onStopGameDone(byte result)
	{
		Debug.Log ("onStopGameDone : " + result);
	}
	
	public void onSetNextTurnDone(byte result)
	{
		Debug.Log ("onSetNextTurnDone : " + result);
	}
	
	public void onGetMoveHistoryDone(byte result, MoveEvent[] moves)
	{
		Debug.Log ("onGetMoveHistoryDone : " + result);
	}
	//#endregion
	
	//ZoneRequestListener
	//#region ZoneRequestListener
	public void onDeleteRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onDeleteRoomDone : " + eventObj.getResult());
	}
	
	public void onGetAllRoomsDone (AllRoomsEvent eventObj)
	{
		Debug.Log ("onGetAllRoomsDone : " + eventObj.getResult());
		for(int i=0; i< eventObj.getRoomIds().Length; ++i)
		{
			Debug.Log ("Room ID : " + eventObj.getRoomIds()[i]);
		}
	}
	
	public void onCreateRoomDone (RoomEvent eventObj)
	{
		Debug.Log ("onCreateRoomDone : " + eventObj.getResult());
	}
	
	public void onGetOnlineUsersDone (AllUsersEvent eventObj)
	{
		Debug.Log ("onGetOnlineUsersDone : " + eventObj.getResult());
	}
	
	public void onGetLiveUserInfoDone (LiveUserInfoEvent eventObj)
	{
		Debug.Log ("onGetLiveUserInfoDone : " + eventObj.getResult());
	}
	
	public void onSetCustomUserDataDone (LiveUserInfoEvent eventObj)
	{
		Debug.Log ("onSetCustomUserDataDone : " + eventObj.getResult());
	}
	
	public void onGetMatchedRoomsDone(MatchedRoomsEvent eventObj)
	{
//		if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
//		{
//			Log ("GetMatchedRooms event received with success status");
//			foreach (var roomData in eventObj.getRoomsData())
//			{
//				Log("Room ID:" + roomData.getId());
//			}
//		}
	}		
	//#endregion
	
	//NotifyListener
	//#region NotifyListener
	
	public void onRoomCreated (RoomData eventObj)
	{
		Debug.Log ("onRoomCreated");
		
	}
	public void onPrivateUpdateReceived (string sender, byte[] update, bool fromUdp)
	{
		Debug.Log ("onPrivateUpdate");
	}
	public void onRoomDestroyed (RoomData eventObj)
	{
		Debug.Log ("onRoomDestroyed");
	}
	
	public void onUserLeftRoom (RoomData eventObj, string username)
	{
		Debug.Log ("onUserLeftRoom : " + username);
	}
	
	public void onUserJoinedRoom (RoomData eventObj, string username)
	{
		Debug.Log ("onUserJoinedRoom : " + username);
		
		/*if (Utility.Instance.isNewRoomCreated)
		{
			Utility.Instance.startGame();
		}*/
	}
	
	public void onUserLeftLobby (LobbyData eventObj, string username)
	{
		Debug.Log ("onUserLeftLobby : " + username);
	}
	
	public void onUserJoinedLobby (LobbyData eventObj, string username)
	{
		Debug.Log ("onUserJoinedLobby : " + username);
	}
	
	public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable)
	{
		Debug.Log ("onUserChangeRoomProperty : " + sender);
	}
	
	public void onPrivateChatReceived(string sender, string message)
	{
		Debug.Log ("onPrivateChatReceived : " + sender);
	}
	
	public void onMoveCompleted(MoveEvent move)
	{
		Debug.Log ("onMoveCompleted by: " + move.getSender() + "\nData: " + move.getMoveData () + "\nNextTurn: " + move.getNextTurn ());
		if((move.getNextTurn ()).Equals(Constants.userName))
		{
			isUserTurn = true;
			Debug.Log("Your Turn");
		}
		else
		{
			isUserTurn = false;
			Debug.Log("Turn: " + move.getNextTurn ());
		}
	}
	
	public void onChatReceived (ChatEvent eventObj)
	{
		Debug.Log(eventObj.getSender() + " sended " + eventObj.getMessage());
		
		if(eventObj.getSender().Equals(Constants.SERVER_NAME))
		{
			if((eventObj.getMessage().IndexOf('#')) != -1)
			{
				int hashIndex = eventObj.getMessage().IndexOf('#');
				string _code = eventObj.getMessage().Substring(0, hashIndex);
				string message = eventObj.getMessage().Substring(hashIndex+1, eventObj.getMessage().Length);
				try {
					int CODE = int.Parse(_code);
					//int.TryParse(_code, out CODE);
					if(CODE==Constants.RESULT_GAME_OVER || CODE==Constants.RESULT_USER_LEFT)
					{
						GAME_STATUS = Constants.STOPPED;
						handleMessage(CODE, message);
					}
					else
					{
						JSONNode obj = JSON.Parse(message);
						JSONArray cardArray = obj.AsArray;
						for(int i=0;i<cardArray.Count;i++)
						{
							USER_CARD.Add(new PlayingCard(int.Parse(cardArray[i]["value"]), int.Parse(cardArray[i]["suit"])));
						}

						//initScreen();
					}
				} catch (UnityException ex) {
					Debug.Log("GameActivity onChatReceived:NumberFormatException " + ex.Message);
				}
			}
		}
	}
	
	public void onUpdatePeersReceived (UpdateEvent eventObj)
	{
		Debug.Log ("onUpdatePeersReceived");
	}
	
	public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<string, System.Object> properties)
	{
		Debug.Log("Notification for User Changed Room Propert received");
		Debug.Log(roomData.getId());
		Debug.Log(sender);
		foreach (KeyValuePair<string, System.Object> entry in properties)
		{
			Debug.Log("KEY:" + entry.Key);
			Debug.Log("VALUE:" + entry.Value.ToString());
		}
	}
	
	public void onUserPaused(string locid, bool isLobby, string username)
	{
		Debug.Log("onUserPaused");
	}
	
	public void onUserResumed(string locid, bool isLobby, string username)
	{
		Debug.Log("onUserResumed");
	}
	
	public void onGameStarted(string sender, string rId, string nextTurn)
	{
		Debug.Log("onGameStarted");
		roomId = rId;
		if(GAME_STATUS==Constants.STOPPED)
		{
			if( !ReferenceEquals(null, progressDialog) ) //if(progressDialog!=null)
			{
				progressDialog.dismiss();
				progressDialog=null;
			}
			
			GAME_STATUS = Constants.RUNNING;
		}
		
		if(nextTurn.Equals(Constants.userName))
		{
			isUserTurn = true;
			Debug.Log("Your Turn");
		}
		else
		{
			isUserTurn = false;
			Debug.Log("Turn: "+nextTurn);
		}
	}
	
	public void onGameStopped(string sender, string roomId)
	{
		Debug.Log("onGameStopped");
		if(sender.Equals(Constants.SERVER_NAME))
		{
			if(GAME_STATUS == Constants.RUNNING)
			{
				GAME_STATUS = Constants.PAUSED;
				handleGameStop(sender);
			}
		}
		
	}
	
	public void onNextTurnRequest (string lastTurn)
	{
		Debug.Log("onNextTurnRequest");
	}
	
	//#endregion
}
