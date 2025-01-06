using UnityEngine;

public class RezoneMovement : DamageableEnemy, IDamager
{
    [SerializeField]
    private float moveSpeed = 1;
    private Rigidbody2D rezoneRigidBody;
    private BoxCollider2D reverseParascope;

    public bool DamagesEnemy => false ;

    private void Awake()
    {
        this.rezoneRigidBody = this.GetComponent<Rigidbody2D>();
        this.reverseParascope = this.GetComponent<BoxCollider2D>();
        this.rezoneRigidBody.velocity += new Vector2(this.moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!this.reverseParascope.IsTouchingLayers(LayerMask.GetMask("Terrain")))
        {
            this.Flip();
        }
    }

    private void Flip()
    {
        this.moveSpeed *= -1;
        this.transform.localScale *= new Vector2(-1, 1);
        this.rezoneRigidBody.velocity = new Vector2(this.moveSpeed, 0);
    }
}
