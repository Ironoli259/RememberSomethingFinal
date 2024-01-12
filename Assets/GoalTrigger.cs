using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] int buildIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string loadingTxt = "";
            if (buildIndex == 3)
            {
                loadingTxt = "You remember the time when you were serving in the army. That particular mission in the abandoned village where the -incident- happened...\nCollect all the memories attached to this incident without being killed!";
                if (!GameManager.instance.playedMemFriend)
                    LevelLoader.instance.LoadNextLevel(buildIndex, loadingTxt, 10f);
            }
            else if (buildIndex == 4)
            {
                loadingTxt = "You remember that time when you were at the arcade bar. You're not sure why, but you believe you met someone very important that day... \nCollect the memories related to this person by clicking on them in the bar";
                if (!GameManager.instance.playedMemWife)
                    LevelLoader.instance.LoadNextLevel(buildIndex, loadingTxt, 10f);
            }
            if (GameManager.instance.playedMemFriend && GameManager.instance.playedMemWife)
                LevelLoader.instance.LoadNextLevel(1, "You finally managed to retrieve all your lost memories and realised that your loved ones died tragically in terrible accidents. You are now living in a nightmare and unable to get out!!\nGood job for completing this experience and thank you!!\nDeveloppers:\nJmar Dylan Antonio Hermosura\nOlivier Grenier\nDominic Audet",20f);
            //else message : already visited memory
        }
    }
}
