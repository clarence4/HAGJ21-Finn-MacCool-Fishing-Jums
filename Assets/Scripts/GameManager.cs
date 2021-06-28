using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ClickHandler ch;
    public FishGenerator fg;
    public UIManager UI;
    public VoiceManager vm;
    public int gameState = 0;
    public int voiceIndex = 0;
    public int dialogIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Advance()
    {
        if (gameState == 0)
        {
            UI.TitlePanel.SetActive(false);
            UI.StreamPanel.SetActive(true);
            
        }
        else if (gameState == 1)
        {
            UI.StreamPanel.SetActive(false);
            UI.SetDialog(4, 4, 5);
            vm.Play();

            
        }
        else if (gameState == 2)
        {
            UI.SetDialog(4, 4, 5);

        }
        else if (gameState == 3)
        {
            UI.SetDialog(4, 4, 5);

        }
        else if (gameState == 4)
        {
            UI.SetDialog(4, 4, 5);

        }
        else if (gameState == 5)
        {
            vm.Stop();
            UI.SetDialog(0, 4, 5);
            vm.Play();

        }
        else if (gameState == 6)
        {
            vm.Stop();
            UI.SetDialog(4, 4, 5);
        }
        else if (gameState == 7)
        {
            UI.SetDialog(4, 4, 5);
        }
        else if (gameState == 8)
        {
            UI.SetActiveAll(false);
            ch.UI = false;
            fg.SpawnPollon();
        }
        //else if (gameState == )
        //{

        //}
        //else if (gameState == )
        //{

        //}
        else
        {

        }
        gameState++;
        
    }

}
