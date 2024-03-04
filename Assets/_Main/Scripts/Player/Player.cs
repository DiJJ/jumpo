using System.Collections;
using Main.SO;
using UnityEngine;
using Zenject;

namespace Main.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour {
        [SerializeField]
        private PlayerSo m_playerSo;
        [SerializeField] 
        private LayerMask m_groundMask;
        
        private Rigidbody2D m_rigidBody;
        private IInputService m_inputService;
        
        private bool m_isGrounded;
        private float m_jumpForce;
        private bool m_jumpAvailable;
        private bool m_jumpBuffered;
        
        private Coroutine m_coyoteTimeCoroutine;
        private Coroutine m_jumpBufferCoroutine;

        [Inject]
        private void Inject(IInputService inputService) {
            m_inputService = inputService;
        }
        
        private void Awake()
        {
            m_rigidBody = GetComponent<Rigidbody2D>();
            var speedToDistance = m_playerSo.AirSpeed / m_playerSo.JumpDistance;
            m_rigidBody.gravityScale = 2 * m_playerSo.JumpHeight * Mathf.Pow(speedToDistance, 2) / 9.81f;
            m_jumpForce = 2 * (m_playerSo.JumpHeight + .1f) * speedToDistance;
            m_inputService.OnJump += BufferJump;
        }

        private void FixedUpdate() {
            CheckForGround();
            UpdateVelocity();
            
            if (m_jumpBuffered)
                Jump();
        }
        
        private void UpdateVelocity()
        {
            var x = m_inputService.MoveInput * (m_isGrounded ? m_playerSo.MoveSpeed : m_playerSo.AirSpeed);
            var y = m_rigidBody.velocity.y;
        
            y = Mathf.Clamp(y, -m_playerSo.MaxFallSpeed, y);

            m_rigidBody.velocity = Vector2.right * x + Vector2.up * y;
        }

        private void BufferJump() {
            if (m_jumpBufferCoroutine != null)
                StopCoroutine(m_jumpBufferCoroutine);
            m_jumpBufferCoroutine = StartCoroutine(JumpBufferCoroutine());
        }
        
        private void Jump() {
            if (m_isGrounded || m_jumpAvailable) {
                m_rigidBody.velocity = new(m_rigidBody.velocity.x, m_jumpForce);

                m_jumpAvailable = false;
                m_jumpBuffered = false;
            }
        }
        
        private void CheckForGround()
        {
            m_isGrounded = Physics2D.Raycast(transform.position, Vector2.down, .45f, m_groundMask);

            if (m_isGrounded == false) {
                m_coyoteTimeCoroutine ??= StartCoroutine(CoyoteTimeCoroutine());
                return;
            }

            m_jumpAvailable = true;
            
            if (m_coyoteTimeCoroutine != null)
            {
                StopCoroutine(m_coyoteTimeCoroutine);
                m_coyoteTimeCoroutine = null;
            }
        }
        
        private IEnumerator CoyoteTimeCoroutine() {
            yield return new WaitForSeconds(.15f);
            m_jumpAvailable = false;
        }
        
        private IEnumerator JumpBufferCoroutine()
        {
            m_jumpBuffered = true;
            yield return new WaitForSeconds(.15f);
            m_jumpBuffered = false;
        }
    }
}
