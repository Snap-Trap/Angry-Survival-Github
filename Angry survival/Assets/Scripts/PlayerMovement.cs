using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IMovable
{
    public InputAction moveLeft, moveRight, moveUp, moveDown;

    public float speed = 5;

    public bool canMove = true;

    public Vector2 movementDirection = Vector2.zero;

    public void Update()
    {
       Movement(speed);
    }


    public void Movement(float speed)
    {
        if (canMove)
        {
            movementDirection = Vector2.zero;
            if (moveLeft.ReadValue<float>() == 1)
            {
                movementDirection.x = -speed;
            }
            if (moveRight.ReadValue<float>() == 1)
            {
                movementDirection.x = speed;
            }
            if (moveUp.ReadValue<float>() == 1)
            {
                movementDirection.y = speed;
            }
            if (moveDown.ReadValue<float>() == 1)
            {
                movementDirection.y = -speed;
            }
            transform.Translate(movementDirection * speed * Time.deltaTime);
        }
    }

    public void Movable(bool value)
    {
        canMove = true;
    }

    public void OnEnable()
    {
        moveLeft.Enable();
        moveRight.Enable();
        moveUp.Enable();
        moveDown.Enable();
    }

    public void OnDisable()
    {
        moveLeft.Disable();
        moveRight.Disable();
        moveUp.Disable();
        moveDown.Disable();
    }
}

