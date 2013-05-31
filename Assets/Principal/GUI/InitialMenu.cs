using UnityEngine;
using System.Collections;


public class InitialMenu : Window {

	
	private static InitialMenu _instance;
	private static MenuController _controller;
	
	
	public static bool Init(MenuController controller){
		
		Assert.Test((_instance != null),"InitialMenu.Init: Second initilization of InitialMenu is not possible");
		
		_instance = new InitialMenu();
		
		_controller = controller;
		
		return true;
	}
	
	public static InitialMenu getInstance(){
		
		Assert.Test ((_instance == null),"InitialMenu.getInstance: Instance not initialized");
		
		return _instance;
	
	}
	
	public override void myOnGUI() {
		
			if (GUI.Button (new Rect(10,10,100,30),"Single Player"))
			{		
				_controller.menuAction(MenuController.MenuAction.INITIALMENU_SINGLEPLAYER);
			}
			if (GUI.Button (new Rect(10,50,100,30),"Multi Player"))
			{	
				_controller.menuAction(MenuController.MenuAction.INITIALMENU_MULTIPLAYER);	
			}
			
			if (GUI.Button (new Rect(10,90,100,30),"Settings"))
			{	
				_controller.menuAction(MenuController.MenuAction.INITIALMENU_SETTINGS);
			}
		
			if (GUI.Button (new Rect(10,130,100,30),"Exit"))
			{
				_controller.menuAction(MenuController.MenuAction.INITIALMENU_QUIT);
			}
		
	
	}
	
}
