using UnityEngine;
using System.Collections;

public class Loging : Window, ICommunicationListener {
	
	private static Loging _instance;
	
	private string _passwd;
	private string _user;
	private MenuController _controller;
	
	Loging(MenuController controller){
		
		_controller = controller;
		 
		_user = "";
		_passwd = "";
		
		CommunicationServer.getInstance().addListener(this);
		
	}
	
	public static bool Init(MenuController controller){
		
		Assert.Test((_instance != null),"Loging.Init: Second initilization of Loging is not possible");
		
		_instance = new Loging(controller);
		
		return true;
	}
	
	public static Loging getInstance(){
		
		Assert.Test ((_instance == null),"Loging.getInstance: Instance not initialized");
		
		return _instance;
	
	}
	
	public override void myOnGUI(){
		
		if (GUI.Button (new Rect(10,50,100,30),"Accept"))
		{	
			acceptClick();	
			_controller.menuAction(MenuController.MenuAction.LOGINMENU_ACCEPT);	
		}
		
		if (GUI.Button (new Rect(10,90,100,30),"Cancel"))
		{	
			_controller.menuAction(MenuController.MenuAction.LOGINMENU_CANCEL);
		}
		
		_user = GUI.TextField (new Rect (10,130,100,30),_user, Constants.TEXTFIELD_LENGHT);
		
		_passwd = GUI.PasswordField (new Rect(10,170,100,30),_passwd,'*',Constants.TEXTFIELD_LENGHT);
		
	}
	
	private void acceptClick(){
		CommunicationServer comInstace = CommunicationServer.getInstance();
		
		Messages.Message msg = new Messages.Message();
		msg.messageType = Messages.Message.MessageType.LOG;
		msg.login = new Messages.Message.Login();
		msg.username = _user;
		msg.login.password = _passwd;
		
		comInstace.sendMessage(msg);
		
		//comInstace.sendMessage(Constants.COM_LOG + ":" + _user + ":" + _passwd + ":" );
	}
	
	public void messageFromServer(Messages.Message message){
		int a = 0;
		Messages.Message.MessageType type = message.messageType;
		switch (type){
			case Messages.Message.MessageType.LOG_OK:
				_controller.menuAction(MenuController.MenuAction.LOGINMENU_LOG_OK);
				UserVariables._user = _user;
				UserVariables._password = _passwd;
				break;
		
		
		}
	}
	
}
