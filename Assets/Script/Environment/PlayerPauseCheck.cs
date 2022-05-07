using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseCheck : MonoBehaviour
{
    private ThirdPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
    }
    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    // Update is called once per frame
    void Update()
    {
        if(player.IsDead == true)
        {
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
