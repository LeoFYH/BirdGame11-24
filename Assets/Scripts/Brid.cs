using FSM;
using UnityEngine;

public class Brid : MonoBehaviour
{
    public Transform nestTrans;
    public Vector3 originalPos;
    public Vector3 nestPos;
    public float radiusX = 2f;
    public float radiusY = 0.8f;
    public float flyRadius = 10;
    public float moveSpeed = 1;
    public float flySpeed = 3;
    public float waitTime = 3;
    public bool isSmall = true;
    public Animator anim;
    public SpriteRenderer sr;
    public int flyIndex = -1;
    float timer;
    bool isIdle=true;
    float x;
    float y;
    float flyX;
    float flyY;
    int index;
    public Nest nest;
    public bool isFlying;
    public float inNestTime = 5;
    float nestTime;
    public bool isInNest;
    float eatTimer;
    public GameObject heartPre;
    public int smallPrice = 20;
    public int bigPrice = 30;
    public int level = 1;
    public string title="幼鸟";
    public string desc="这是一只幼鸟";
    public Transform heartPos;
    bool isFirst=true;
    [Header("点击的次数后鸟跟随鼠标移动")]
    public int clickCount = 5;
    bool isFollow;
    float followTime = 10;
    float followTimer;
    [Header("吃几次食物变中鸟")]
    public int eatCountForMid = 3;
    [Header("吃几次食物变成鸟")]
    public int eatCountForBig = 2;
    [Header("雏鸟日收益")]
    public int incomeForMid;
    [Header("成鸟日收益")]
    public int incomeForBig;
    public float scaleX;
    public float distance;
    public float eatFoodCount;
    public float eatFoodTime = 1;
    float eatFoodTimer;
    bool isEnter;

    public Food currFood;

    private StateMachine _stateMachine;

    private float startTimer = 0;

    void Start()
    {
        scaleX = transform.localScale.x;
        originalPos = transform.position;
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        _stateMachine = new StateMachine(gameObject);
        _stateMachine.AddState(new BirdIdleState(_stateMachine));
        _stateMachine.AddState(new BirdRunState(_stateMachine));
        _stateMachine.AddState(new BirdFlyState(_stateMachine));
        _stateMachine.AddState(new BirdEatState(_stateMachine));
        _stateMachine.AddState(new BirdFlyWaitState(_stateMachine));
        _stateMachine.AddState(new BirdFlyDownState(_stateMachine));
        _stateMachine.AddState(new BirdFlyAirState(_stateMachine));
        _stateMachine.AddState(new BirdFlyInAirState(_stateMachine));
        startTimer = Time.time;
    }

    private void OnMouseDown()
    {
        if (!isSmall)
        {
            level++;
        }
        
        GameObject go = Instantiate(heartPre);
        go.transform.SetParent(transform);
        go.transform.position = heartPos.position;
        go.transform.localScale = Vector3.one * 0.01f;
        //UIManager.Instance.ShowInfoPanel(gameObject, price, title, desc, level);
    }

    private void OnMouseEnter()
    {
        isEnter = true;
    }
    
    private void OnMouseExit()
    {
        isEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!isSmall)
                {
                    title = "Adult bird";
                    desc = "It's an adult bird";
                }

