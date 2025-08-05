using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Polarity { Red, Blue }

public class MagneticPlayer : MonoBehaviour
{
    public Polarity currentPolarity = Polarity.Red;
    public float switchInterval = 5f;

    private float switchTimer;

    void Update()
    {
        switchTimer += Time.deltaTime;
        if (switchTimer >= switchInterval)
        {
            SwitchPolarity();
            switchTimer = 0f;
        }
    }

    void SwitchPolarity()
    {
        currentPolarity = (currentPolarity == Polarity.Red) ? Polarity.Blue : Polarity.Red;

        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            rend.material.color = (currentPolarity == Polarity.Red) ? Color.red : Color.blue;
        }
    }

}
