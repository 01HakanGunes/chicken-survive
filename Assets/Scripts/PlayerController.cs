using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _orientationTransform;
    private float _horizontalInput, _verticalInput;
    private Vector3 _moveDirection;

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
    }

    private void SetPlayerMovement()
    {
        _moveDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        _playerRigidbody.AddForce(_moveDirection.normalized * 10f, ForceMode.Force);
    }


}
