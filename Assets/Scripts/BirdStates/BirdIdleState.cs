using System.Collections;
using FSM;
using UnityEngine;

public class BirdIdleState : StateBase
{
    private Brid _brid;
    private Coroutine coroutine;
    
    public BirdIdleState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        float time = Random.Range(1f, 2f);
        
        coroutine = _brid.StartCoroutine(WaitForNext(time));
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
                }
            }
        }
    }

    public override void OnExit()
    {
        if(coroutine == null)
            return;
        _brid.StopCoroutine(coroutine);
    }

    private void DONext()
    {
        if (_brid.isSmall)
        {
            currMachine.ChangeState<BirdRunState>();
            return;
        }

        int random = Random.Range(0, 2);
        if (random == 0)
        {
            currMachine.ChangeState<BirdRunState>();
        }
        else
        {
            currMachine.ChangeState<BirdFlyState>();
        }
    }

    private IEnumerator WaitForNext(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DONext();
    }
}
