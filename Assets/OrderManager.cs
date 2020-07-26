using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


enum Recipe {
    Latte,
    Choc,
    Mocha,
}



public class OrderManager : MonoBehaviour
{
    [SerializeField]
    GameObject orderPrefab;
    List<Order> orders;

    UIController uIController;

    [SerializeField]
    Vector3 startPos;

    // Start is called before the first frame updatesas
    void Start()
    {
        StartCoroutine(AddOrder());
        uIController = GetComponent<UIController>();
        orders = new List<Order>();
    }


    IEnumerator AddOrder()
    {
        yield return new WaitForSeconds(0.5f);
        CreateOrder();
        StartCoroutine(AddOrder());
    }


    public void RemoveOrder(Order order)
    {
        StartCoroutine(Remove(order));
    }

    IEnumerator Remove(Order order)
    {
        var gameObj = order.gameObject;
        orders.Remove(order);

        Destroy(gameObj);

        yield return new WaitForSeconds(1f);
        ReorderOrders();
    }



    List<Ingredients> GetIngredients(Recipe recipe)
    {
        bool iced = (Random.value > 0.5f);
        var list = new List<Ingredients>();
        if (iced)
        {
            list.Add(Ingredients.Ice);
        }
        
        switch (recipe)
        {
            case Recipe.Latte:
                list.Add(Ingredients.Coffee);
                return list;
            case Recipe.Mocha:
                list.Add(Ingredients.Coffee);
                list.Add(Ingredients.Choc);
                return list;
            case Recipe.Choc:
                list.Add(Ingredients.Choc);
                return list;
            default:
                throw new System.Exception("WHOOPS");
        }
    }

    void ReorderOrders()
    {
        for (int i = 0; i < orders.Count; i++)
        {
            var rectTransform = orders[i].GetComponent<RectTransform>();
            rectTransform.position = new Vector3(startPos.x + (i * (rectTransform.rect.width + 30)), startPos.y);
        }
    }

    void CreateOrder()
    {
       var randomRecipe = (Recipe)Random.Range(0, (int)Recipe.GetValues(typeof(Recipe)).Cast<Recipe>().Max());
       var obj = Instantiate(orderPrefab, this.gameObject.transform);
       var order = obj.GetComponent<Order>();
        order.SetIngredients(GetIngredients(randomRecipe));
        if (orders.Count > 0)
        {
            var position = orders.Last().GetComponent<RectTransform>().position;
            var rectTransform = order.GetComponent<RectTransform>();
            rectTransform.position = new Vector3(position.x + rectTransform.rect.width + 30, position.y);
        } else
        {
            startPos = obj.GetComponent<RectTransform>().position;
        }
       orders.Add(order);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
