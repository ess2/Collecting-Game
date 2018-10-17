using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 40f;
	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(Input.GetAxisRaw("Horizontal")); -> Para ver o input horizontal no console do unity
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
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

	public void OnLanding() {
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool IsCrouching) {
		animator.SetBool("IsCrouching", IsCrouching);
	}

	void FixedUpdate () {
		//Mover seu personagem
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); //primeiro false é pra dizer que não queremos agachar, segundo é que não queremos pular, fixedDeltaTime é o tempo que decorreu desde a última vez que essa função foi chamada
		jump = false;
	}
}
