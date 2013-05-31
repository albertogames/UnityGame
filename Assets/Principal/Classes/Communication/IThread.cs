using UnityEngine;
using System.Collections;
using System.Threading;

public abstract class IThread {
	
	private Thread _thread = null;
	protected bool _stop = false;

	protected abstract void startThread();
	
	public void stop(){
		_stop = true;
	}
	
	~IThread(){
		if (_thread.IsAlive)
			_thread.Interrupt();
	}
	
	public void run(){
		
		_thread = new Thread(this.startThread);
		_thread.Start();
	}
}
