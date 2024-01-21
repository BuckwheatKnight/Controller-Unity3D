using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float Speed = 5f, JumpForce = 300f;

    private bool _isGrounded;
    private Rigidbody _rb;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        JumpLogic();
        MovementLogic();
    }

    void MovementLogic()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if (movement != Vector3.zero)
            RotateCharacter(movement);

        _animator.SetBool("Walking", movement != Vector3.zero);
        _rb.MovePosition(transform.position + movement * Speed * Time.fixedDeltaTime);
    }

    void RotateCharacter(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.fixedDeltaTime * 1000f);
    }

    void JumpLogic()
    {
        if (Input.GetButton("Jump") && _isGrounded)
            _rb.AddForce(Vector3.up * JumpForce);
    }

    void OnCollisionEnter(Collision collision) => IsGroundedUpdate(collision, true);
    void OnCollisionExit(Collision collision) => IsGroundedUpdate(collision, false);

    void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _isGrounded = value;
    }
}