using UnityEngine;

public class DamageableEnemy : Damageable
{
    protected override bool ObjectDealsDamage(GameObject other, out IDamager damager)
        => base.ObjectDealsDamage(other, out damager) && damager.DamagesEnemy;
}
