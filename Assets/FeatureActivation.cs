using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureActivation : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    private void Start()
    {
        // Проверка купленных фич и активация соответствующих кнопок
        CheckAndActivateFeatures();
    }

    private void CheckAndActivateFeatures()
    {
        // Проверка каждой фичи и активация соответствующей кнопки
        ActivateButtonBasedOnPlayerPrefs(button1, 0);
        ActivateButtonBasedOnPlayerPrefs(button2, 1);
        ActivateButtonBasedOnPlayerPrefs(button3, 2);
    }

    private const string PlayerPrefsKey = "ShopItems";

    private void ActivateButtonBasedOnPlayerPrefs(Button button, int featureIndex)
    {
        // Получаем уникальный ключ для текущей фичи
        var key = PlayerPrefsKey + "_" + featureIndex;

        // Чтение данных из PlayerPrefs
        // Активация или деактивация кнопки в зависимости от наличия фичи
        button.interactable = PlayerPrefs.GetInt(key, 0) == 1;
    }
}
