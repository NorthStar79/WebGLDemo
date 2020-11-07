using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Button[] SkillButtons;


    public void PlayButtonClicked()
    {
        GameObject.Instantiate(PlayerPrefab, Vector3.zero, Quaternion.Euler(0, 92, 0));
        SkillManager skillManager = FindObjectOfType<SkillManager>();
        skillManager.players = FindObjectsOfType<PlayerController>().ToList();

        foreach (var item in SkillButtons)
        {
            item.interactable = true;
        }
    }

    public void MenuButtonClicked()
    {
        PlayerController[] playerManager = FindObjectsOfType<PlayerController>();
        foreach (var item in playerManager)
        {
            Destroy(item.gameObject);
        }
        SkillManager skillManager = FindObjectOfType<SkillManager>();
        skillManager.numOfActiveSkills = 0;
        skillManager.usedSkills.Clear();
        skillManager.players.Clear();
    }
}
