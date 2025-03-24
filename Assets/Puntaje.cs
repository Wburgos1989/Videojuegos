using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Puntaje : MonoBehaviour
{
    public TMP_Text scoreText;
    private Dictionary<string, int> scores = new Dictionary<string, int>();

    void Start()
    {
        // Inicializa los valores
        scores["Gemas"] = 0;
        scores["Pocion"] = 0;
        scores["Cartas"] = 0;

        UpdateScoreText();
    }

    public void AddScore(string item)
    {
        if (scores.ContainsKey(item))
        {
            scores[item]++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Gemas: {scores["Gemas"]} | Pocion: {scores["Pocion"]} | Cartas: {scores["Cartas"]}";
    }
}
