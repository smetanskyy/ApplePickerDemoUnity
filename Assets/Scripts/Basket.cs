using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour
{
    public float panSpeed = 20f;
    Vector3 lastMouseCoordinate = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadScene(1);
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
        }
    }
}
