using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public float runSpeed = 40f;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(Input.GetAxisRaw("Horizontal")); -> Para ver o input horizontal no console do unity
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if(Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} 
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
	}

	void FixedUpdate () {
		//Mover seu personagem
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); //primeiro false é pra dizer que não queremos agachar, segundo é que não queremos pular, fixedDeltaTime é o tempo que decorreu desde a última vez que essa função foi chamada
		jump = false;
	}
}
