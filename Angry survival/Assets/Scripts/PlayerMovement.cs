using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IMovable, IDamagable
{
    public InputAction moveLeft, moveRight, moveUp, moveDown;

    public float speed, health;

    public bool canMove = true;

    public bool isFacingRight;

    public Vector2 movementDirection = Vector2.zero;

    public void Start()
    {
        health = 100f;
        speed = 2;
    }

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

                GameManagerScript.Instance.isFacingRight = false;
            }
            if (moveRight.ReadValue<float>() == 1)
            {
                movementDirection.x = speed;
                GameManagerScript.Instance.isFacingRight = true;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage, remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died.");
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

