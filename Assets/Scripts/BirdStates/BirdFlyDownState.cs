using DG.Tweening;
using FSM;
using UnityEngine;

public class BirdFlyDownState : StateBase
{
    private Brid _brid;
    
    public BirdFlyDownState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        _brid.anim.SetBool("Fly", true);
        _brid.anim.Play("FlyStart");
        float x = Random.Range(-8, 8);
        float y = Random.Range(-5f, -3.5f);
        var target = new Vector3(x, y, 0);
        if (_brid.nestTrans != null)
        {
            GameManager.Instance.flyPositions.Add(_brid.nestTrans);
            _brid.nestTrans = null;
        }

        _brid.sr.flipX = target.x > _brid.transform.position.x;
        float distance = Vector3.Distance(target, _brid.transform.position);
        float time = distance / _brid.flySpeed;
        DOTween.Sequence().AppendCallback(() =>
        {
            _brid.transform.DOScale(0.12f, time).SetEase(Ease.Linear);
            _brid.sr.sortingLayerName = "bird";
            _brid.sr.sortingOrder = 0;
            

            var anim = DOTween.Sequence();
            anim.Append(_brid.transform.DOMove(target, time).SetEase(Ease.Linear));
            anim.AppendCallback(() =>
            {
                _brid.anim.SetBool("Fly", false);
            });
            anim.AppendInterval(2f);
            anim.OnComplete(() =>
            {
                currMachine.ChangeState<BirdIdleState>();
            });
        }).SetDelay(0.2f);
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
