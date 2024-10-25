using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected Enemy enemy;
    protected EnemyStateMachine enemystateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemystatemachine)
    {
        this.enemy = enemy;
        this.enemystateMachine = enemystatemachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
