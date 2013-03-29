using UnityEngine;
using System.Collections;

public class Cell : ICell {

	private int _posX;
	private int _posZ;
	private int _content;
	
	private float _coordsX;
	private float _coordsZ;
	
	public Cell(int posX,int posZ, float coordsX, float coordsZ){
		_posX = posX;
		_posZ = posZ;
		
		_coordsX = coordsX;
		_coordsZ = coordsZ;
	}
	
	public void setContent(int content){
		_content = content;
	}
	
	public int getContent(){
		return _content;
	}
	
	public float getCoordsX(){
		return _coordsX;
	}
	
	public float getCoordsZ(){
		return _coordsZ;
	}
	
	
	public override bool isFree(){
		return (_content == 0);
	}
	
	public override int getPosX(){
		return _posX;
	}
	
	public override int getPosZ(){
		return _posZ;
	}
}
