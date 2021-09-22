using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovement : MonoBehaviour {
    private const float WrapAroundOffset = 0.1f;
    
    [SerializeField] private float forceMagnitude = 500;
    [SerializeField] private float maxVelocity = 6;
    [SerializeField] private float rotationSpeed = 6;

    private Vector3 movementDirection;
    
    private Camera mainCamera;
    private Rigidbody rigidbody;

    private void OnEnable() {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable() {
        EnhancedTouchSupport.Disable();
    }

    void Start() {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        ProcessInput();
        
        var newPosition = GetScreenWrappedPosition();
        var newRotation = GetRotationFacingVelocity();
        transform.SetPositionAndRotation(newPosition, newRotation);
    }

    private void ProcessInput() {
        var activeTouches = Touch.activeTouches;
        if (activeTouches.Count <= 0) {
            movementDirection = Vector3.zero;
            return;
        }
        var touch = activeTouches[0];
        var touchWorldPosition = mainCamera.ScreenToWorldPoint(touch.screenPosition);
        touchWorldPosition.y = 0;

        movementDirection = transform.position - touchWorldPosition;
        movementDirection.y = 0;
        movementDirection.Normalize();
    }

    private Vector3 GetScreenWrappedPosition() {
        var newPosition = transform.position;

        var viewportPosition = mainCamera.WorldToViewportPoint(newPosition);
        var viewportX = viewportPosition.x;
        var viewportY = viewportPosition.y;

        if (viewportX >= 1) {
            newPosition.x = -newPosition.x + WrapAroundOffset;
        } else if (viewportX <= 0) {
            newPosition.x = -newPosition.x - WrapAroundOffset;
        }

        if (viewportY >= 1) {
            newPosition.z = -newPosition.z + WrapAroundOffset;
        } else if (viewportY <= 0) {
            newPosition.z = -newPosition.z - WrapAroundOffset;
        }

        return newPosition;
    }
    
    private Quaternion GetRotationFacingVelocity() {
        var targetRotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
        return Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate() {
        if (movementDirection == Vector3.zero) return;
        rigidbody.AddForce(movementDirection * forceMagnitude);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
    }
}
