using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screens : MonoBehaviour
{
    public GameEvent EndTransition;
    public GameEvent StartFlashback;
    public GameEvent EndFlashback;
    public Image flashback;
    public Image transition;
    public int currentMemorySprite;
    public int flashbackNumber;
    public int[] spritesPerFlashback;
    public Sprite[] memorySprites;
    public float transitionSpeed;
    public float[] timePerImage;
    public float timeInBlackScreen;

    private void Start()
    {
        StartFlashback.Raise();
    }

    public void CallFlashback()
    {
        StartCoroutine("Flashback");
    }

    public IEnumerator Flashback()
    {
        yield return new WaitForSeconds(2);
        yield return TransitionFadeIn();
        flashback.enabled = true;
        for (int i = 0; i < spritesPerFlashback[flashbackNumber]; i++)
        {
            flashback.sprite = memorySprites[currentMemorySprite];
            yield return new WaitForSeconds(timeInBlackScreen);
            yield return TransitionFadeOut();
            yield return new WaitForSeconds(timePerImage[currentMemorySprite]);
            yield return TransitionFadeIn();
            currentMemorySprite++;
        }
        flashbackNumber++;
        flashback.enabled = false;
        yield return new WaitForSeconds(0.7f);
        yield return TransitionFadeOut();
        yield return new WaitForSeconds(0.3f);
        EndFlashback.Raise();
    }

    public IEnumerator TransitionFadeIn()
    {
        while (transition.color.a < 1)
        {
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, transition.color.a + transitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator TransitionFadeOut()
    {
        while (transition.color.a > 0)
        {
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, transition.color.a - transitionSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        EndTransition.Raise();
    }

    public void ResetFlashback()
    {
        currentMemorySprite = 5;
    }
}
