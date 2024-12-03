using FSM;
using UnityEngine;

public class BirdRunState : StateBase
{
    private Brid _brid;
    private Vector3 target;
    
    public BirdRunState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        float x = Random.Range(-_brid.radiusX, _brid.radiusX);
        float y = Random.Range(-_brid.radiusY, _brid.radiusY);
        target = _brid.originalPos + new Vector3(x, y);
    }

    public override void OnUpdate()
    {
        if (_brid.isSmall)
        {
            if (_brid.currFood == null)
            {
                Food food;
                if (GameManager.Instance.TryGetUntargetedFood(out food))
                {
                    _brid.currFood = food;
                    currMachine.ChangeState<BirdEatState>();
                    return;
                }
            }
        }
        
        _brid.sr.flipX = target.x > _brid.transform.position.x;
        _brid.transform.position = Vector3.MoveTowards(_brid.transform.position, target, _brid.moveSpeed * Time.deltaTime);
        _brid.anim.SetFloat("MoveSpeed", 1);
        if (Vector3.Distance(_brid.transform.position, target) <= 0.1f)
        {
            DONext();
        }
    }

    public override void OnExit()
    {
        _brid.anim.SetFloat("MoveSpeed", 0f);
    }

    private void DONext()
    {
        if (_brid.isSmall)
        {
            currMachine.ChangeState<BirdIdleState>();
            return;
        }

        int random = Random.Range(0, 2);
        if (random == 0)
        {
            currMachine.ChangeState<BirdIdleState>();
        }
        else if (random == 1)
        {
            if (GameManager.Instance.flyPositions.Count == 0)
            {
                currMachine.ChangeState<BirdFlyAirState>();
                return;
            }

            int index = Random.Range(0, 2);
            if (index == 0)
            {
                currMachine.ChangeState<BirdFlyState>();
            }
            else
            {
                currMachine.ChangeState<BirdFlyAirState>();
            }
        }
    }
}
