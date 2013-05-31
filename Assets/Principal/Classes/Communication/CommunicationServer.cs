using UnityEngine;

using System;

using System.Collections.Generic;
using System.Threading;

using System.IO;
using System.Net.Sockets;
using System.Text;

using ProtoBuf;

public class CommunicationServer: IThread{
	
	private static CommunicationServer _instance;
	
	private byte[] _message;
	private byte[] _buffer;
	private int _bytesReceived;
	private int _messageSize;
	private int _offset;
	private int _messageBytesLeft;
	
	private ASCIIEncoding _encoder;
	private TcpClient _client;
	private NetworkStream _clientStream = null;
	
	private List<ICommunicationListener>  _listeners;
	
	private CommunicationServer(){
		
		_message = new byte[20];
		_encoder = new ASCIIEncoding();
		_client = new TcpClient(UserVariables._ip,UserVariables._port);
		_clientStream = _client.GetStream();
		
		_listeners = new List<ICommunicationListener>();
		
	}
	
	~CommunicationServer(){
		if (_clientStream != null){
			_clientStream.Close();
			_clientStream = null;
		}
	}
	
	public static bool Init(){
		Assert.Test((_instance != null),"CommunicationServer.Init: Second initilization of CommunicationServer is not possible");
		
		_instance = new CommunicationServer();
	
		return true;
	}
	
	public static bool Release(){
		Assert.Test ((_instance == null),"CommunicationServer.getInstance: CommunicationServer not initialized");
		
		_instance.threadStopped();
		
		return true;
	}
	
	public static CommunicationServer getInstance(){
		Assert.Test ((_instance == null),"CommunicationServer.getInstance: CommunicationServer not initialized");
		
		return _instance;
	}
	
	public void sendMessage(Messages.Message message){
		Assert.Test ((_clientStream == null),"CommunicationServer.sendMessage: Communication server not connected");

		//_buffer = _encoder.GetBytes(message);
		byte[] dataBuffer;
		MemoryStream ms = new MemoryStream();
		Serializer.Serialize(ms, message);
		
		dataBuffer = ms.ToArray();
		
		byte[] buffer = new byte[dataBuffer.Length + 1];
		buffer[0] = (byte)dataBuffer.Length;

		Array.Copy(dataBuffer,0,buffer,1,dataBuffer.Length);
		
		_clientStream.Write (buffer, 0 , buffer.Length);
			
		
		
	}
	
	protected override void startThread ()
	{
		int errorCode;
		
		for (;!_stop;){
			try{
				byte[] buffer = new byte[40];	
			
			
				int _bytesReceived = _clientStream.Read(buffer,0,buffer.Length);
				
				_offset = 0;
				
				
				if (_bytesReceived > 0){
					
					if (_messageBytesLeft > 0){
						
						if (_messageBytesLeft >= _bytesReceived){
							
							System.Array.Copy(buffer,0,_message,_messageSize-_messageBytesLeft,_bytesReceived);
							
							_messageBytesLeft -= _bytesReceived;
							
							_bytesReceived = 0;
						
						}else{
							System.Array.Copy(buffer,0,_message,_messageSize-_messageBytesLeft,_messageBytesLeft);
							
							_bytesReceived -= _messageBytesLeft;
							
							_offset = _messageBytesLeft;
							
							_messageBytesLeft = 0;
							
							//Procesar mensaje
							MemoryStream stream = new MemoryStream(_message);	 
				
							Messages.Message message = Serializer.Deserialize<Messages.Message> (stream);
							notifyListeners(message);
							
						}
					
					}
				
				
					for (;((_messageBytesLeft == 0) && (_bytesReceived > 0));){
						
						_messageSize = buffer[0 + _offset];
						
						Array.Resize(ref _message,_messageSize);
						
						
						if (_messageSize > _bytesReceived -1){
							
							System.Array.Copy(buffer,_offset+1,_message,0,_bytesReceived);
							
							_messageBytesLeft += _messageSize - (_bytesReceived -1);
						
						}else if(_messageSize <= _bytesReceived -1){
							
							System.Array.Copy(buffer,_offset+1,_message,0,_messageSize);
							
							_bytesReceived -= _messageSize + 1;
							
							//Procesar mensaje
							MemoryStream stream = new MemoryStream(_message);	 
				
							Messages.Message message = Serializer.Deserialize<Messages.Message> (stream);
							notifyListeners(message);
							
							
								
							_messageBytesLeft = 0;
							
							if (_bytesReceived > 0){
								_offset += _messageSize + 1;
							}
						}
					}
				}else{
					
					if (_bytesReceived == 0){
						
					}else{
						
					}
					
				}
			}
			catch(IOException ioe){
			
			}catch(ObjectDisposedException ode){
			
			}
		}
		
		threadStopped();	
	}
	
	private void threadStopped(){
		_clientStream.Close();
		_clientStream = null;
	}
	
		
	public void addListener(ICommunicationListener listener){

		_listeners.Add(listener);
	}
	
	public void removeListener(ICommunicationListener listener){
		
		
		_listeners.Remove(listener);
	}
	
	private void notifyListeners(Messages.Message message){
		ICommunicationListener[] arrayList = _listeners.ToArray();
		for (int i = 0; i < arrayList.Length; i++){
			arrayList[i].messageFromServer(message);
		}
	}
	
	
	
	
}
