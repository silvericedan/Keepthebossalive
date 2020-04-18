using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    // Referenciamos una clase AudioMixer para poner controlar su volumen a travez de la funcion
    // el valor float lo introducimos cuando llamamos la funcion moviendo el slider
    public AudioMixer audioMixer;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeParam", volume);
    }
}
