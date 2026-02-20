using System.ComponentModel;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
[CreateAssetMenu(fileName = "Homing Modifier", menuName = "Modifiers/Homing Modifier")]
public class HomingModifier : ModifierDefinition
{
    public float acceleration = 1f;
    public float proxMult = 1f;
    public float range = 1f;
    public override void OnCast(SpellInstance instance)
    {
    }

    public override void OnDespawn(SpellInstance instance)
    {
    }
    public override void OnHitLiving(SpellInstance instance, HitContext context)
    {
    }

    public override void OnHitTerrain(SpellInstance instance, HitContext context)
    {
    }

    public override void OnSpawn(SpellInstance instance)
    {
    }

    public override void OnUpdate(SpellInstance instance)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = enemies[0];

        float minDist = float.MaxValue;
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(instance.transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestEnemy = enemy;
            }
        }
        if(minDist > range) return;

        float proximityMult = proxMult/minDist;

        Vector2 vel = instance.rigidBody.linearVelocity;
        Vector2 targetVel = (nearestEnemy.transform.position - instance.transform.position).normalized * instance.definition.speed * acceleration;

        instance.rigidBody.linearVelocity = Vector2.MoveTowards(vel, targetVel, proximityMult * Time.deltaTime);
    }
}
