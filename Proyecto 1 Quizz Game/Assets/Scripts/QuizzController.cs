using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizzController : MonoBehaviour
{
    public int numberQuestion;
    public int count = 0;
    public int countQuestions = 0;
    public int points = 0;
    public bool stop = false;
    public List<int> numberRandom;
    public GameObject[] questions;
    public GameObject[] canvas;
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI puntosFinale;

    // Start is called before the first frame update
    void Start()
    {
        canvas[0].SetActive(true);
        canvas[1].SetActive(false);
        canvas[2].SetActive(false);

        puntos.text = points.ToString();

        while (!stop)
        {
            int temp = Random.Range(0, numberQuestion);
            if (!numberRandom.Contains(temp))
            {
                numberRandom.Add(temp);
                count++;
            }
            if (count == numberQuestion)
            {
                break;
            }

        }

        ActiveQuestion();
    }

    public void BotonInicio(Button boton)
    {
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
    }
    
    //Método que activa y desactiva las preguntas.
    public void ActiveQuestion()
    {
        if (countQuestions != 0)
        {
            questions[numberRandom[countQuestions - 1]].SetActive(false);
            countQuestions++;
            questions[numberRandom[countQuestions - 1]].SetActive(true);
        }
        else
        {
            questions[numberRandom[countQuestions]].SetActive(true);
            countQuestions++;
        }
        if (countQuestions == numberQuestion)
        {
            questions[numberRandom[countQuestions - 1]].SetActive(true);
        }
    }

    //Método que pinta de color verde cuando la respuesta es incorrecta
    public void ColoreaBoton(Button boton)
    {
        boton.image.color = Color.green;
    }

    //Método para activar la corrutina de la respuesta correcta.
    public void CorrectQuestion(Button boton)
    {
        StartCoroutine(CorrectQuestionCoroutine(boton));
    }

    //Método para activar la corrutina de las respuestas incorrectas.
    public void IncorrectQuestions(Button boton)
    {
        StartCoroutine(IncorrectQuestionsCoroutine(boton));
    }

    //Corrutina que le da función al botón de respuesta correcta.
    public IEnumerator CorrectQuestionCoroutine(Button boton)
    {
        if (countQuestions != numberQuestion)
        {
            boton.image.color = Color.green;
            points += 100;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(1);
            ActiveQuestion();
        }
        else
        {
            boton.image.color = Color.green;
            points += 100;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(1);
            canvas[1].SetActive(false);
            canvas[2].SetActive(true);
            puntosFinale.text = points.ToString();
        }
    }

    //Corrutina que le da función a los botones de respuesta incorrecta.
    public IEnumerator IncorrectQuestionsCoroutine(Button boton)
    {
        if (countQuestions != numberQuestion)
        {
            boton.image.color = Color.red;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(1);
            ActiveQuestion();
        }
        else
        {
            boton.image.color = Color.red;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(1);
            canvas[1].SetActive(false);
            canvas[2].SetActive(true);
            puntosFinale.text = points.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
