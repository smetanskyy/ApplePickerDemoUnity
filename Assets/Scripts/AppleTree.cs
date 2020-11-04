using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    //Шаблон для створення яблука
    public GameObject applePrefab;
    //Швидкість руху яблуні
    public float speed = 1f;

    //Відстань на яку повинно змінюватися напрямок руху яблуні
    public float leftAndRightEnge = 10f;

    //Імовірність случайної зміни напряку руху
    public float changeToChangeDirections = 0.1f;

    //Частота створення екземплярів яблук
    public float secondsBetweenAppleDrops = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Скидання яблука раз в секунду
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position; //позиція яблука рівна позиції яблуні
        Invoke("DropApple", secondsBetweenAppleDrops);  //кожну секунду буде скидатися нове яблуко
    }

    // Update is called once per frame
    void Update()
    {
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
