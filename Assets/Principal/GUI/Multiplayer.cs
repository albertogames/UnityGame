using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Multiplayer : Window, ICommunicationListener {

	private static Multiplayer _instance;
	private MenuController _controller;
	
	private Vector2 scrollViewVector = Vector2.zero;
	
	private static List<string> _playersConnected;
	
	private int selected;
	private Vector2 scrollPos;
	
	Multiplayer(MenuController controller){
		
		_controller = controller;
		
		_playersConnected = new List<string>();
		
		CommunicationServer.getInstance().addListener(this);
		
	}
	
	
	public static bool Init(MenuController controller){
		
		Assert.Test((_instance != null),"Multiplayer.Init: Second initilization of Multiplayer is not possible");
		
		_instance = new Multiplayer(controller);
		
		
		
		return true;
	}
	
	public static Multiplayer getInstance(){
		
		Assert.Test ((_instance == null),"Multiplayer.getInstance: Instance not initialized");
		
		return _instance;
	
	}
	
	public static void Release(){
		
		CommunicationServer.getInstance().removeListener(_instance);
		_playersConnected.Clear();
		_instance = null;
		_playersConnected = null;
		
		
	}
	
	public override void myOnGUI() {
		
			if (GUI.Button (new Rect(10,10,100,30),"Cancel"))
			{		
				_playersConnected.Clear();
				_controller.menuAction(MenuController.MenuAction.MULTIPLAYER_CANCEL);
			
			}else{
				scrollViewVector = GUI.BeginScrollView(new Rect ( 10,300,200,100), scrollViewVector, new Rect ( 0,0,150,150));
				//GUI.Label(new Rect(0,0,100,20), innerText);
				if (_playersConnected.Count > 0){
					selected = GUI.SelectionGrid (new Rect ( 0,0,100,100),selected,_playersConnected.ToArray(),1);
				}
				GUI.EndScrollView();
			}		
	
	}
	
	public void messageFromServer(Messages.Message message){
		Messages.Message.MessageType type = message.messageType;
		switch (type){
			case Messages.Message.MessageType.USER_AVAILABLE_PLAY:{
					if (UserVariables._user.CompareTo(message.username) != 0){
						_playersConnected.Add(message.username);
					}
					break;
				}
			
			case Messages.Message.MessageType.USER_AVAILABLE_PLAY_CANCEL:{
					if (UserVariables._user.CompareTo(message.username) != 0){
						_playersConnected.Remove(message.username);
					}
					break;
				}
		
		
		}
	}
}
