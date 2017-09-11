using UnityEngine;
using System.Collections;

public class Colour : MonoBehaviour
{
    /// <summary>
    /// Class which gets the sprite renderer's material colour of the player 
    /// This is updated when the user selects which colour character they wish to be and all colour variations can be found in 'ColourChanger' class
    /// </summary>

    //Static variables because they are used accross scenes
    public static Color m_colour;
    public static SpriteRenderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.material.color = ColourChanger.m_colour;
    }
}
