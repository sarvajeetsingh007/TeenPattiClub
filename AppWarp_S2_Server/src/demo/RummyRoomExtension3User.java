package demo;

import com.shephertz.app42.server.idomain.BaseTurnRoomAdaptor;
import com.shephertz.app42.server.idomain.HandlingResult;
import com.shephertz.app42.server.idomain.ITurnBasedRoom;
import com.shephertz.app42.server.idomain.IUser;
import com.shephertz.app42.server.idomain.IZone;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Dictionary;
import java.util.List;

import org.apache.commons.lang.ObjectUtils.Null;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class RummyRoomExtension3User extends BaseTurnRoomAdaptor {

	private ITurnBasedRoom gameRoom;
    private IZone izone;
    ArrayList<IUser> pausedUserList = new ArrayList<IUser>();
    
    // GameData
    //private int[] CARDS = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51};
    private ArrayList<Integer> CARDS_DECK = new ArrayList<Integer>();
    //private ArrayList<Integer> CARDS_DECK = new ArrayList<Integer>();
    
    private PlayerInfo USER_1 = new PlayerInfo();
    private PlayerInfo USER_2 = new PlayerInfo();
    private PlayerInfo USER_3 = new PlayerInfo();
    private PlayerInfo USER_4 = new PlayerInfo();
    private PlayerInfo USER_5 = new PlayerInfo();
    
    private final int MAX_NO_OF_CARDS = 3;// for each user
    
    private int dealerIndex = -1;
    private int turnIndex = 0;
    
    private int potValue;
    private int bootValue;
    
    public byte GAME_STATUS;
    
    public RummyRoomExtension3User(IZone izone, ITurnBasedRoom room){
        this.gameRoom = room;
        this.izone = izone;
        GAME_STATUS = CardsConstants.STOPPED;
    }
    
    /*
     * This is a RPC Method when user request for new Card from the deck
     */
    public ArrayList<Integer> seeCard(String username)
    {
    	ArrayList<Integer> Card = null;
        if(username.equals(USER_1.user.getName()))
        {
        	Card = USER_1.HAND;
        }
        else if(username.equals(USER_2.user.getName()))
        {
        	Card = USER_2.HAND;
        }
        else if(username.equals(USER_3.user.getName()))
        {
        	Card = USER_3.HAND;
        }
        else if(username.equals(USER_4.user.getName()))
        {
        	Card = USER_4.HAND;
        }
        else if(username.equals(USER_5.user.getName()))
        {
        	Card = USER_5.HAND;
        }
        
        return Card;
    }
    
    /*
     * This function is invoked when server receive a move request.
     */
    @Override
    public void handleMoveRequest(IUser sender, String moveData, HandlingResult result){
    	
    	System.out.println("handleMoveRequest: " + sender.getName() + "DAT: " + moveData);
        try{
            
        	int hashIndex = moveData.indexOf('#', 0);
        	String moveStatus = moveData.substring(0, hashIndex);
        	String moveValue = moveData.substring(hashIndex + 1, moveData.length());
        	
        	UserStatus status = UserStatus.PLAYING;
            if(Integer.parseInt(moveStatus) == CardsConstants.PACK)
            {
            	status = UserStatus.PACK;
            	
            }
//            else if(Integer.parseInt(moveStatus) == CardsConstants.SIDESHOW)
//            {
//            	
//            }
//            else
//            {
//            	
//            }
            	
        	if(sender.getName().equals(USER_1.user.getName()))
            {
        		USER_1.status = status;
            }
            else if(sender.getName().equals(USER_2.user.getName()))
            {
            	USER_2.status = status;
            }
            else if(sender.getName().equals(USER_3.user.getName()))
            {
            	USER_3.status = status;
            }
            else if(sender.getName().equals(USER_4.user.getName()))
            {
            	USER_4.status = status;
            }
            else if(sender.getName().equals(USER_5.user.getName()))
            {
            	USER_5.status = status;
            }
            
        	setNextUserTurn();
            
        }catch(Exception e){
            e.printStackTrace();
        }
        
        printAll("handleMoveRequest", true);
    }
    
    @Override
    public void handleTurnExpired(IUser turn, HandlingResult result)
    {
    	System.out.println("handleTurnExpired: " + turn.getName());
    	//gameRoom.BroadcastChat("SERVER",  turn.getName() + "Turn Expired");
    	if(turn.getName().equals(USER_1.user.getName()))
        {
    		USER_1.status = UserStatus.PACK;
        }
        else if(turn.getName().equals(USER_2.user.getName()))
        {
        	USER_2.status = UserStatus.PACK;
        }
        else if(turn.getName().equals(USER_3.user.getName()))
        {
        	USER_3.status = UserStatus.PACK;
        }
        else if(turn.getName().equals(USER_4.user.getName()))
        {
        	USER_4.status = UserStatus.PACK;
        }
        else if(turn.getName().equals(USER_5.user.getName()))
        {
        	USER_5.status = UserStatus.PACK;
        }
    }
    
    /*
     * This function is invoked when server receive a chat request.
     */
    @Override
    public void handleChatRequest(IUser sender, String message, HandlingResult result){
        
    	
    }
    
    /*
     * This function is invoked when server receive leave user request. In case of two users, user left in room will be declared winner
     */
    @Override
    public void handleUserLeavingTurnRoom(IUser user, HandlingResult result){
        
    	if(user.getName().equals(USER_1.user.getName()))
        {
    		USER_1.user = null;
			USER_1.status = UserStatus.LEFT;
        }
        else if(user.getName().equals(USER_2.user.getName()))
        {
        	USER_2.user = null;
			USER_2.status = UserStatus.LEFT;
        }
        else if(user.getName().equals(USER_3.user.getName()))
        {
        	USER_3.user = null;
			USER_3.status = UserStatus.LEFT;
        }
        else if(user.getName().equals(USER_4.user.getName()))
        {
        	USER_4.user = null;
			USER_4.status = UserStatus.LEFT;
        }
        else if(user.getName().equals(USER_5.user.getName()))
        {
        	USER_5.user = null;
			USER_5.status = UserStatus.LEFT;
        }
    	
    	if(GAME_STATUS != CardsConstants.RUNNING)
        {
            return;
        }
    	
        if(gameRoom.getJoinedUsers().size()==1)
        {
        	// if two users are playing and one of them left room
            
            String message = user.getName();
            gameRoom.BroadcastChat(CardsConstants.SERVER_NAME, CardsConstants.RESULT_USER_LEFT+"#"+message);
            gameRoom.setAdaptor(null);
            //izone.deleteRoom(gameRoom.getId());
            gameRoom.stopGame(CardsConstants.SERVER_NAME);
        }
    }
    
    public void onUserPaused(IUser user){
        if(gameRoom.getJoinedUsers().contains(user)){
            pausedUserList.add(user);
            GAME_STATUS = CardsConstants.PAUSED;
            gameRoom.stopGame(CardsConstants.SERVER_NAME);
        }
    }
    
    public void onUserResume(IUser user){
        if(pausedUserList.indexOf(user)!=-1){
            pausedUserList.remove(user);
        }
        if(pausedUserList.isEmpty()){
            GAME_STATUS = CardsConstants.RESUMED;
        }
    }
    
    /*
     * This method deal new hand for each user and send
     * chat message having his cards array
     */
    private void dealNewCards()
    {
    	System.out.println("dealNewCards");
    	CARDS_DECK.clear();
        for(int i=1;i<=CardsConstants.MAX_CARD;i++){
            CARDS_DECK.add(i);
        }
        
        Collections.shuffle(CARDS_DECK);
        for(int i=0;i<MAX_NO_OF_CARDS;i++)
        {
        	USER_1.HAND.add(CARDS_DECK.remove(0));
            USER_2.HAND.add(CARDS_DECK.remove(0));
            USER_3.HAND.add(CARDS_DECK.remove(0));
            USER_4.HAND.add(CARDS_DECK.remove(0));
            USER_5.HAND.add(CARDS_DECK.remove(0));
        }
        
        dealerIndex++;
        if(dealerIndex == 5)
        	dealerIndex = 0;
        
        turnIndex = dealerIndex;
        
        List<IUser>list = gameRoom.getJoinedUsers();
        USER_1.status = UserStatus.WAITING;
       	USER_2.status = UserStatus.WAITING;
       	USER_3.status = UserStatus.WAITING;
       	USER_4.status = UserStatus.WAITING;
       	USER_5.status = UserStatus.WAITING;
       	
        List<String> userNames = new ArrayList<String>();
        if(USER_1.user != null)
        	userNames.add(USER_1.user.getName());
        if(USER_2.user != null)
        	userNames.add(USER_2.user.getName());
        if(USER_3.user != null)
        	userNames.add(USER_3.user.getName());
        if(USER_4.user != null)
        	 userNames.add(USER_4.user.getName());
        if(USER_5.user != null)
        	userNames.add(USER_5.user.getName());
        
        for(int i = 0; i < list.size(); ++i)
        {
        	IUser user = list.get(i);
        	int index = userNames.indexOf(user.getName());
        	if(index != -1)
        	{
        		list.remove(user);
        		if(index == 0)
        		{
        			USER_1.user = user;
        			USER_1.status = UserStatus.PLAYING;
        		}
        		else if(index == 1)
        		{
        			USER_2.user = user;
        			USER_2.status = UserStatus.PLAYING;
        		}
        		else if(index == 2)
        		{
        			USER_3.user = user;
        			USER_3.status = UserStatus.PLAYING;
        		}
        		else if(index == 3)
        		{
        			USER_4.user = user;
        			USER_4.status = UserStatus.PLAYING;
        		}
        		else if(index == 4)
        		{
        			USER_5.user = user;
        			USER_5.status = UserStatus.PLAYING;
        		}
        	}
        }
        
        for(int i = 0; i < list.size(); ++i)
        {
        	IUser user = list.get(i);
        	if(USER_1.status == UserStatus.WAITING)
    		{
    			USER_1.user = user;
    			USER_1.status = UserStatus.PLAYING;
    		}
    		else if(USER_2.status == UserStatus.WAITING)
    		{
    			USER_2.user = user;
    			USER_2.status = UserStatus.PLAYING;
    		}
    		else if(USER_3.status == UserStatus.WAITING)
    		{
    			USER_3.user = user;
    			USER_3.status = UserStatus.PLAYING;
    		}
    		else if(USER_4.status == UserStatus.WAITING)
    		{
    			USER_4.user = user;
    			USER_4.status = UserStatus.PLAYING;
    		}
    		else if(USER_5.status == UserStatus.WAITING)
    		{
    			USER_5.user = user;
    			USER_5.status = UserStatus.PLAYING;
    		}
        }
        
        printAll("dealNewCards", true);
    }
    
    @Override
    public void onTimerTick(long time){
        /*
         * Game start when minimum no. of users are joined
         */
    	System.out.println("onTimerTick... " + time);
        if(GAME_STATUS==CardsConstants.STOPPED && gameRoom.getJoinedUsers().size()>=CardsConstants.MIN_PLAYER_TOSTART){
            GAME_STATUS=CardsConstants.RUNNING;
            System.out.println("GAME START... ");
            dealNewCards();
            setNextUserTurn();
            gameRoom.startGame(CardsConstants.SERVER_NAME);
            gameRoom.BroadcastChat("SERVER", "HEY HEY GAME STARTED>>>>>>>>>>>>");
        }else if(GAME_STATUS==CardsConstants.RESUMED){
            GAME_STATUS=CardsConstants.RUNNING;
            gameRoom.startGame(CardsConstants.SERVER_NAME);
        }
      
    }
    
    @Override
    public void handleStartGameRequest(IUser sender, HandlingResult result)
    {
    	System.out.println("handleStartGameRequest... " + sender.getName());
    }
    
    /*
     * This function stop the game and notify the room players about winning user and his cards.
     */
    private void handleFinishGame(String winningUser, ArrayList<Integer> cards){
        try{
            JSONObject object = new JSONObject();
            object.put("win", winningUser);
            object.put("cards", cards);
            GAME_STATUS = CardsConstants.FINISHED;
            gameRoom.BroadcastChat(CardsConstants.SERVER_NAME, CardsConstants.RESULT_GAME_OVER+"#"+object);
            gameRoom.setAdaptor(null);
            izone.deleteRoom(gameRoom.getId());
            gameRoom.stopGame(CardsConstants.SERVER_NAME);
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    
    // for debugging 
    
    private void printAll(String TAG, boolean status){
        if(status){
            System.out.println("==================="+TAG+"======================");
//            System.out.println("USER_1:   "+USER_1);
//            System.out.println("USER_2:   "+USER_2);
//            System.out.println("USER_3:   "+USER_3);
//            System.out.println("USER_4:   "+USER_4);
//            System.out.println("USER_5:   "+USER_5);
            System.out.println("TOTAL_CA: "+CARDS_DECK);
        }
    }
    
    private void setNextUserTurn()
    {
    	PlayerInfo player = null;
        do
        {
        	turnIndex++;
        	if(turnIndex == 5)
        		turnIndex = 0;
        	
        	if(turnIndex == 0)
        	{
        		player = USER_1;
        	}
        	else if(turnIndex == 1)
        	{
        		player = USER_2;
        	}
        	else if(turnIndex == 2)
        	{
        		player = USER_3;
        	}
        	else if(turnIndex == 3)
        	{
        		player = USER_4;
        	}
        	else if(turnIndex == 4)
        	{
        		player = USER_5;
        	}
        	
        } while (player.status != UserStatus.PLAYING);
        
        System.out.println("NEXTTURN: " + player.user.getName());
        gameRoom.setNextTurn(player.user);
    }
}
