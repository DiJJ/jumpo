using System;
using DG.Tweening;
using Main.SO;
using UnityEngine;
using Zenject;

namespace Main.Player {
    [RequireComponent(typeof(Rigidbody2D), typeof(CoyoteTime), typeof(JumpBuffering))]
    public class Player : MonoBehaviour {
        [SerializeField]
        private PlayerSo m_playerSo;
        [SerializeField] 
        private LayerMask m_groundMask;
        [SerializeField] 
        private GameUI m_gameUI;
        [SerializeField] 
        private CoinPocket m_coinPocket;
        
        private Rigidbody2D m_rigidBody;
        private IInputService m_inputService;
        private JumpBuffering m_jumpBuffer;
        private CoyoteTime m_coyoteTimer;
        private WallJump m_wallJump;
        
        private bool m_isGrounded;
        private bool m_isWalled;
        private bool m_isWallInFront;
        private float m_jumpForce;

        private Vector3 m_latestSafePoint;

        public Vector2 Velocity => m_rigidBody.velocity;
        public bool IsGrounded => m_isGrounded;
        public bool IsWalled => m_isWalled;
        
        public event Action<bool> OnGroundedChanged = isGrounded => { };
        public event Action OnJumpPerformed = () => { };
        public event Action OnWallJumpPerformed = () => { };
        public event Action OnDie = () => { };
        public event Action OnRespawn = () => { };

        [Inject]
        private void Inject(IInputService inputService) {
            m_inputService = inputService;
        }
        
        private void Awake()
        {
            m_rigidBody = GetComponent<Rigidbody2D>();
            m_coyoteTimer = GetComponent<CoyoteTime>();
            m_jumpBuffer = GetComponent<JumpBuffering>();
            m_wallJump = GetComponent<WallJump>();
            
            var speedToDistance = m_playerSo.AirSpeed / m_playerSo.JumpDistance;
            m_rigidBody.gravityScale = 2 * m_playerSo.JumpHeight * Mathf.Pow(speedToDistance, 2) / 9.81f;
            m_jumpForce = 2 * (m_playerSo.JumpHeight + .1f) * speedToDistance;
        }

        private void FixedUpdate() {
            CheckForGround();
            CheckForSafePoint();
            CheckForWall();
            UpdateVelocity();
            
            if (m_jumpBuffer.IsJumpBuffered)
                Jump();
        }
        
        private void UpdateVelocity()
        {
            var x = m_inputService.MoveInput * (m_isGrounded ? m_playerSo.MoveSpeed : m_playerSo.AirSpeed);
            var y = m_rigidBody.velocity.y;
        
            y = Mathf.Clamp(y, -m_playerSo.MaxFallSpeed, y);

            if (m_isWalled && y <= -0.05f) {
                y = -m_playerSo.WallSlideSpeed;
            }

            if (m_wallJump.MovementLocked)
                x = m_rigidBody.velocity.x;

            x = Mathf.Lerp(m_rigidBody.velocity.x, x, 1-Mathf.Pow(2,-Time.deltaTime/.07f));
            m_rigidBody.velocity = Vector2.right * x + Vector2.up * y;
        }
        
        private void Jump() {
            if (m_isGrounded || (m_coyoteTimer.IsItCoyoteTime == true)) {
                m_rigidBody.velocity = new(m_rigidBody.velocity.x, m_jumpForce);
                OnJumpPerformed.Invoke();
            }
            
            if (m_isWalled && m_isGrounded == false)
            {
                if (m_isWallInFront)
                    transform.right = -transform.right;
                m_rigidBody.velocity = Vector3.up * m_jumpForce + transform.right * (m_jumpForce * 0.6f);
                OnWallJumpPerformed.Invoke();
            }
        }
        
        private void CheckForGround()
        {
            var size = new Vector2(0.5f, .1f);
            m_isGrounded = Physics2D.BoxCast(transform.position, size, 0f, -transform.up, .45f, m_groundMask);
            OnGroundedChanged.Invoke(m_isGrounded);
        }

        private void CheckForWall()
        {
            var size = new Vector2(0.1f, .5f);
            var rightWall = Physics2D.BoxCast(transform.position, size, 0f, transform.right, .5f, m_groundMask);
            var leftWall = Physics2D.BoxCast(transform.position, size, 0f, -transform.right, .5f, m_groundMask);
            m_isWallInFront = rightWall;
            m_isWalled = leftWall || rightWall;
        }

        private void CheckForSafePoint()
        {
            var left = Physics2D.Raycast(transform.position - transform.right * 0.45f, -transform.up, .45f, m_groundMask);
            var right = Physics2D.Raycast(transform.position + transform.right * 0.45f, -transform.up, .45f, m_groundMask);
            if (left && right)
                m_latestSafePoint = transform.position;
        }
        
        private void Respawn()
        {
            enabled = true;
            OnRespawn.Invoke();
        }
        
        public void Die()
        {
            if (enabled == false)
                return;
            
            enabled = false;
            if (m_coinPocket.Coins > 0)
            {
                m_coinPocket.DropCoins();
                transform.DOJump(m_latestSafePoint, .5f, 1, .5f).OnComplete(Respawn);
            }
            else
            {
                m_gameUI.ShowDead();
            }
            
            OnDie.Invoke();
        }
    }
}
