using UnityEngine;
using System;

public class Block_Script : MonoBehaviour
{
    public int HP = 1;
    public static event Action<Block_Script> OnBD;
    public GameObject Death;
    public float AddSpeed = 100;
    [SerializeField] private Sprite oneHP;
    [SerializeField] private Sprite twoHP;
    [HideInInspector]public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        ball Ball = coll.gameObject.GetComponent<ball>();
        APCL(Ball);//Apply Collison Logic
        
    }
    private void APCL(ball Ball)
    {
        this.HP--;
        if(this.HP<=0)
        {   //you ask why check GameManager for answer
            OnBD?.Invoke(this);
            gameObject.transform.tag = "deadBlock";
            Instantiate(Death, transform.position, transform.rotation);
            GameManager.Instance.VictoryCheck();
            Destroy(this.gameObject);
        }
        else
        {
            OnBD?.Invoke(this);
            GetComponent<Animator>().enabled = false;
            if (this.HP == 2)
            {
                spriteRenderer.sprite = twoHP;
            }
            else
            {
                spriteRenderer.sprite = oneHP;
            }
        }
    }
}
