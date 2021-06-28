using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] allPanels;

    [Space]

    public GameObject StreamPanel;
    public GameObject TitlePanel;
    public GameObject ThumbPanel;
    public GameObject DialogPanel;

    [Space]

    public string[] lines;
    public int lineIndex;
    public GameObject FGusPanel;
    public GameObject[] FGusEmotes;
    public GameObject FMCPanel;
    public GameObject[] FMCEmotes;

    [Space]

    public GameObject[] objPanels;
    public GameObject OakPanel;
    public GameObject RowanPanel;
    public GameObject[] FishPanel;

    [Space]

    public int index;

    private void Start()
    {
        foreach(GameObject obj in allPanels)
        {
            SetActiveAll(false);
            obj.SetActive(false);
        }
        TitlePanel.SetActive(true);
    }

    public void Advance()
    {

    }

    public void SetActiveAll(bool active)
    {
        foreach (GameObject obj in allPanels)
        {
            obj.SetActive(active);
        }
    }

    public void SetDialog(int FGIndex, int FMCIndex, int ObjIndex)
    {
        int newIndex = index;
        DialogPanel.SetActive(true);
        lineIndex = newIndex;
        foreach(GameObject obj in FGusEmotes)
        {
            obj.SetActive(false);
        }
        FGusEmotes[FGIndex].SetActive(true);

        foreach (GameObject obj in FMCEmotes)
        {
            obj.SetActive(false);
        }
        FMCEmotes[FMCIndex].SetActive(true);

        foreach (GameObject obj in objPanels)
        {
            obj.SetActive(false);
        }
        objPanels[ObjIndex].SetActive(true);

        index++;
    }

}
