using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Singleton pattern
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    //Block reference for spawning
    [Header("Block Info")]
    public GameObject Block;
    public Vector3 Spawn_Block_Pos;
    public float Block_X_Dist = 0.5f, Block_Y_Dist = 0.25f;
    public int Block_Rows = 4;
    public List<Color32> Colours;
    private int _totalBlocks = 0;    

    //Variables related to the game bounds and 
    [Header("Game Variables")]
    public float XBound = 3, YBound = 4;
    public int MaxLives = 3;
    private int _lives = 0;
    public int BlockScoreAmount = 100; 
    private int _score = 0;

    //Information for UI display
    [Header("User Interface")]
    public Text WinDisplay;
    public string WinText = "Win!";
    public Text LoseDisplay;
    public string LoseText = "Lose!";
    public Text LivesDisplay;
    public string LivesText = "Lives: ";
    public Text ScoreDisplay;
    public string ScoreText = "Score: ";

    // Start is called before the first frame update
    void Awake(){
        _instance = this;

        _lives= MaxLives;
        //Spawn in blocks
        for (float x = Spawn_Block_Pos.x; x<XBound; x+=Block_X_Dist){
            for(int y = 0; y<Block_Rows; y++){
                _totalBlocks++;
                Vector3 blockPos = new Vector3(x, Spawn_Block_Pos.y+y*Block_Y_Dist*-1);
                GameObject blockClone = GameObject.Instantiate(Block);
                blockClone.transform.position = blockPos;

                //Set colour
                SpriteRenderer blockSprite = blockClone.GetComponent<SpriteRenderer>();
                blockSprite.color = Colours[y];
            }
        }

        //Text setup
        LivesDisplay.text = LivesText+_lives;
        ScoreDisplay.text = ScoreText+_score;
    }

    //Function that covers what to do when a ball is lost
    public void OnLoseBall(){
        if(Time.time>0.5f){
            //Lose a life
            _lives-=1; 
            //Update lives display text
            LivesDisplay.text = LivesText+_lives;

            //Play sound for losing ball?

            if(_lives==0){
                LoseDisplay.text = LoseText;
                Time.timeScale = 0;
            }
        }
    }

    //Function that covers what to do when a block is broken
    public void OnBreakBlock(){
        _totalBlocks--;
        _score+=BlockScoreAmount;
        //Update score display text
        ScoreDisplay.text = ScoreText+_score;

        //Play sound for breaking block?

        if (_totalBlocks==0){
            WinDisplay.text = WinText;
            Time.timeScale = 0;
        }
    }

}
