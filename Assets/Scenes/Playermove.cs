using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    public Collider Baf1;
    public Collider Baf2;
    public Collider Baf3;

    public float baseSpeed = 12f;
    public float maxSpeed = 1000f;
    public float speedIncreaseRate = 1f;
    public TextMeshProUGUI playerSpeedText;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public double baf1 = 1.5;// улучшения (бафы)для скорости
    public double baf2 = 2;//улучшения (бафы)для скорости
    public double baf3 = 3;//улучшения (бафы)для скорости
    public Transform groundCheck;// проверка наличия земли для прыжка
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Collider col;// коллайдер триггер
    private Vector3 velocity;
    private bool isGrounded;
    public float boostAmount1 = 2f;
    public float boostAmount2 = 3f;
    public float boostAmount3 = 4f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerController player = collider.GetComponent<PlayerController>();

            if (gameObject.tag == "Trigger1")
            {
                player.speed *= boostAmount1;
            }
            else if (gameObject.tag == "Trigger2")
            {
                player.speed += boostAmount2;
            }
            else if (gameObject.tag == "Trigger3")
            {
                player.speed += boostAmount3;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            
        }
    }
    private void OnTriggerEnter()
    {
        
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerSpeedText.text = "" + Convert.ToInt32(baseSpeed*10);//вывод шагов






        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * baseSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            // Увеличение скорости за счёт времени при ходьбе
            baseSpeed = baseSpeed + speedIncreaseRate * Time.deltaTime;
        }
    }
}
