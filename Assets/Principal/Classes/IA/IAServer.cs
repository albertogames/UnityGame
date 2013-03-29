using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAServer{
	
	private static IAServer _instance;
	
	private IAStarNode[,] _map;
	
	private ILogicMap _logicMap;
	
 	private IAServer() {
		
	}
	
	
	public static bool Init(){
		Assert.Test((_instance != null),"IAServer.Init: Second initilization of IAServer is not possible");
	    
		_instance = new IAServer();                                                                                                                                                                                                 
	
		return true;	
	}
	
	public static IAServer getInstance(){
		                                                                                                                                                           
		Assert.Test ((_instance == null),"IAServer.getInstance: Instance not initialized");
		
		return _instance;
	}
	
	public void setIAMap(ILogicMap map){
		_logicMap = map;
	}
	
	public List<IAStarNode> calculateRoute(Vector2 initialPos, Vector2 finalPos){
		
		float time = Time.realtimeSinceStartup;
		
		List<IAStarNode> _closedList = new List<IAStarNode>();
		PriorityQueue<IAStarNode> _openList = new PriorityQueue<IAStarNode>();
		
		IAStarNode finalNode = new IAStarNode((int) finalPos.x, (int) finalPos.y, null);

		//Add the starting square (or node) to the open list
		_openList.addElement(new IAStarNode((int) initialPos.x, (int) initialPos.y, null),0);
		
		
		//Look for the loswest F cost square on the opne list.
		IAStarNode currentNode = _openList.getFirst();
		
		
		//Stop when:
		//Add the target square to the closed list, in which case the path has been found (see note below), or
		//Fail to find the target square, and the open list is empty. In this case, there is no path.  
		while((!_openList.isEmpty()) && (!currentNode.isSameState(finalNode))){                                                                                                                    
			
			//Switch it to the closed list
			_openList.removeFirst();       
			
			_closedList.Add(currentNode);
			
			//For each of the 8 squares adjacent to this current square:
			List<ICell> cellList = _logicMap.getAdjacents(currentNode.getPosX(),currentNode.getPosY());
			
			foreach(ICell cell in cellList){
				
				//If it is not walkable or if it is on the closed list ignore it.
				if (cell.isFree()){
					
					IAStarNode aux = new IAStarNode(cell.getPosX(),cell.getPosZ(),currentNode);
					                                                                                                                                                                                                                                                                                                                                                                                                                                             
					if (!_closedList.Contains(aux)){
						
						//If it isn't on the open list
						IAStarNode cont = _openList.contains(aux);
						
						if (cont == null){
							
							//add it to the opne list. Make the current square the parent of this square. Record F,G,H costs of the square
							aux.calculate(finalNode);
							
							_openList.addElement(aux,aux.getF());
							
						}else{
							//If it is on the open list already, check to see if this path to that square is better, using G cost as the measure. 
							//A lower G cost means that this is a better path. If so, change the parent of the square to the current square, and recalculate the G and F scores of the square. 
							//If you are keeping your open list sorted by F score, you may need to resort the list to account for the change.
							if (cont.getG() > aux.getG()){
								_openList.delete(cont);
								_openList.addElement(aux,aux.getF());
							}
							
							
						}

					}
				}
			}
				
			//Look for the loswest F cost square on the opne list.
			currentNode = _openList.getFirst();
		
		}
		List<IAStarNode> _finalList = new List<IAStarNode>();
		
		
		_finalList.Add(currentNode);
		
		if (currentNode != null){
			
			while (currentNode.getParent() != null){
				_finalList.Add(currentNode.getParent());
				currentNode = currentNode.getParent();
			}
			
			_finalList.Add(currentNode);
			
			_finalList.Reverse();
			
		}
		
		time = Time.realtimeSinceStartup - time;
		
		MonoBehaviour.print("TimePath: " + time);
		
		
		return _finalList;
	}
		
	
}
