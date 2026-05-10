using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        public float moveSpeed = 6.0f;
        public float acceleration = 10.0f;
        public float deceleration = 5.0f;
        public float rotationSpeed = 10f;

        private Transform _camTransform;
        
        private Animator _animator;
        
        private Vector2 _moveInput;
        private Vector3 _currentVelocity;
        
        private Vector3 _targetVelocity;
        private Quaternion _targetRotation = Quaternion.identity;
        
        private PlayerInputActions _inputActions;
        
        private readonly int speedHash = Animator.StringToHash("Speed");

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += _ => _moveInput = Vector2.zero;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (Camera.main) 
                _camTransform = Camera.main.transform;
        }

        private void Update()
        {
            Vector3 inputDir = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

            Vector3 camForward = new Vector3(_camTransform.forward.x, 0f, _camTransform.forward.z).normalized;
            Vector3 camRight = new Vector3(_camTransform.right.x, 0f, _camTransform.right.z).normalized;
            
            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;
            moveDir.y = 0.0f;

            var dirSqrMagnitude = moveDir.sqrMagnitude;
            Vector3 targetVelocity = moveDir * moveSpeed;
            
            _currentVelocity = dirSqrMagnitude < 0.001f ? 
                Vector3.Lerp(_currentVelocity, Vector3.zero, deceleration * Time.deltaTime) : 
                Vector3.Lerp(_currentVelocity, targetVelocity, acceleration * Time.deltaTime);
            
            if (dirSqrMagnitude > 0.001f)
                _targetRotation = Quaternion.LookRotation(moveDir);

            float speedPercent = _currentVelocity.magnitude / moveSpeed;
            _animator.SetFloat(speedHash, speedPercent, 0.05f, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            
            Vector3 velocity = _currentVelocity;
            velocity.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = velocity;
            
            _rigidbody.MoveRotation(
                Quaternion.Slerp(_rigidbody.rotation, _targetRotation, rotationSpeed * fixedDeltaTime));
        }
    }
}