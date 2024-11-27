using DG.Tweening;
using FSM;
using UnityEngine;

public class BirdFlyState : StateBase
{
    private Brid _brid;
    
    public BirdFlyState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        _brid.anim.SetBool("Fly", true);
        int random = Random.Range(0, GameManager.Instance.flyPositions.Length);
        _brid.flyIndex = random;
        var target = GameManager.Instance.flyPositions[random].position - Vector3.down * 0.15f;
        _brid.sr.flipX = target.x > _brid.transform.position.x;
        float distance = Vector3.Distance(_brid.transform.position, target);
        float flyTime = distance / _brid.flySpeed;
        if (_brid.flyIndex >= 10)
        {
            _brid.transform.DOScale(0.06f, flyTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                _brid.sr.sortingLayerName = "middle";
                _brid.sr.sortingOrder = 0;
            });
        }
        _brid.transform.DOMove(target, flyTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            currMachine.ChangeState<BirdFlyWaitState>();
        });
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
