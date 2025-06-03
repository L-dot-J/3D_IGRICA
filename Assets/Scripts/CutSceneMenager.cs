using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private ScreenFade fader;

    private void Start()
    {
        StartCoroutine(PlayCutsceneSequence());
        
    }

    IEnumerator PlayCutsceneSequence()
    {

        yield return fader.FadeFromBlack();

        director.Play();

        
        yield return new WaitForSeconds(6f);
        StartCoroutine(fader.FadeToBlack());

        
        while (director.state == UnityEngine.Playables.PlayState.Playing)
            yield return null;

        PlayerPrefs.SetInt("CutscenePlayed", 1);
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
