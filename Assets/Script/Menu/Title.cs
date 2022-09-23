using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] Canvas EscapeCanvas;
    [SerializeField] Canvas TitleCanvas;
    [SerializeField] Canvas SelectCanvas;
    [SerializeField] Canvas HelpCanvas;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _openClip;
    [SerializeField] AudioClip _closeClip;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscapeCanvas.enabled == true)
            {
                _audioSource.PlayOneShot(_closeClip);
            }
            else
            {
                _audioSource.PlayOneShot(_openClip);
            }
            EscapeCanvas.enabled = !EscapeCanvas.enabled;
            EscapeCanvas.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (HelpCanvas.enabled == true)
            {
                _audioSource.PlayOneShot(_closeClip);
            }
            else
            {
                _audioSource.PlayOneShot(_openClip);
            }
            HelpCanvas.enabled = !HelpCanvas.enabled;
            HelpCanvas.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {if(TitleCanvas.gameObject.activeSelf)
            {
            _audioSource.PlayOneShot(_openClip);
            }
            TitleCanvas.gameObject.SetActive(false);
            SelectCanvas.gameObject.SetActive(true);
        }
    }

}
