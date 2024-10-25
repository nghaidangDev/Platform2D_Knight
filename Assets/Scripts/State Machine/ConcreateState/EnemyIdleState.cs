using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    /*private Vector3 _targetPos;
    private Vector3 _direction;*/
    private float speed = 3f;
    private float distanceThreshold = 0.1f;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 targetPoint;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemystatemachine) : base(enemy, enemystatemachine)
    {
        pointA = GameObject.FindGameObjectWithTag("PointA").transform.position;
        pointB = GameObject.FindGameObjectWithTag("PointB").transform.position;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        /*_targetPos = GetRandomPointInCircle();*/
        targetPoint = pointA;
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector3.Distance(enemy.transform.position, targetPoint) < distanceThreshold)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }

        /*_direction = (_targetPos - enemy.transform.position).normalized;
        enemy.MoveEnemy(_direction * enemy.RandomMovementSpeed);

        if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

/*    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.RandomMovementRange;
    }*/
}
