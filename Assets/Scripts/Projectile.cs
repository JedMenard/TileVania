using UnityEngine;

public class Projectile : MonoBehaviour, IDamager
{
    [SerializeField]
    private float projectileSpeed;
    private Rigidbody2D projectileRigidBody;

    public bool DamagesPlayer => false;

    private void Awake()
    {
        this.projectileRigidBody = this.GetComponent<Rigidbody2D>();
        this.projectileRigidBody.velocity = new Vector2(this.projectileSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
        => Destroy(this.gameObject);

    private void OnTriggerEnter2D(Collider2D collision)
        => Destroy(this.gameObject);
}
