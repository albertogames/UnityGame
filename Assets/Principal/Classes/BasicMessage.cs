using UnityEngine;
using System.Collections;


public class BasicMessage<type> {
	
    private type _parameter;

    private string _playerID; 
	
	public BasicMessage(string playerID){
		_playerID = playerID;
	}
	
    public string getPlayerID()
    {
        return _playerID;
    }

    public void setPlayerID(string playerID)
    {
        _playerID = playerID;
    }
	
	public void setParameter(type parameter)
    {
        _parameter = parameter;
    }

    public type getParameter()
    {
        return _parameter;
    }

   

}

