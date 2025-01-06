using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private float jumpHeight = 5;

    [SerializeField]
    private float climbSpeed = 5;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform projectileSpawn;

    #endregion

    #region Components

    private Rigidbody2D playerRigidbody;

    private Vector2 moveInput;

    private Animator animator;

    private CircleCollider2D feetCollider;

    #endregion

    #region Properties

    private float defaultGravityScale;

    private bool isOnLadder = false;

    #endregion

    #region Fields

    private bool isRunning => Mathf.Abs(this.playerRigidbody.velocity.x) > float.Epsilon;

    private bool isAttemptingToClimb => Mathf.Abs(this.moveInput.y) > float.Epsilon;

    private bool isClimbing => this.isOnLadder && this.isAttemptingToClimb;

    private bool isTouchingGround => this.feetCollider.IsTouchingLayers(LayerMask.GetMask("Terrain"));

    private bool isTouchingLadder => this.feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

    #endregion

    #region Overrides

    private void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.feetCollider = this.GetComponent<CircleCollider2D>();
        this.defaultGravityScale = this.playerRigidbody.gravityScale;
        this.moveInput = new Vector2(0, 0);
        this.playerRigidbody.velocity = new Vector2(0, 0);
    }

    private void Update()
    {
        this.Run();
        this.FlipSprite();
        this.Climb();
    }

    private void OnMove(InputValue value) => this.moveInput = value.Get<Vector2>();

    private void OnJump(InputValue value)
    {
        if (value.isPressed && this.isTouchingGround)
        {
            this.playerRigidbody.velocity += new Vector2(0, this.jumpHeight);
        }
    }

    private void OnFire(InputValue value)
    {
        GameObject projectile = Instantiate(this.projectile, this.projectileSpawn.position, this.transform.rotation);
        projectile.transform.localScale *= new Vector2(this.transform.localScale.x, 1);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.velocity *= new Vector2(Mathf.Sign(this.transform.localScale.x), 1);
        projectileRigidbody.velocity += new Vector2(this.playerRigidbody.velocity.x, 0);
    }

    #endregion

    #region Helpers

    private void Run()
    {
        // Update the velocity.
        this.playerRigidbody.velocity = new Vector2(this.moveInput.x * this.moveSpeed, this.playerRigidbody.velocity.y);

        // Verify that the running state for the animation matches whether we're running.
        bool inRunningState = this.animator.GetBool("isRunning");
        if (this.isRunning != inRunningState)
        {
            this.animator.SetBool("isRunning", this.isRunning);
        }
    }

    private void FlipSprite()
    {
        if (!this.isRunning)
        {
            // Not moving, do nothing.
            return;
        }

        // Determine which direction we're moving and which direction we're facing.
        bool movingRight = this.playerRigidbody.velocity.x > 0;
        bool facingRight = this.transform.localScale.x > 0;

        // If they don't match, flip the player.
        if (facingRight != movingRight)
        {
            this.transform.localScale = new Vector2(Mathf.Sign(this.playerRigidbody.velocity.x), this.transform.localScale.y);
        }

    }

    private void Climb()
    {
        // Track local climbing state.
        if (this.isOnLadder && !this.isTouchingLadder)
        {
            // No longer touching a ladder, stop climbing.
            this.isOnLadder = false;
            this.playerRigidbody.gravityScale = this.defaultGravityScale;
        }
        else if (!this.isOnLadder && this.isTouchingLadder && this.isAttemptingToClimb)
        {
            // Start climbing.
            this.isOnLadder = true;
            this.playerRigidbody.gravityScale = 0;
        }

        // Manage movement.
        if (this.isOnLadder)
        {
            // Set the vertical velocity.
            this.playerRigidbody.velocity = new Vector2(this.playerRigidbody.velocity.x, this.moveInput.y * this.climbSpeed);
        }

        // Toggle our animator's climbing state, if necessary.
        bool inClimbingState = this.animator.GetBool("isClimbing");
        if (inClimbingState != this.isClimbing)
        {
            this.animator.SetBool("isClimbing", this.isClimbing);
        }
    }

    #endregion
}
