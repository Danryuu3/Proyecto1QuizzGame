using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public TextMeshProUGUI puntosMaximos;
    public TextMeshProUGUI puntosMaximos2;
    int maximos = 0;

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

        maximos = PlayerPrefs.GetInt("maximo");
        puntosMaximos.text = maximos.ToString();
        
    }

    //Metodo que inicia el Quizz
    public void BotonInicio(Button boton)
    {
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
    }

    //Método que avisa si superaste el record.
    public void PuntajeMaximo()
    {
        if (maximos <= points)
        {
            puntosMaximos2.text = "Felicidades tienes el puntaje más alto";
        }
        else
        {
            puntosMaximos2.text = "Sigue intentandolo el record es de: " + maximos.ToString();
        }
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

    //Método que reinicia el juego.
    public void Reinicia()
    {
        SceneManager.LoadScene(0);
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
            yield return new WaitForSeconds(.5f);
            ActiveQuestion();
        }
        else
        {
            boton.image.color = Color.green;
            points += 100;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(0.5f);
            canvas[1].SetActive(false);
            canvas[2].SetActive(true);
            puntosFinale.text = "Has logrado " + points.ToString() + " puntos";
        }
    }

    //Corrutina que le da función a los botones de respuesta incorrecta.
    public IEnumerator IncorrectQuestionsCoroutine(Button boton)
    {
        if (countQuestions != numberQuestion)
        {
            boton.image.color = Color.red;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(0.5f);
            ActiveQuestion();
        }
        else
        {
            boton.image.color = Color.red;
            puntos.text = points.ToString();
            yield return new WaitForSeconds(0.5f);
            canvas[1].SetActive(false);
            canvas[2].SetActive(true);
            puntosFinale.text = "Has logrado " + points.ToString() + " puntos";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numberQuestion == countQuestions)
        {
            if (maximos < points)
            {
                PlayerPrefs.SetInt("maximo", points);
            }
        }
        PuntajeMaximo();
    }
}
