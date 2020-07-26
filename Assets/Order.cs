using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Ingredients {
    Coffee,
    Milk,
    FrothedMilk,
    Choc,
    Ice,
    Water
}

public class Order : MonoBehaviour
{
    TMP_Text timeLeft;
    List<Ingredients> ingredients;
    float timeWillWait = 60f;
    float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = transform.GetComponentInChildren<TMP_Text>();

        timeLeft.text = "Time Left: " + (timeWillWait - currentTime) + "s";
    }

    public void SetIngredients(List<Ingredients> ingredients)
    {
        this.ingredients = ingredients;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timeLeft.text = "Time Left: " + Mathf.Round((timeWillWait - currentTime)) + "s";

        if (currentTime >= timeWillWait)
        {
            FindObjectOfType<OrderManager>().RemoveOrder(this);
        }
    }
}
