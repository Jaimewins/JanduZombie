using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    private Vector2 axis;
    private CharacterController controller;      
    public float speed;
    private float defaultSpeed;
    public Vector3 moveDirection;
    public float jumpSpeed;
    private bool jump;
    public float gravityMagnitude = 1.0f;

    private Vector3 transformDirection;
    public GameObject mano;
    public GameObject lanzacohetes;
    public GameObject manager;


    [SerializeField] private FireTemplate bullet;
    [SerializeField] private FireTemplate rocketBullet;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultSpeed = speed;
    }

    void Update()
    {    
        if (Input.GetKey(KeyCode.R) && manager.GetComponent<LevelManager>().stamina >= 1)
        {
            speed = defaultSpeed + 10;
            manager.GetComponent<LevelManager>().stamina = manager.GetComponent<LevelManager>().stamina - 1;
        }
        else
        {
            speed = defaultSpeed;
        }

        transformDirection = axis.x * transform.right + axis.y * transform.forward;

        moveDirection.x = transformDirection.x * speed;
        moveDirection.z = transformDirection.z * speed;

        if (controller.isGrounded && !jump)
        {
            moveDirection.y = Physics.gravity.y * gravityMagnitude * Time.deltaTime;
        }
        else
        {
            jump = false;
            moveDirection.y += Physics.gravity.y * gravityMagnitude * Time.deltaTime;
        }


        controller.Move(moveDirection * Time.deltaTime);
    }


    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }

    public void Fire()
    {
        if (manager.GetComponent<LevelManager>().left != 0)
        {
            Debug.Log("Fire");

        // Instanciar una pelota
            FireTemplate pelota = Instantiate(bullet, mano.transform.position, mano.transform.rotation) as FireTemplate;

        // Ponerla en la posición del player

        // Dispararla
            pelota.Fire();
            manager.GetComponent<LevelManager>().left = manager.GetComponent<LevelManager>().left - 1;
        }
    }

    public void Fire2()
    {
        if (manager.GetComponent<LevelManager>().right != 0)
        {
            Debug.Log("Fire2");

        // Instanciar una pelota
            FireTemplate misil = Instantiate(rocketBullet, lanzacohetes.transform.position, lanzacohetes.transform.rotation) as FireTemplate;

        // Ponerla en la posición del player

        // Dispararla
            misil.Fire();
            manager.GetComponent<LevelManager>().right = manager.GetComponent<LevelManager>().right - 1;
        }
    }

    public void Jump()
    {
        if (!controller.isGrounded) return;

        moveDirection.y = jumpSpeed;
        jump = true;
    }

/*
    if (controller.isGrounded && !jump)
        {
            moveDirection.y = forceToGround;
        }
        else
        {
            jump = false;
            moveDirection.y += Physics.gravity.y* gravityMagnitude * Time.deltaTime;
        }

        transformDirection = axis.x* transform.right + axis.y* transform.forward;

        moveDirection.x = transformDirection.x* speed;
        moveDirection.z = transformDirection.z* speed;

        controller.Move(moveDirection* Time.deltaTime);
*/
}
