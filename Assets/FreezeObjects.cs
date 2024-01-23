using System.Collections;
using System.Collections.Generic;
using Scenes.StoryMode.LevelScripts;
using UnityEngine;

public class FreezeObjects : MonoBehaviour
{
    public string targetTag = "YourTag"; // Замените "YourTag" на фактический тег, который вы хотите нацелить
    public float freezeDuration = 5f;

    public void FreezeObjectsWithTag()
    {
        PlayerPrefs.SetInt("ShopItems_2", 0);
        PlayerPrefs.Save();
        var objectsToFreeze = GameObject.FindGameObjectsWithTag(targetTag);

        // Заморозить каждый объект
        foreach (var obj in objectsToFreeze)
        {
            // Попытаться вызвать метод Freeze() у объекта
            obj.GetComponent<Enemy>()?.Freeze();
        }

        // Начать обратный отсчет
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        var timer = freezeDuration;

        while (timer > 0)
        {
            Debug.Log("Обратный отсчет: " + timer.ToString("F1") + " секунд осталось...");
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        Debug.Log("Обратный отсчет завершен. Размораживание объектов.");

        // Разморозить все объекты после обратного отсчета
        UnfreezeObjectsWithTag();
    }

    private void UnfreezeObjectsWithTag()
    {
        // Найти все игровые объекты с указанным тегом
        var objectsToUnfreeze = GameObject.FindGameObjectsWithTag(targetTag);

        // Разморозить каждый объект
        foreach (var obj in objectsToUnfreeze)
        {
            // Попытаться вызвать метод Unfreeze() у объекта
            obj.GetComponent<Enemy>()?.Unfreeze();
        }
    }
}
