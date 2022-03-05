using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class gamepause : MonoBehaviour
{
    public Text SCORETEXT;
    private bool GameIsPaused;
    private GameObject oof;
    public GameObject BGMENU;
    // Start is called before the first frame update
    void Start()
    {
        oof = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SCORETEXT.text = localsetup.score.ToString()+"/12";
        if (localsetup.score >= 12)
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          
            //Debug.Log(oof.Length);
            if (GameIsPaused)
            {
                 oof.SetActive(false);
                //   oof.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                GameIsPaused = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                oof.SetActive(true);
                //  oof.SetActive(true);
                GameIsPaused = true;
            }
            
        }
    }

   public void resume()
    {
       BGMENU.SetActive(false);
        //   oof.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

   public void exit()
   {
       Application.Quit();
   }
}
