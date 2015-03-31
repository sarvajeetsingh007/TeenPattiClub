using UnityEngine;
using System.Collections;

public class Constants {

	public static string userName;
	public static string ROOM_ADMIN;
	public static string ROOM_ID;

	public static string APP_KEY = "15a0291a-c40e-4bb9-8";
	
	public static string HOST_NAME =  "192.168.1.8";

	public const int PORT_NO =  12346;
	
	public const string FB_APP_ID = "421905537937918";

	public const int MIN_USER_COUNT = 1;

	public const int MAX_USER_COUNT = 4;

	public const bool MAX_PREFERRED = true;

	public const int ROOM_SIZE = 5;
	
	public const int RECCOVERY_ALLOWANCE_TIME = 15;
	
	public const int MAX_RECOVERY_ATTEMPT = 10;
	
	public const int TURN_TIME = 10;
	
	public const int TOTAL_CARDS = 52;
	
	public static string SERVER_NAME = "AppWarpS2";
	
	public const byte USER_HAND = 1;
	
	public const byte RESULT_GAME_OVER = 3;
	public const byte RESULT_USER_LEFT = 4;
	
	// error code
	public const int INVALID_MOVE = 121;
	public const byte SUBMIT_CARD = 111;
	
	// GAME_STATUS
	
	public const int STOPPED = 71;
	public const int RUNNING = 72;
	public const int PAUSED = 73;

	// CARD_STATUS
	
	public const int BLIND = 81;
	public const int SEEN = 82;

	// String constants

	public static string JOIN_PRIVATE_ROOM_TEXT = "Creating Room...";
	public static string JOIN_PUBLIC_ROOM_TEXT = "Joining Room...";
	public static string RECOVER_TEXT = "Recovering...";
	
	// Alert Messages
	public static string ALERT_INIT_EXEC = "Exception in Initilization";
	public static string ALERT_ERR_DISCONN = "Can't Disconnect";
	public static string ALERT_INV_MOVE = "Invalid Move: Not Your Turn";
	public static string ALERT_ROOM_CREATE = "Room creation failed";
	public static string ALERT_CONN_FAIL = "Connection Failed";
	public static string ALERT_SEND_FAIL = "Send Move Failed";
	public static string ALERT_CONN_SUCC = "Connection Success";
	public static string ALERT_CONN_RECOVERED = "Connection Recovered";
	public static string ALERT_CONN_ERR_RECOVABLE = "Recoverable connection error. Recovering session after 5 seconds";
	public static string ALERT_CONN_ERR_NON_RECOVABLE = "Non-recoverable connection error.";
	
	public const string HOW_TO_PLAY = "Login: You can login with";
}

public enum Suit
{
	SPADE,
	HEART,
	DIAMOND,
	CLUB
}
