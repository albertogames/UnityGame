using UnityEngine;
using System.Collections;

public class Settings : Window {

	private static Settings _instance;
	private static MenuController _controller;
	
	public static bool Init(MenuController controller){
		
		Assert.Test((_instance != null),"Settigns.Init: Second initilization of Settings is not possible");
		
		_instance = new Settings();
		
		_controller = controller;
		
		return true;
	}
	
	public static Settings getInstance(){
		
		Assert.Test ((_instance == null),"Settigns.getInstance: Instance not initialized");
		
		return _instance;
	
	}
	
	public override void myOnGUI(){
		
		if (GUI.Button (new Rect(10,50,100,30),"Accept"))
		{	
			acceptClick();	
			_controller.menuAction(MenuController.MenuAction.INITIALMENU);	
		}
		
		if (GUI.Button (new Rect(10,90,100,30),"Cancel"))
		{	
			_controller.menuAction(MenuController.MenuAction.INITIALMENU);
		}
		
	}
	
	private void acceptClick(){
		
	}
}
