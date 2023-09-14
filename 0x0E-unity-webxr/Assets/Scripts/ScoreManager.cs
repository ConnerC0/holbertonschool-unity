using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance; // Singleton

        public int score = 0;
        public TextMeshProUGUI scoreText; // Using TextMeshPro for the score display

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void IncrementScore()
        {
            score++;
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            if (scoreText)
                scoreText.text = "" + score;
        } 
    }

