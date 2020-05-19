using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInputManager2 : MonoBehaviour
{
    //[SerializeField] Player player;
    [SerializeField] LookRotation lookRotation;
    [SerializeField] private float sensitivity = 3.0f;
    [SerializeField] private Vector2 mouseAxis;
    [SerializeField] private Vector2 inputAxis;
    [SerializeField] private PlayerManagement player;

    private void Awake()
    {
        MouseCursor.HideCursor();
    }

    private void Update()
    {
        // Modificar la camara del player

        // Rotación de la cámara
        mouseAxis = Vector2.zero; //{ 0 , 0}
        mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
        mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;

        lookRotation.SetRotation(mouseAxis);


        // Obtener el input
        inputAxis = Vector2.zero; //{ 0 , 0}
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        player.SetAxis(inputAxis);

        // Detectar el input para saltar
        if (Input.GetButtonDown("Jump"))
        {
            player.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            MouseCursor.HideCursor();
            player.Fire();            
        }

        if (Input.GetButtonDown("Fire2"))
        {
            MouseCursor.HideCursor();
            player.Fire2();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MouseCursor.ShowCursor();
        }
    }
}
