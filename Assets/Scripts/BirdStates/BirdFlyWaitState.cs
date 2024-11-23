using DG.Tweening;
using FSM;
using UnityEngine;

public class BirdFlyWaitState : StateBase
{
    private Brid _brid;
    
    public BirdFlyWaitState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        _brid.anim.SetBool("Fly", false);
        float waitTime = Random.Range(3f, 8f);
        DOTween.Sequence().AppendCallback(() =>
        {
            currMachine.ChangeState<BirdFlyDownState>();
        }).SetDelay(waitTime);
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
