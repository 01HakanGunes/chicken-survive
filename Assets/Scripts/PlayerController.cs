using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _orientationTransform;
    private float _horizontalInput, _verticalInput;
    private Vector3 _moveDirection;
    [SerializeField] private float _moveSpeed = 10f;
    private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private float _jumpForce = 2f;
    private bool _isGrounded = false;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight = 2f;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(_jumpKey))
        {
            CheckGrounded();
            if (_isGrounded)
            {
                SetPlayerJump();
            }
        }
    }

    private void SetPlayerMovement()
    {
        _moveDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        _playerRigidbody.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode.Force);
    }

    private void SetPlayerJump()
    {
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false;
    }


    private void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight / 2 + 0.1f, _groundLayer);
    }
}
