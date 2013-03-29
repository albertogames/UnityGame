using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public float limitLeft;
	public float limitRight;
	public float limitUp;
	public float limitDown;
	
	public float velocityLeft;
	public float velocityRight;
	public float velocityUp;
	public float velocityDown;
	
	public float velocityScrollUp;
	public float velocityScrollDown;
	
	
	public float lerpVelocity;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
		//Obtener las coordenadas relativas del artón
		float positionX = Input.mousePosition.x/Screen.width;
		float positionY = Input.mousePosition.y/Screen.height;
		
		
		float speed = Time.deltaTime*lerpVelocity;
		
		if(Input.GetAxis("Mouse ScrollWheel") < 0){
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x, transform.position.y + (1 * velocityScrollUp), transform.position.z), speed);
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x, transform.position.y - (1 * velocityScrollDown), transform.position.z), speed);
		}
		
		if(positionX >= (1.0 - limitRight)){
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x + (1 * velocityRight), transform.position.y, transform.position.z), speed);
			
		}
		
		if(positionX  <= (limitLeft)){
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x - (1 * velocityLeft), transform.position.y, transform.position.z), speed);
		}
		
		if(positionY  >= (1.0 - limitUp)){
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x, transform.position.y, transform.position.z + (1 * velocityUp)), speed);
		}
		
		if(positionY <= (limitDown)){
			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(transform.position.x, transform.position.y, transform.position.z - (1 * velocityDown)), speed);
		}
		
		
	}
	
	
}
