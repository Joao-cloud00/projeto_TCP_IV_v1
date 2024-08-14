using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 touchStartPosition;
    public float touchRadius = 0.5f; // Margem de erro para detectar o toque dentro do objeto

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchInsideObject(touchPosition))
                    {
                        isDragging = true;
                        touchStartPosition = touchPosition;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        if (IsTouchInsideObject(touchPosition)) // Movimenta apenas se o toque estiver dentro do objeto
                        {
                            rb.position = touchPosition;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }

    bool IsTouchInsideObject(Vector2 touchPosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider == Physics2D.OverlapCircle(touchPosition, touchRadius);
    }
}








