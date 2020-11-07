using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public enum _Skills
    {
        TripleShot,
        DualShot,
        QuickShot,
        FastShot,
        Clone
    }

    [HideInInspector] public List<PlayerController> players = new List<PlayerController>();

    [HideInInspector] public int numOfActiveSkills = 0;

    [HideInInspector] public List<_Skills> usedSkills = new List<_Skills>();
    [SerializeField] Button[] skillButtons;


    public void ActivateSkill(int index)
    {
        if (numOfActiveSkills >= 3) return;

        switch ((_Skills)index)
        {
            case _Skills.TripleShot:
                foreach (var player in players)
                {
                    TripleShot triple = new TripleShot(player.attacker);
                    player.attacker = triple;
                }
                numOfActiveSkills++;
                break;
            case _Skills.DualShot:
                foreach (var player in players)
                {
                    DualShot dual = new DualShot(player.attacker);
                    player.attacker = dual;
                }
                numOfActiveSkills++;
                break;
            case _Skills.QuickShot:
                foreach (var player in players)
                {
                    QuickShot quick = new QuickShot(player.attacker);
                    player.attacker = quick;
                }
                numOfActiveSkills++;
                break;
            case _Skills.FastShot:
                foreach (var player in players)
                {
                    FastBullet fast = new FastBullet(player.attacker);
                    player.attacker = fast;
                }
                numOfActiveSkills++;
                break;
            case _Skills.Clone:
                ClonePlayer();
                players = FindObjectsOfType<PlayerController>().ToList();
                numOfActiveSkills++;
                break;
            default:
                Debug.LogError("Error: SkillManager>ActivateSkill(int index) , param= " + index.ToString() + "is out of bounds");
                break;
        }

        if (numOfActiveSkills >= 3)
        {
            foreach (var item in skillButtons)
            {
                item.interactable = false;
            }
        }

        usedSkills.Add((_Skills)index);
    }

    void ClonePlayer()
    {
        Vector3 radnomPos = new Vector3(Random.Range(-1.5f,1.5f),0,Random.Range(-5.5f,-2f));
        GameObject g = GameObject.Instantiate(players[0].gameObject, radnomPos, players[0].transform.rotation);
        PlayerController player = g.GetComponent<PlayerController>();
        foreach (var item in usedSkills)
        {
            switch (item)
            {
                case _Skills.TripleShot:

                    TripleShot triple = new TripleShot(player.attacker);
                    player.attacker = triple;

                    break;
                case _Skills.DualShot:

                    DualShot dual = new DualShot(player.attacker);
                    player.attacker = dual;

                    break;
                case _Skills.QuickShot:

                    QuickShot quick = new QuickShot(player.attacker);
                    player.attacker = quick;

                    break;
                case _Skills.FastShot:

                    FastBullet fast = new FastBullet(player.attacker);
                    player.attacker = fast;

                    break;
                default:
                    Debug.LogError("Error: SkillManager>ClonePlayer() , param= usedSkills is out of bounds");
                    break;
            }
        }
    }


}
