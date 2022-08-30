using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Unity.VisualScripting;

public class EnemyAIBehavior : MonoBehaviour, IDamagable
{
    [SerializeField] private float nextWaypointDistance;

    private Path path;
    private EnemyAIVariables variables;
    private Seeker seeker;

    private IDisposable disposable;

    private Transform target;
    private Rigidbody2D rb;

    private int currentWaypoint = 0;

    public int MaxHealth => variables.maxHp;

    public int CurrentHealth { get => variables.currentHp; set => variables.currentHp = value; }

    public void InitBehavior()
    {
        variables = GetComponent<EnemyAIVariables>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        
        variables.InitVariables();

        target = GlobalAI.Instance.Player.transform;
    }

    public void Flip()
    {
        if (rb.velocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb.velocity.x <= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnGeneratePathComplete);
        }
    }

    public void TurnOnPathUpdate()
    {
        disposable = Observable.Interval(TimeSpan.FromSeconds(0.5f)).Subscribe(_=>
        {
            UpdatePath();
        });
    }

    public void TurnOffPathUpdate()
    {
        disposable.Dispose();
    }

    public bool IsPathHavePath()
    {
        return path != null;
    }

    private void OnGeneratePathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void NextWaypoint()
    {
        currentWaypoint++;
    }

    public bool IsReachedWaypoint()
    {
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        return nextWaypointDistance > distance;
    }

    public bool IsReachedPath()
    {
        return currentWaypoint >= path.vectorPath.Count;
    }

    public void MoveToPlayer()
    {
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * variables.speed * Time.fixedDeltaTime;

        rb.AddForce(force);
    }

    public void DamagedAnimEnd()
    {
        if (IsDead())
        {
            CustomEvent.Trigger(gameObject, "On Death");
            return;
        }
        else
        {
            CustomEvent.Trigger(gameObject, "On Animation End");
        }
    }

    public void ApplyDamage(int damage)
    {
        if (CurrentHealth - damage <= 0)
        {
            CurrentHealth = 0;
            CustomEvent.Trigger(gameObject, "On Death");
        }
        else
        {
            CurrentHealth -= damage;
            CustomEvent.Trigger(gameObject, "On Damaged");
        }
        
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
