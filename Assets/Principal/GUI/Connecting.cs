using UnityEngine;
using System.Collections;

public class Connecting : Window {

	private static Connecting _instance;
	private static MenuController _controller;
	
	public static bool Init(MenuController controller){
		
		Assert.Test((_instance != null),"Connecting.Init: Second initilization of Connecting is not possible");
		
		_instance = new Connecting();
		
		_controller = controller;
		
		return true;
	}
	
	public static Connecting getInstance(){
		
		Assert.Test ((_instance == null),"Connecting.getInstance: Instance not initialized");
		
		return _instance;
	
	}
	
	public override void myOnGUI(){
		if (GUI.Button (new Rect(10,10,100,30),"Connecting"))
		{		

		}
		
	}
	
	
}
