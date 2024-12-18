using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GraphicRaycaster _GraphicRaycaster;
    public BindableProperty<int> coin =new BindableProperty<int>(100);
    public int eggPackage = 100;
    public GameObject eggPre;
    public GameObject foodPre;
    public Transform[] birdPositions;
    public List<Food> foods;
    public List<Nest> nests;
    public List<Transform> flyPositions;
    public int noOpenEggs;
    public float createFoodTime = 0.5f;
    float foodTimer;
    public GameObject numPre;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateNum(string s, Vector3 pos)
    {
        GameObject go = Instantiate(numPre, UIManager.Instance.transform);
        go.transform.position = pos;
        go.GetComponent<NumPanel>().Init(s);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            CreateFood();
        }
        else if (Input.GetMouseButton(0))
        {
            if (foodTimer < createFoodTime)
            {
                foodTimer += Time.deltaTime;
            }
            else
            {
                foodTimer = 0;
                CreateFood();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            foodTimer = 0;
        }
    }

    private bool IsPointerBird()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("Bird"))
            {
                return true;
            }
        }

        return false;
    }

    void CreateFood()
    {

        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider!=null && hit.collider.tag == "Terrain")
        {
            Food food = Instantiate(foodPre).GetComponent<Food>();
            food.isTargeted = false;
            food.transform.position = hit.point;
            foods.Add(food);
        }

    }

    public void CreateBrid()
    {
        noOpenEggs = 3;
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(eggPre);
            go.transform.position = new Vector3(i, go.transform.position.y);
        }
    }

    public void CreateBirds(int count, GameObject birdPrefab)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = GameObject.Instantiate(birdPrefab);
            obj.transform.position = birdPositions[i].position;
        }
    }

    public void ReduceFood()
    {
        foods[0].hp--;
        if (foods[0].hp <= 0)
        {
            //CreateNum("+1");
            Destroy(foods[0].gameObject);
            foods.Remove(foods[0]);
        }
    }

    public void ReduceFood(Food food)
    {
        food.hp--;
        if (food.hp <= 0)
        {
            CreateNum("+1", food.transform.position);
            foods.Remove(food);
            Destroy(food.gameObject);
        }
    }

    public void RecycleFood(Food food)
    {
        foods.Remove(food);
        Destroy(food.gameObject);
    }

    public bool TryGetUntargetedFood(out Food food)
    {
        foreach (var temp in foods)
        {
            if (!temp.isTargeted)
            {
                food = temp;
                return true;
            }
        }

        food = null;
        return false;
    }
}
