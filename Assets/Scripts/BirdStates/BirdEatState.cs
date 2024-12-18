using FSM;
using UnityEngine;

public class BirdEatState : StateBase
{
    private Brid _brid;
    private float eatFoodTimer;

    public BirdEatState(StateMachine machine) : base(machine)
    {
        _brid = machine.currObj.GetComponent<Brid>();
    }

    public override void OnEnter()
    {
        _brid.currFood.isTargeted = true;
    }

    public override void OnUpdate()
    {
        if (!_brid.isSmall || _brid.currFood == null)
        {
            DONext();
            return;
        }

        Vector3 target = _brid.currFood.transform.position;
        if (target.x < _brid.transform.position.x)
        {
            target += _brid.isSmall ? new Vector3(0.2f, 0.2f, 0) : new Vector3(0.4f, 0.4f, 0);
        }
        else
        {
            target += _brid.isSmall ? new Vector3(-0.1f, 0.2f, 0) : new Vector3(-0.4f, 0.4f, 0);
        }

        if (Vector3.Distance(_brid.transform.position, target) > 0.1f)
        {
            _brid.sr.flipX = target.x > _brid.transform.position.x;
            _brid.transform.position =
                Vector3.MoveTowards(_brid.transform.position, target, _brid.moveSpeed * Time.deltaTime);
            _brid.anim.SetFloat("MoveSpeed", 1f);
        }
        else
        {
            _brid.anim.SetFloat("MoveSpeed", 0);
            _brid.anim.SetBool("Eat", true);
            if (_brid.eatFoodCount < _brid.eatCountForMid)
            {
                if (eatFoodTimer < _brid.eatFoodTime)
                {
                    eatFoodTimer += Time.deltaTime;
                }
                else
                {
                    eatFoodTimer = 0;
                    _brid.eatFoodCount++;
                    if (_brid.eatFoodCount == _brid.eatCountForMid)
                    {
                        _brid.transform.localScale = Vector3.one * _brid.middleScale;
                    }

                    // timer = 0;
                    // isIdle = true;
                    _brid.scaleX = _brid.transform.localScale.x;
                    GameManager.Instance.ReduceFood(_brid.currFood);
                    _brid.currFood = null;
                    _brid.anim.SetBool("Eat", false);
                }
            }
            else
            {
                if (_brid.eatFoodCount < _brid.eatCountForMid + _brid.eatCountForBig &&
                    _brid.eatFoodCount >= _brid.eatCountForMid)
                {
                    if (eatFoodTimer < _brid.eatFoodTime)
                    {
                        eatFoodTimer += Time.deltaTime;
                    }
                    else
                    {
                        eatFoodTimer = 0;
                        _brid.eatFoodCount++;
                        if (_brid.eatFoodCount == _brid.eatCountForMid + _brid.eatCountForBig)
                        {
                            _brid.transform.localScale = Vector3.one * _brid.largeScale;
                            int index = Random.Range(0, GameManager.Instance.nests.Count);
                            _brid.nest = GameManager.Instance.nests[index];
                            _brid.nest.Init(_brid);
                            _brid.nestPos = _brid.nest.transform.position;
                            //GameManager.Instance.nests.Remove(_brid.nest);
                            _brid.isSmall = false;
                            _brid.distance = Vector3.Distance(_brid.transform.position, _brid.nestPos);
                        }

                        // timer = 0;
                        // isIdle = true;
                        _brid.scaleX = _brid.transform.localScale.x;
                        GameManager.Instance.ReduceFood(_brid.currFood);
                        _brid.currFood = null;
                        _brid.anim.SetBool("Eat", false);
                    }
                }
            }
        }
    }

    public override void OnExit()
    {
        _brid.anim.SetFloat("MoveSpeed", 0);
        _brid.anim.SetBool("Eat", false);
    }

    private void DONext()
    {
        if (_brid.isSmall)
        {
            Food food;
            if (GameManager.Instance.TryGetUntargetedFood(out food))
            {
                _brid.currFood = food;
                currMachine.ChangeState<BirdEatState>();
                return;
            }

            int random = Random.Range(0, 2);
            if (random == 0)
            {
                currMachine.ChangeState<BirdIdleState>();
            }
            else if (random == 1)
            {
                currMachine.ChangeState<BirdRunState>();
            }
        }
        else
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                currMachine.ChangeState<BirdIdleState>();
            }
            else if (random == 1)
            {
                currMachine.ChangeState<BirdRunState>();
            }
            else if (random == 2)
            {
                currMachine.ChangeState<BirdFlyState>();
            }
        }
    }
}
