using UnityEngine;

public class Damageable : MonoBehaviour
{
    /// <summary>
    /// Checks for damage caused by the trigger crossing and applies as necessary.
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
        => this.ProcessDamage(collision.gameObject);

    /// <summary>
    /// Checks for damage caused by the collision and applies as necessary.
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
        => this.ProcessDamage(collision.gameObject);

    /// <summary>
    /// Checks if the other object deals damage and applies as necessary.
    /// </summary>
    /// <param name="other"></param>
    protected virtual void ProcessDamage(GameObject other)
    {
        if (this.ObjectDealsDamage(other, out IDamager damager))
        {
            this.ApplyDamage(damager);
        }
    }

    /// <summary>
    /// Determines if the provided object should deal damage to us.
    /// By default, this is true if the object is an <see cref="IDamager"/>.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="damager"></param>
    /// <returns></returns>
    protected virtual bool ObjectDealsDamage(GameObject other, out IDamager damager)
        => other.TryGetComponent(out damager);

    /// <summary>
    /// Applies any damage caused. By default, destroys self.
    /// </summary>
    /// <param name="damager"></param>
    protected virtual void ApplyDamage(IDamager damager) => Destroy(this.gameObject);
}