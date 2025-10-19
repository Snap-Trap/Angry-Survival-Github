using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IMovable, IDamagable
{
    public InputAction moveLeft, moveRight, moveUp, moveDown;

    public float speed, health;

    public bool canMove = true;

    public Vector2 movementDirection = Vector2.zero;

    public void Start()
    {
        health = 100f;
        speed = 4;
    }

    public void Update()
    {
       Movement(speed);
    }


    public void Movement(float speed)
    {
        if (!canMove) return;

        Vector2 moveInput = Vector2.zero;

        moveInput.x = moveRight.ReadValue<float>() - moveLeft.ReadValue<float>();
        moveInput.y = moveUp.ReadValue<float>() - moveDown.ReadValue<float>();

        if (moveInput != Vector2.zero)
        {
            moveInput.Normalize();
            GameManagerScript.Instance.facingDirection = moveInput;
        }

        transform.Translate(moveInput * speed * Time.deltaTime);
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
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(0.2f);
        SceneHandler.Instance.LoadScene(2);
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

