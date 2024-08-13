using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    public class MovementController : MonoBehaviour, IMove
    {
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float rotationSpeed = 0.15f;
        [SerializeField] private float jumpHeight = 1f;
        [SerializeField] private float groundCheckRayDistance = 0.1f;
        [Tooltip("Mask layers to determine if a player is grounded")]
        [SerializeField] private LayerMask groundLayerMask;

        private Rigidbody body;
        private CapsuleCollider capsuleCollider;
        private Vector3 velocityVector = new Vector3(0, 0);
        private Vector3 lookTarget = new Vector3(0, 0);
        private Vector3 mouseLook = new Vector3(0, 0);
        
        private const float RayStartOffset = 0.1f;

        public Vector3 GetVelocity() => body.velocity;

        public void SetVelocity(Vector3 velocityVector)
        {
            this.velocityVector = velocityVector;
        }

        public void Jump()
        {
            Debug.Log($"{IsGrounded()}");

            if (!IsGrounded())
            {
                return;
            }

            var jumpForce = Mathf.Sqrt(jumpHeight * Physics.gravity.y * -2) * body.mass;
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public void LookAtTarget()
        {
            transform.SmoothLookAt(lookTarget, rotationSpeed);
        }

        public void SetLookTarget(Vector3 target)
        {
            lookTarget = target;
        }
       
        public bool IsGrounded()
        {
            var origin = capsuleCollider.transform.position;
            origin.y = origin.y - (capsuleCollider.height * 0.5f) + RayStartOffset;
            return Physics.Raycast(origin, Vector3.down, out RaycastHit hitResult, groundCheckRayDistance, groundLayerMask);
        }
        
        protected virtual void Awake()
        {
            body = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            Assert.IsNotNull(body, $"{gameObject} Rigidbody is null");
            Assert.IsNotNull(capsuleCollider, $"{gameObject} capsule collider is null");
        }

        protected virtual void Start()
        {
            groundCheckRayDistance += RayStartOffset;
        }
        
        protected virtual void Update()
        {
            LookAtTarget();
        }

        protected virtual void FixedUpdate()
        {
            body.velocity = new Vector3(velocityVector.x * movementSpeed, GetVelocity().y, velocityVector.y * movementSpeed);
        }
    }
}