                UIManager.Instance.ShowInfoPanel(gameObject, isSmall ? smallPrice : bigPrice, title, desc, isSmall? incomeForMid : incomeForBig,
                    eatFoodCount * 1f / 20);
            }
        }
        
        _stateMachine.OnUpdate();

        if (Time.time - startTimer >= 60)
        {
            startTimer = Time.time;
            AddCoins();
        }

        // if (isSmall)
        // {
        //     if (GameManager.Instance.foods.Count>0)
        //     {
        //         Vector3 target = GameManager.Instance.foods[0].transform.position;
        //         if (Vector3.Distance(transform.position, target) > 0.1f)
        //         {
        //             sr.flipX = target.x > transform.position.x;
        //             transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        //             anim.Play("Run");
        //         }
        //         else
        //         {
        //             anim.Play("Idle");
        //             if (eatFoodCount < eatCountForMid)
        //             {
        //                 if (eatFoodTimer < eatFoodTime)
        //                 {
        //                     eatFoodTimer += Time.deltaTime;
        //                 }
        //                 else
        //                 {
        //                     eatFoodTimer = 0;
        //                     eatFoodCount++;
        //                     if (eatFoodCount == eatCountForMid)
        //                     {
        //                         transform.localScale = Vector3.one * 0.09f;
        //                     }
        //                     timer = 0;
        //                     isIdle = true;
        //                     scaleX = transform.localScale.x;
        //                     GameManager.Instance.ReduceFood();
        //                 }
        //             }
        //             else
        //             {
        //                 if (eatFoodCount < eatCountForMid+eatCountForBig && eatFoodCount >= eatCountForMid)
        //                 {
        //                     if (eatFoodTimer < eatFoodTime)
        //                     {
        //                         eatFoodTimer += Time.deltaTime;
        //                     }
        //                     else
        //                     {
        //                         eatFoodTimer = 0;
        //                         eatFoodCount++;
        //                         if (eatFoodCount == eatCountForMid + eatCountForBig)
        //                         {
        //                             transform.localScale = Vector3.one * 0.12f;
        //                             index = Random.Range(0, GameManager.Instance.nests.Count);
        //                             nest = GameManager.Instance.nests[index];
        //                             nest.Init(this);
        //                             nestPos = nest.transform.position;
        //                             GameManager.Instance.nests.Remove(nest);
        //                             isSmall = false;
        //                             distance = Vector3.Distance(transform.position, nestPos);
        //                         }
        //                         timer = 0;
        //                         isIdle = true;
        //                         scaleX = transform.localScale.x;
        //                         GameManager.Instance.ReduceFood();
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     else
        //     {
        //         if (isIdle)
        //         {
        //             anim.Play("Idle");
        //             timer += Time.deltaTime;
        //             if (timer >= waitTime)
        //             {
        //                 isIdle = false;
        //                 timer = 0;
        //                 x = Random.Range(-radiusX, radiusX);
        //                 y = Random.Range(-radiusY, radiusY);
        //             }
        //         }
        //         else
        //         {
        //             Vector3 target = originalPos + new Vector3(x, y);
        //             sr.flipX = target.x > transform.position.x;
        //             transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        //             anim.Play("Run");
        //             if (Vector3.Distance(transform.position, target) <= 0.1f)
        //             {
        //                 isIdle = true;
        //             }
        //         }
        //     }
        // }
        // else
        // {
        //     if (level >= clickCount && !isFollow)
        //     {
        //         if (followTimer < followTime)
        //         {
        //             followTimer += Time.deltaTime;
        //             Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //             sr.flipX = target.x > transform.position.x;
        //             transform.position = Vector3.MoveTowards(transform.position, target, flySpeed * Time.deltaTime);
        //             anim.Play("Fly");
        //         }
        //         else
        //         {
        //             followTimer = 0;
        //             isFollow = true;
        //         }
        //     }
        //     else
        //     {
        //         if (GameManager.Instance.foods.Count > 0)
        //         {
        //             isInNest = false;
        //             isFlying = false;
        //             Vector3 target = GameManager.Instance.foods[0].transform.position;
        //             distance = Vector3.Distance(target, nest.transform.position);
        //             if (Vector3.Distance(transform.position, target) > 0.1f)
        //             {
        //                 sr.flipX = target.x > transform.position.x;
        //                 transform.position = Vector3.MoveTowards(transform.position, target, flySpeed * Time.deltaTime);
        //                 anim.Play("Fly");
        //                 if (nest.isFarest)
        //                 {
        //                     Vector3 scale = transform.localScale;
        //                     scale.x = scaleX + 0.03f * (distance - Vector3.Distance(transform.position, target)) / distance;
        //                     scale.y = scaleX + 0.03f * (distance - Vector3.Distance(transform.position, target)) / distance;
        //                     transform.localScale = scale;
        //                 }
        //             }
        //             else
        //             {
        //                 anim.Play("Idle");
        //                 if (eatTimer < 1)
        //                 {
        //                     eatTimer += Time.deltaTime;
        //                 }
        //                 else
        //                 {
        //                     eatTimer = 0;
        //                     GameManager.Instance.ReduceFood();
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             if (isFlying)
        //             {
        //                 isInNest = false;
        //                 Vector3 target = nestPos + new Vector3(flyX, flyY);
        //                 if (Vector3.Distance(transform.position, target) > 0.3f)
        //                 {
        //                     sr.flipX = target.x > transform.position.x;
        //                     transform.position = Vector3.MoveTowards(transform.position, target, flySpeed * Time.deltaTime);
        //                     anim.Play("Fly");
        //                 }
        //                 else
        //                 {
        //                     isFlying = false;
        //                 }
        //             }
        //             else
        //             {
        //                 if (Vector3.Distance(transform.position, nest.transform.position) > 0.01f)
        //                 {
        //                     sr.flipX = nest.transform.position.x > transform.position.x;
        //                     transform.position = Vector3.MoveTowards(transform.position, nest.transform.position, flySpeed * Time.deltaTime);
        //                     anim.Play("Fly");
        //                     isInNest = false;
        //                     if (nest.isFarest)
        //                     {
        //                         Vector3 scale = transform.localScale;
        //                         scale.x = scaleX - 0.03f * (distance - Vector3.Distance(transform.position, nest.transform.position)) / distance;
        //                         scale.y = scaleX - 0.03f * (distance - Vector3.Distance(transform.position, nest.transform.position)) / distance;
        //                         transform.localScale = scale;
        //                     }
        //                 }
        //                 else
        //                 {
        //                     transform.localScale = nest.isFarest? Vector3.one * 0.09f : Vector3.one * 0.12f;
        //                     isInNest = true;
        //                     nest.gameObject.SetActive(true);
        //                     if (isFirst)
        //                     {
        //                         nest.egg.SetActive(true);
        //                         isFirst = false;
        //                     }
        //                     anim.Play("Idle");
        //                     if (nestTime < inNestTime)
        //                     {
        //                         nestTime += Time.deltaTime;
        //                     }
        //                     else
        //                     {
        //                         isFlying = true;
        //                         nestTime = 0;
        //                         flyX = Random.Range(flyRadius / 2, flyRadius);
        //                         flyY = Random.Range(flyRadius / 2, flyRadius);
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
    }

    private void AddCoins()
    {
        Debug.Log("增加金币");
        if (isSmall)
        {
            GameManager.Instance.coin += incomeForMid;
        }
        else
        {
            GameManager.Instance.coin += incomeForBig;
        }
        
        UIManager.Instance.RefreshCoin();
    }
}
