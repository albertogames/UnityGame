using UnityEngine;
using System.Collections;
using System;


public class MenuController : MonoBehaviour {
	
		
	public enum MenuAction{
		INITIALMENU,
		INITIALMENU_SETTINGS,
		INITIALMENU_SINGLEPLAYER,
		INITIALMENU_MULTIPLAYER,
		INITIALMENU_QUIT,
		
		MULTIPLAYER_CANCEL,
		MULTIPLAYER_CONNECT,
		
		
		LOGINMENU_LOG_OK,
		LOGINMENU_ACCEPT,
		LOGINMENU_CANCEL
	};
	
	private Window _settings;
	private Window _initialMenu;
	private Window _singlePlayer;
	private Window _multiPlayer;
	private Window _connecting;
	private Window _loging;
	
	private Window _actual;
	
	private CommunicationServer _comm;
	// Use this for initialization
	void Start() {
		_actual = null;
		Loging.Init(this);
		_loging = Loging.getInstance();
		_actual = _loging;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if (_actual != null){
			_actual.myOnGUI();
		}
	}
	
	public void menuAction(MenuAction action){
	
		
		switch (action){
			
			case MenuAction.INITIALMENU_SETTINGS:
				_actual = _settings;
				break;
			
			case MenuAction.INITIALMENU:
				_actual = _initialMenu;
				break;
			
			case MenuAction.INITIALMENU_SINGLEPLAYER:
				_actual = _singlePlayer;
				break;
			
			case MenuAction.INITIALMENU_MULTIPLAYER:{
					Multiplayer.Init(this);
					
					_multiPlayer = Multiplayer.getInstance();

					Messages.Message msg = new Messages.Message();
					msg.messageType = Messages.Message.MessageType.DISP_PLAY;
					msg.username = UserVariables._user;
			
			
					CommunicationServer.getInstance().sendMessage(msg);

					_actual = _multiPlayer;
				
					break;
				}
			case MenuAction.INITIALMENU_QUIT:
				Application.Quit();
				break;
			
			case MenuAction.MULTIPLAYER_CANCEL:{
					Multiplayer.Release();
				
					Messages.Message msg = new Messages.Message();
					msg.messageType = Messages.Message.MessageType.DISP_PLAY_CANCEL;
					msg.username = UserVariables._user;
					CommunicationServer.getInstance().sendMessage(msg);
				
					_actual = _initialMenu;
					break;
				}
			
			case MenuAction.MULTIPLAYER_CONNECT:
				
				break;
			
			case MenuAction.LOGINMENU_CANCEL:
				Application.Quit();
				break;
			
			case MenuAction.LOGINMENU_LOG_OK:
				loadMainMenu();
				break;
			
		}

	}
	
	private void loadMainMenu(){
		
		Settings.Init(this);
		InitialMenu.Init(this);
		
		_settings = Settings.getInstance();
		_initialMenu = InitialMenu.getInstance();
		
		_actual = _initialMenu;
	}
	
	
	
}
