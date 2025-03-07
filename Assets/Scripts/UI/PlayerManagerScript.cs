using UnityEngine;
using UnityEngine.UI;

public class PlayerManagerScript : MonoBehaviour
{   
    // get all the variable from the player class
    public Player player;
    //exp bar setting 
    public Image expbar;
    public float maxXp = 100f;

    //will change after we get the xp and level from others
    public int level = 1;
    public float xp = 0f;
    
    public GameObject LevelUpUI;
    public GameObject[] players; 
    private int index;

    public BulletSpawner bulletSpawner;
    public Bullet bulletPrefab;

    // BulletSpawner
    public float newFireRate = 5f;
    public int newBurstCount = 1;
    public int newParallelCount = 2;

    // Bullet
    public int newDamage = 20;
    public int newSplitCount = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (players != null && players.Length > 2 && players[2] != null)
        {
            GameObject child = players[2].transform.Find("SimpleBullet")?.gameObject;
            if (child != null)
            {
                Debug.Log("找到子对象：" + child.name);
                Debug.Log("找到对象：" + players[2].name);
            }
            else
            {
                Debug.LogError("SimpleBullet not found in player " + players[2].name);
            }
        }
        else
        {
            Debug.LogError("Player array is not properly initialized or player[2] is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("exp == " + player.exp);

        //update the exp bar 
        expbar.fillAmount = Mathf.Clamp(player.exp / maxXp, 0, 1);


        // add more player
        ChoosePlayer();

        //show the UI of choosing skills
        LevelUp();
    }



    public void LevelUp(){
        if(player.exp == 100){
            LevelUpUI.SetActive(true);
            player.exp = 0;
            level += 1;
        }
        
    }

    public void CloseUpgradeWindow(){
        LevelUpUI.SetActive(false);
        Debug.Log("upgrade the skills");
        Debug.Log("find the bullet  " + bulletPrefab.name);
        
    }

    public void ChoosePlayer(){
        if(level % 3 == 0){
            index = player.playerLevel / 3;
            players[index].SetActive(true);
            level = 1;
        }
    }


    public void ModifyBulletSpawnerProperties()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.fireRate += newFireRate;
            bulletSpawner.burstCount += newBurstCount;
            bulletSpawner.parallelCount += newParallelCount;
        }
    }

    public void ModifyBulletProperties()
    {
        if (bulletPrefab != null)
        {
            Debug.Log("find the bullet" + bulletPrefab.name);
            bulletPrefab.damage = newDamage;
            bulletPrefab.splitCount += newSplitCount;
        }
        else{
            Debug.LogError("do not find the bullet");
        }
    }



}