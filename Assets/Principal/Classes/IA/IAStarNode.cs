using UnityEngine;
using System;


public class IAStarNode : System.Object {
	
	private int _g;
	private int _h;
	private int _f;
	
	private int _posX;
	private int _posY;
	
	private bool _free;
	
	private IAStarNode _parent;

	public IAStarNode(int posX, int posY, IAStarNode parent){
		_posX = posX;
		_posY = posY;
		_free = true;
		_parent = parent;
		
		calculateG();
	}
	
	public IAStarNode getParent(){
		return _parent;
	}
	
	public int getPosX()
	{
		return _posX;
	}
	
	public int getPosY()
	{
		return _posY;
	}
	
	public bool isSameState(IAStarNode ANode){
		return ((ANode.getPosX() == _posX) && (ANode.getPosY() == _posY));
	}
	
	public int calculate(IAStarNode finalNode){
		_h = Mathf.Abs(_posX - finalNode.getPosX()) + Mathf.Abs(_posY - finalNode.getPosY());
		_f = _h + _g;
		return _f;
	}
	
	public void printNodeInfo(){
	}
	
	public void setFree(){
		_free = true;
	}
	
	public bool isFree(){
		return _free;
	}

	public int getG(){
		return _g;
	}
	
	public int getH(){
		return _h;
	}
	
	public int getF(){
		return _f;
	}
	
	
	public void setParent(IAStarNode parent){
		_parent = parent;
		calculateG ();	
	}
	
	private void calculateG(){
		if (_parent != null){
		
			int difX = _posX - _parent.getPosX();
			int difY = _posY - _parent.getPosY();
			
			if ((difX != 0 && difY == 0) || (difY != 0 && difX == 0)){
				//Vertical u horizonatal 10 
				_g = _parent.getG() + 10;
			}else{
				//Diagonal 14
				_g = _parent.getG() + 14; 
			}
			
		}else{
			_g = 0;
		}
	}
	
	public override bool Equals(System.Object obj){
		 // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to Point return false.
        IAStarNode p = obj as IAStarNode;
        if ((System.Object)p == null)
        {
            return false;
        }

		return ((p.getPosX() == _posX) && (p.getPosY() == _posY));
	}
	
	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}
	
	
}
