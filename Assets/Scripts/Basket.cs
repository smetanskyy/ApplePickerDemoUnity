using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;
    public float panSpeed = 20f;
    Vector3 lastMouseCoordinate = Vector3.zero;

    //Відстань на яку повинно змінюватися напрямок руху яблуні
    public float leftAndRightEnge = 25f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Получить компонент Text этого игрового объекта
        scoreGT = scoreGO.GetComponent<Text>();
        // Установить начальное число очков равным 0
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //Отримати поточні координати мишки на екрані з Input
        Vector3 mousePos2D = Input.mousePosition;

        //Переміщаємо корзину вздовж осі X в координату X вказівника миші
        Vector3 pos = this.transform.position;

        // First we find out how much it has moved, by comparing it with the stored coordinate.
        Vector3 mouseDelta = mousePos2D - lastMouseCoordinate;

        // Then we check if it has moved to the left.
        if (mousePos2D != lastMouseCoordinate) // Assuming a negative value is to the left.
        {
            //Координати Z камери визначаючий, як далеко в трохмірному просторі
            //знаходиться вказівник миші
            mousePos2D.z = -Camera.main.transform.position.z;
            //Перетворити точку на двухмірній площині єкрану в трохмірні коодинати ігри
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
            pos.x = mousePos3D.x;
        }

        // Then we store our mousePosition so that we can check it again next frame.
        lastMouseCoordinate = Input.mousePosition;

        if (Input.GetKey("left"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("right"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(0);
        }

        if (pos.x < -leftAndRightEnge || pos.x > leftAndRightEnge)
        {
            pos = this.transform.position;
        }

        this.transform.position = pos;
    }


    //Визивається всякий раз коли який інший обєкт стикається із корзиною
    private void OnCollisionEnter(Collision coll)
    {
        //Пошук яблука, яке попало в корзину
        GameObject collidedWidth = coll.gameObject;  //Обєкт який стикнувся із корзиною
        if (collidedWidth.tag == "Apple")
        {
            Destroy(collidedWidth); //Якщо облуко то видаємо його
            
            // Преобразовать текст в scoreGT в целое число
            int score = int.Parse(scoreGT.text);
            
            // Добавить очки за пойманное яблоко
            score += 100;
            // Преобразовать число очков обратно в строку и вывести ее на экран
            scoreGT.text = score.ToString();
        }
    }
}
