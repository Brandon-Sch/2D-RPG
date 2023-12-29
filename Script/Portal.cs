using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : Collidable {
    protected override void OnCollide(Collider2D coll) 
    {
        if (coll.name == "Player") 
        {
            //Teleport the player
            GameManager.instance.SaveState();
            string sceneName = "Dungeon1";
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
