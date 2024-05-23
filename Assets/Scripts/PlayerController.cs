using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
	public bool walking;
	public Transform playerTrans;
    private CharacterController characterController;

    private float gravity = -9.81f;
    private float gravityMultiplier = 2f;
    private float velocity;
    private Vector3 move;

    public float Speed = 5f;
    public float Sprintspeedmultiplier = 2f;

	[SerializeField]
	private Transform cameraTransform;

	

	
    void Start(){
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate(){
	    // Calcula o movimento baseado nos inputs do eixo horizontal e vertical
	    move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	    move = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * move;
	    move.Normalize();

	    // Verifica se o personagem está correndo
	    float speedMultiplier = walking && Input.GetKey(KeyCode.LeftShift) ? Sprintspeedmultiplier : 1f;

	    // Aplica a gravidade
	    if (characterController.isGrounded && velocity < 0)
	    {
		    velocity = -2f;  // Um pequeno valor negativo para manter o personagem firmemente no chão
	    }
	    velocity += gravity * gravityMultiplier * Time.deltaTime;
	    move.y = velocity;

	    // Move o personagem
	    characterController.Move(move * Speed * speedMultiplier * Time.deltaTime);

	    // Roda a personagem apenas quando se move para frente
	    if (move.magnitude > 0.1f && move.x != 0 || move.z != 0)  // Ajuste a verificação para se adequar às suas necessidades
	    {
		    if (!Input.GetKey(KeyCode.S))
		    {
			    transform.rotation = Quaternion.LookRotation(new Vector3(-move.x, 0, -move.z));
		    }
	    }
    }
	void Update(){
		
		if(Input.GetKeyDown(KeyCode.W)){
			playerAnim.SetTrigger("runforward");
			playerAnim.ResetTrigger("idle");
			walking = true;
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.W)){
			playerAnim.ResetTrigger("runforward");
			playerAnim.SetTrigger("idle");
			walking = false;
			//steps1.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			playerAnim.SetTrigger("runbackward");
			playerAnim.ResetTrigger("idle");
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.S)){
			playerAnim.ResetTrigger("runbackward");
			playerAnim.SetTrigger("idle");
			//steps1.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			playerAnim.SetTrigger("runleft");
			playerAnim.ResetTrigger("idle");
			walking = true;
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.A)){
			playerAnim.ResetTrigger("runleft");
			playerAnim.SetTrigger("idle");
			walking = false;
			//steps1.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			playerAnim.SetTrigger("runright");
			playerAnim.ResetTrigger("idle");
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.D)){
			playerAnim.ResetTrigger("runright");
			playerAnim.SetTrigger("idle");
			//steps1.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			playerAnim.SetTrigger("jump");
			playerAnim.ResetTrigger("idle");
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			playerAnim.ResetTrigger("jump");
			playerAnim.SetTrigger("idle");
			//steps1.SetActive(false);
		}
		if(walking){				
			if(Input.GetKeyDown(KeyCode.LeftShift)){
				//steps1.SetActive(false);
				//steps2.SetActive(true);
				playerAnim.SetTrigger("sprint");
				playerAnim.ResetTrigger("runforward");
			}
			if(Input.GetKeyUp(KeyCode.LeftShift)){
				//steps1.SetActive(true);
				//steps2.SetActive(false);
				playerAnim.ResetTrigger("sprint");
				playerAnim.SetTrigger("runforward");
			}
            
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if(hasFocus){
			Cursor.lockState = CursorLockMode.Locked;
		}
		else{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	void ApplyGravity()
	{
    if (characterController.isGrounded)
    {
        velocity = 0f;  // Reseta a velocidade de queda quando no chão
    }
    else
    {
        velocity += gravity * gravityMultiplier * Time.deltaTime;  // Aplica a gravidade acumulativa
    }
    move.y = velocity;  // Atualiza o componente vertical do movimento
	}
}