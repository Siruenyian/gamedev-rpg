using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BattleStatusBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text nameText;

    public void SetHealth(int current, int max)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateHealth(current, max));
    }
    public void SetName(string name)
    {
        nameText.text = $"{name}";
    }

    private IEnumerator AnimateHealth(int current, int max)
    {
        float start = fill.fillAmount;
        float target = (float)current / max;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            fill.fillAmount = Mathf.Lerp(start, target, t);
            yield return null;
        }

        fill.fillAmount = target;
        hpText.text = $"{current}/{max}";
    }
}