using System;
using UnityEngine;

namespace Main
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        private void Start()
        {
            m_animator.Play("window", 0, (transform.position.x - .5f) % 3f * 0.33f);
        }
    }
}
