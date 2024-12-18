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
        int random = Random.Range(0, GameManager.Instance.flyPositions.Count);
        _brid.flyIndex = random;
        _brid.nestTrans = GameManager.Instance.flyPositions[random];
        GameManager.Instance.flyPositions.RemoveAt(random);
        var target = _brid.nestTrans.position - Vector3.up * 0.15f;
        _brid.sr.flipX = target.x > _brid.transform.position.x;
        float distance = Vector3.Distance(_brid.transform.position, target);
        float flyTime = distance / _brid.flySpeed;
        if (_brid.flyIndex == 10)
        {
            _brid.transform.DOScale(_brid.farAwayScale, flyTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                _brid.sr.sortingLayerName = "middle";
                _brid.sr.sortingOrder = 5;
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
