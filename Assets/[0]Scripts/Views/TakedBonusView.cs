﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace ConnectionLost.Views
{
    internal sealed class TakedBonusView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI useCountTmp;
        [SerializeField] private Image bonusIcon;
        [SerializeField] private Button activateButton;


        public void SetInitialized(Sprite bonusSprite, int countOfUse)
        {
            bonusIcon.gameObject.SetActive(true);
            bonusIcon.sprite = bonusSprite;
            if (countOfUse > 1)
            {
                useCountTmp.text = countOfUse.ToString();
            }
            else
            {
                useCountTmp.text = "";
            }

            activateButton.interactable = true;
        }

        public void Remove()
        {
            bonusIcon.gameObject.SetActive(false);
            useCountTmp.text = "";
            activateButton.interactable = false;
        }

        public void UpdateCountInfo(int count)
        {
            useCountTmp.text = count.ToString();
            activateButton.interactable = false;
        }
    }
}
