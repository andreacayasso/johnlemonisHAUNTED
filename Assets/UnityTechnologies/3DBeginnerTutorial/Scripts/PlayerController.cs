using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float normalSpeed = 5f; // Define your normal movement speed
    public float boostedSpeed = 10f; // Define your boosted movement speed
    public float boostMultiplier = 2f; // Multiplier for the speed boost

    private float currentSpeed; // Track the current speed
    private bool isBoosting = false; // Flag to check if boosting

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        currentSpeed = normalSpeed; // Set initial speed to normal speed
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // Check if spacebar is held down
        {
            isBoosting = true;
            currentSpeed = boostedSpeed; // Set speed to boosted speed when spacebar is held
        }
        else
        {
            isBoosting = false;
            currentSpeed = normalSpeed; // Set speed back to normal speed when spacebar is released
        }
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

        // Apply movement with the current speed
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * currentSpeed * Time.deltaTime);
    }

    void OnAnimatorMove ()
    {
        // Only rotate the character if it's moving
        if (isBoosting || Mathf.Abs(m_Movement.magnitude) > 0)
        {
            m_Rigidbody.MoveRotation (m_Rotation);
        }
    }
}
