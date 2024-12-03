using DG.Tweening;
using FSM;
using UnityEngine;

public class BirdFlyAirState : StateBase
{
    private Brid _brid;
    
    public BirdFlyAirState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        _brid.anim.SetBool("Fly", true);
        //_brid.anim.Play("FlyInAir");
        var target = GetTargetPos();
        _brid.sr.flipX = target.x > _brid.transform.position.x;
        float distance = Vector3.Distance(_brid.transform.position, target);
        float flyTime = distance / _brid.flySpeed;
        _brid.transform.DOScale(0.06f, flyTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            _brid.sr.sortingLayerName = "middle";
            _brid.sr.sortingOrder = 0;
        });
        _brid.transform.DOMove(target, flyTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            currMachine.ChangeState<BirdFlyInAirState>();
        });
    }

    private Vector3 GetTargetPos()
    {
        float x = Random.Range(-4.2f, 4.2f);
        float y = Random.Range(1.2f, 4f);
        var target = new Vector3(x, y, 0);
        if (Mathf.Abs(target.x - _brid.transform.position.x) < 1)
        {
            return GetTargetPos();
        }

        return target;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
