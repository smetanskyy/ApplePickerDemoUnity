using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    public Text scoreGT;
    public bool upLevel = false;

    //Шаблон для створення яблука
    public GameObject applePrefab;

    //Шаблон для створення bomb
    public GameObject bombPrefab;
    
    //Швидкість руху яблуні
    public float speed = 10f;

    //Відстань на яку повинно змінюватися напрямок руху яблуні
    public float leftAndRightEnge = 25f;

    //Імовірність случайної зміни напряку руху
    public float changeToChangeDirections = 0.05f;

    //Частота створення екземплярів яблук
    public float secondsBetweenAppleDrops = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Получить компонент Text этого игрового объекта
        scoreGT = scoreGO.GetComponent<Text>();
        //Скидання яблука раз в секунду
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position; //позиція яблука рівна позиції яблуні
        Invoke("DropApple", secondsBetweenAppleDrops);  //кожну секунду буде скидатися нове яблуко
    }

    void DropBomb()
    {
        GameObject bomb = Instantiate<GameObject>(bombPrefab);
        bomb.transform.position = transform.position; //позиція яблука рівна позиції яблуні
        Invoke("DropBomb", secondsBetweenAppleDrops);  //кожну секунду буде скидатися нове яблуко
    }

    // Update is called once per frame
    void Update()
    {
        // Преобразовать текст в scoreGT в целое число
        int score = int.Parse(scoreGT.text);
        if (score > 2000 && !upLevel)
        {
            CancelInvoke("DropApple");
            Invoke("DropBomb", 2f);
            upLevel = true;
        }
        //Просте переміщення
        Vector3 pos = transform.position;
        var deltaTime = Time.deltaTime;
        pos.x += speed * deltaTime;
        transform.position = pos;

        //Зміна напрямку
        if (pos.x < -leftAndRightEnge)
        {
            speed = Mathf.Abs(speed); //починаємо рухатися в право
        }
        else if (pos.x > leftAndRightEnge)
        {
            speed = -Mathf.Abs(speed); //починаємо рухатися в ліво
        }
    }
    private void FixedUpdate() //визываэться только 50 раз в секунду
    {
        //Тепер випадкова зміна напрямку привязана до часу,
        //тому, що виконується в методі FixedUpdate()
        if (Random.value < changeToChangeDirections)  //починаємо рухатися рандомом, якщо рандом попав
        {
            speed *= -1;
        }
    }
}
