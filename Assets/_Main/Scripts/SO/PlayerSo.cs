using UnityEngine;

namespace Main.SO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Player", fileName = "Player SO")]
    public class PlayerSo : ScriptableObject
    {
        [SerializeField] private float m_moveSpeed = 6f;
        [SerializeField] private float m_airSpeed = 7f;
        [SerializeField] private float m_jumpHeight = 1f;
        [SerializeField] private float m_jumpDistance = 1.5f;
        [SerializeField] private float m_maxFallSpeed = 15f;
        [SerializeField] private float m_wallSlideSpeed = 4f;

        public float MoveSpeed => m_moveSpeed;
        public float AirSpeed => m_airSpeed;
        public float JumpHeight => m_jumpHeight;
        public float JumpDistance => m_jumpDistance;
        public float MaxFallSpeed => m_maxFallSpeed;
        public float WallSlideSpeed => m_wallSlideSpeed;
    }
}
