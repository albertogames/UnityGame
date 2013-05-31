using UnityEngine;
using System.Collections;

public class Parser  {
	
	private static Parser _instance;
	private char _character;
	
	Parser(char character){
		_character = character;
	}

	
	public static bool Init(char character){
		Assert.Test((_instance != null),"Parser.Init: Second initilization of Parser is not possible");
		
		_instance = new Parser(character);

		return true;
	}
	
	public static Parser getInstance(){
		Assert.Test ((_instance == null),"Parser.getInstance: Parser not initialized");
		
		return _instance;
	}
	
	public void setCharacter(char character){
		_character = character;
	}
	
	public string[] parseMessage(string message){
	
		string[] words = message.Split(_character);
		return words;
		
	}
	
	
}
