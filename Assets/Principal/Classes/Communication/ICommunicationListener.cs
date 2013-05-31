using UnityEngine;
using System.Collections;

public interface ICommunicationListener{

	void messageFromServer(Messages.Message message);
}
