using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] string SceneName;
    public void OnPointerClick(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(_clip);
        SceneManager.LoadScene(SceneName);
    }
}
