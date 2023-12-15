using Scenes.StoryMode.LevelScripts.Script;
using Scenes.StoryMode.Scripts.Script;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    //Bullet object class. Realisation of shot, impact, explosion and destruction of the bullet itself
    
    private Enemy _target;

    private int _killPoint = 1;

    public float speed = 70f;

    public GameObject impactEffect;

    public float explodeRadius;

    public void Seek(Enemy target, int killPoint)
    {
        _target = target;
        _killPoint = killPoint;
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var dir = _target.transform.position - transform.position;
        var distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distThisFrame, Space.World);

        transform.LookAt(_target.transform);
    }

    private void HitTarget()
    {
        var t = transform;
        Instantiate(impactEffect, t.position, t.rotation);

        if (explodeRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(_target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explodeRadius);

        foreach (var coll in colliders)
        {
            if (!coll.CompareTag("Enemy")) continue;
            Debug.Log($"Find enemy in {explodeRadius}");
            Damage(coll.GetComponent<Enemy>());
        }
    }

    private void Damage(Enemy target)
    {
        target.SetDamage(_killPoint);
    }
}