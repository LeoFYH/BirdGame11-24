using DG.Tweening;
using FSM;
using UnityEngine;

public class BirdFlyInAirState : StateBase
{
    private Brid _brid;

    public BirdFlyInAirState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        _brid.anim.Play("FlyInAir");
        _brid.anim.SetBool("Fly", false);
        Fly();
    }

    private void Fly()
    {
        Vector3 target = Vector3.zero;
        if (_brid.sr.flipX)
        {
            _brid.sr.flipX = false;
            target = new Vector3(8.2f, _brid.transform.position.y, 0);
        }
        else
        {
            _brid.sr.flipX = true;
            target = new Vector3(-8.2f, _brid.transform.position.y, 0);
        }

        float distance = Vector3.Distance(_brid.transform.position, target);
        float flyTime = distance / _brid.flySpeed;
        _brid.transform.DOMove(target, flyTime).SetEase(Ease.Linear).OnComplete(DoNext);
    }

    private void DoNext()
    {
        int random = Random.Range(0, 10);
        if (random < 4)
        {
            currMachine.ChangeState<BirdFlyDownState>();
        }
        else
        {
            Fly();
        }
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
