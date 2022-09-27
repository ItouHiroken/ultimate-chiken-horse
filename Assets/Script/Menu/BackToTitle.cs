using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToTitle : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] string _sceneName;
    [SerializeField] float _time;
    [SerializeField] float _a;
    private void Update()
    {
        _a += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) || _a >= _time)
        {
            _audioSource.PlayOneShot(_clip);
            SceneManager.LoadScene(_sceneName);
        }
    }

}
