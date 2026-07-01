using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    public void Play(PlayableDirector director)
    {
        player.Lock(this);
        director.stopped += OnStopped;
        director.Play();
    }
    private void OnStopped(PlayableDirector director)
    {
        player.Unlock(this);
        // director.stopped -= OnStopped;
    }
}