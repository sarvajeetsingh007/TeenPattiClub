package demo;

import java.util.ArrayList;

import com.shephertz.app42.server.idomain.IUser;

enum UserStatus
{
	WAITING,
	LEFT,	
	STANDUP,
	PLAYING,
	PACK
}

public class PlayerInfo {

	public UserStatus status;
	public long totalChips;
	public IUser user;
	public ArrayList<Integer> HAND = new ArrayList<Integer>();
//	public long turnAmount;
	public PlayerInfo()
	{
		this.status = UserStatus.LEFT;
	}
}
