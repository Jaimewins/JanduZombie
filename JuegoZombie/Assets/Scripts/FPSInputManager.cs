using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInputManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LookRotation lookRotation;
    [SerializeField] private float sensitivity = 3.0f;
    private Vector2 mouseAxis;
    private Vector2 inputAxis;

    private void Update()
    {
        // Modificar la camara del player

        // Rotación de la cámara
        mouseAxis = Vector2.zero;
        mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
        mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;
        lookRotation.SetRotation(mouseAxis);


        // Pasar input al player
        inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        player.SetAxis(inputAxis);
        // El salto del player
        if (Input.GetButton("Jump")) player.StartJump();

        // Disparar
        if (Input.GetButtonDown("Fire1")) player.Fire1();



        // Mouse Cursor
        //Cursor del ratón
        if (Input.GetMouseButtonDown(0)) MouseCursor.HideCursor();
        else if (Input.GetKeyDown(KeyCode.Escape)) MouseCursor.ShowCursor();

    }
}
