using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class WeaponInfoUI : MonoBehaviour
{
    public TMP_Text currentBullets;
    public TMP_Text totalBullets;
    private void OnEnable()
    {
        EventManager.current.updateBulletsEvent.AddListener(UpdateBullets);
    }
    private void OnDisable()
    {
        
    }
    public void UpdateBullets(int newCurrentBullets, int newTotalBullets)
    {
        if(newCurrentBullets <= 0)
        {
            currentBullets.color = new Color(255, 0, 0);
        }
        else
        {
            currentBullets.color = new Color(255, 255, 255);
        }
        currentBullets.text = newCurrentBullets.ToString();
        totalBullets.text = newTotalBullets.ToString();
    }
}