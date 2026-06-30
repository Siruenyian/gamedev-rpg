using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private bool playOnce = true;

    private bool hasPlayed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed && playOnce)
            return;

        if (!other.CompareTag("Player"))
        {
            return;
        }

        hasPlayed = true;
        cutsceneManager.Play(director);
    }
}