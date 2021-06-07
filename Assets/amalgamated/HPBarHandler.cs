using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.Progress;

public class HPBarHandler : MonoBehaviour
{

    Progressor hpProgressor;
    int maxHP = -1;
    int hp = -1;

    public void SetMaxHpValue(int value)
    {
        if (!hpProgressor)
        {
            maxHP = value;
            return;
        }
        hpProgressor.SetMax(value);
    }
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(int value)
    {
        if (!hpProgressor)
        {
            hp = value;
            return;
        }
        hpProgressor.SetValue(value);
        //if (HealthBarImage.fillAmount < 0.2f)
        //{
        //    SetHealthBarColor(Color.red);
        //}
        //else if (HealthBarImage.fillAmount < 0.4f)
        //{
        //    SetHealthBarColor(Color.yellow);
        //}
        //else
        //{
        //    SetHealthBarColor(Color.green);
        //}
    }

    //public float GetHealthBarValue()
    //{
    //    return HealthBarImage.fillAmount;
    //}

    ///// <summary>
    ///// Sets the health bar color
    ///// </summary>
    ///// <param name="healthColor">Color </param>
    //public void SetHealthBarColor(Color healthColor)
    //{
    //    HealthBarImage.color = healthColor;
    //}

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        hpProgressor = GetComponent<Progressor>();
        SetMaxHpValue(maxHP);
        SetHealthBarValue(hp);
    }
}