using UnityEngine;

public class DamageablePlayer : Damageable
{
    [SerializeField]
    private float deathJumpHeight = 10;

    private Animator animator;

    private Rigidbody2D playerRigidbody;

    PlayerMovement playerMovement;

    private bool isAlive = true;

    private void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    protected override bool ObjectDealsDamage(GameObject other, out IDamager damager)
        => base.ObjectDealsDamage(other, out damager) && damager.DamagesPlayer;

    protected override void ApplyDamage(IDamager damager)
    {
        if (this.isAlive)
        {
            this.isAlive = false;
            this.playerMovement.enabled = false;
            this.animator.SetTrigger("Death");
            this.playerRigidbody.velocity = new Vector2(0, this.deathJumpHeight);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}