using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.UI;
using System;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;
        public bool hasExtraLife = false;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public bool isInvulnerable = false;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        //Mobile Controls
        Vector2 startPos;
        Vector2 direction;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = GetTouchHorizontalAxis();
                if (jumpState == JumpState.Grounded && GetTouchJump())
                    jumpState = JumpState.PrepareToJump;
                else if (GetTouchJump())
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            GetTouchHorizontalAxis();
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        float GetTouchHorizontalAxis()
        {
            #if UNITY_EDITOR
                return Input.GetAxis("Horizontal");
            #endif
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.position.x < Screen.height / 2)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            startPos = touch.position;
                            break;

                        case TouchPhase.Moved:
                            direction = touch.position - startPos;
                            break;

                        case TouchPhase.Ended:
                            direction = Vector2.zero;
                            break;
                    }
                }
                else
                {
                    direction = Vector2.zero;
                }
            }
            return direction.normalized.x;
        }

        bool GetTouchJump()
        {

            #if UNITY_EDITOR
                return Input.GetButtonDown("Jump");
            #endif
            foreach (var touch in Input.touches)
            {
                if(touch.position.x > Screen.height / 2 && touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }
            return false;
        }

        internal void IncreaseJump(float jumpTakeOffSpeed)
        {
            this.jumpTakeOffSpeed = jumpTakeOffSpeed;
        }

        internal void IncreaseSpeed(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
        }

        internal void GiveExtraLife()
        {
            hasExtraLife = true;
        }
    }
}