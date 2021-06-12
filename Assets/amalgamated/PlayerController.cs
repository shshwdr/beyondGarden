﻿using DG.Tweening;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//using PixelCrushers.DialogueSystem;
public class PlayerController: FriendController
{

    public Dictionary<string, int> inventory = new Dictionary<string, int>();
   // public static Player instance = null;
    Vector2 movement;
    public float moveSpeed = 5f;

    public PlayerMeleeAttack meleeAttackCollider;
    Vector3 originMeleeAttackPosition;
    bool firstClear = true;
    bool spawned = false;
    public AnimatorOverrideController animatorController;

    public float dashTime = 0.2f;
    float currentDashTimer = 0f;
    public float dashCooldown = 0.5f;
    float currentDashCooldownTimer = 0f;
    public float dashScale = 5f;
    List<AllyController> allyList = new List<AllyController>();

    public float disembleForce = 10f;
    public float stopAttachTime = 1f;
    int allyCount = 0;
    public int maxAllyCount = 10;

    public GameObject gameOverObject;

    // private void Awake()
    //{
    //if (instance == null)

    //    //if not, set instance to this
    //    instance = this;

    ////If instance already exists and it's not this:
    //else if (instance != this)
    //{
    //    instance.gameObject.transform.position = transform.position;
    //    //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
    //    Destroy(gameObject);
    //}


    ////Sets this to not be destroyed when reloading scene
    //DontDestroyOnLoad(gameObject);
    //}
    // Start is called before the first frame update

    public void updateToMergedPlayer()
    {
        animator.runtimeAnimatorController = animatorController;
    }
    protected override void Start()
    {

        EnemyManager.instance.player = this;
        //originMeleeAttackPosition = meleeAttackCollider.transform.localPosition;
        base.Start();

        animator = transform.Find("Sprites").GetComponent<Animator>();
        spriteObject = animator.gameObject;

        if (FModSoundManager.Instance.isMerged)
        {
            updateToMergedPlayer();
        }
    }

    //public void Damage(int dam = 1)
    //{

    //    GameOver();
    //}

    //void GameOver()
    //{
    //    if (GameManager.instance.dontDie)
    //    {
    //        return;
    //    }
    //    SoundManager.instance.playBGM(gameoverClip);
    //    GameManager.instance.GameOver();


    //}

    //public void pause()
    //{
    //    GameManager.instance.isPaused = true;
    //}
    //public void resume()
    //{
    //    GameManager.instance.isPaused = false;
    //}
    // Update is called once per frame

    bool isDashing()
    {
        return currentDashTimer > 0;
    }
    bool canDashing()
    {
        return currentDashCooldownTimer <= 0;
    }
    override protected void Update()
    {
        if (isDead)
        {
            stopAttackAnim();

            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            disemble();
        }
        //if (GameManager.instance.isCheatOn && Input.GetKeyDown(KeyCode.M))
        //{
        //    GameOver();
        //    return;
        //}
        //if (GameManager.instance.isPaused)
        //{
        //    moveSpeed = 0;
        //    movement = Vector2.zero;
        //    return;
        //}
        if (currentDashTimer > 0)
        {

            currentDashTimer -= Time.deltaTime;
        }
        if (currentDashCooldownTimer > 0)
        {
            currentDashCooldownTimer -= Time.deltaTime;
        }

        if (EnemyManager.instance.isLevelCleared && firstClear && DungeonManager.Instance.currentLevel != 0 && DungeonManager.Instance.currentLevel != 7)
        {
            firstClear = false;
            animator.SetTrigger("victory");

        }

        movement.x = Input.GetAxisRaw("Horizontal");

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        movement.y = Input.GetAxisRaw("Vertical");
        float speed = movement.sqrMagnitude;
        movement = Vector2.ClampMagnitude(movement, 1);
        animator.SetFloat("speed", movement.sqrMagnitude);


        if (speed>0.001 && Input.GetMouseButtonDown(0) && !isDashing() && canDashing())
        {
            currentDashTimer = dashTime;
            currentDashCooldownTimer = dashCooldown;
        }

        base.Update();


        ////melee prepare
        //if (speed > 0.01f)
        //{
        //    Time.timeScale = 1;
        //    meleeAttackCollider.setactive(true);
        //    var dir = new Vector3(movement.x, movement.y, 0)*0.08f;
        //    // The shortcuts way
        //    //meleeAttackCollider. transform.DOMove(transform.position + dir, 1);
        //    // The generic way
        //    DOTween.To(() => meleeAttackCollider.transform.localPosition, x => meleeAttackCollider.transform.localPosition = x, dir, 0.5f);

        //    //DOTween.To(() => transform.position, x => transform.position = x, new Vector3(2, 2, 2), 1);

        //}
        //else
        //{

        //    meleeAttackCollider.setactive(false);
        //    meleeAttackCollider.transform.localPosition = Vector3.zero;
        //    stopAttackAnim();
        //}
        // transform

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    getDamage(1000);
        //}

        if (FModSoundManager.Instance.isMerged && Input.GetKeyDown(KeyCode.Space) && DungeonManager.Instance.currentLevel == 6&& DialogueEventHelper.Instance.dialogueFinished)
        {
            var dir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            EnemyManager.instance.spawnMinions(transform.position+ dir);
            if (!spawned)
            {
                spawned = true;

                DialogueManager.StartConversation("firstMerge", null, null);
            }

        }
    }
    override protected void playHurtSound()
    {

        AudioManager.Instance.playPlayerHurt();
    }

    public void attackAnim()
    {
        animator.SetBool("attack", true);
        animator.SetFloat("attackHorizontal", movement.x);

        animator.SetFloat("attackVertical", movement.y);
    }

    public void stopAttackAnim()
    {

        animator.SetBool("attack", false);
    }

    private void LateUpdate()
    {
        if (isDead)
        {
            return;
        }
        var speed = moveSpeed;
        if (isDashing())
        {
            speed *= dashScale;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        testFlip(movement);
        // rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing())
        {

            var enemy = collision.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.getDamage();
            }
            var stone = collision.GetComponent<DesctroyableObstacle>();
            if (stone)
            {
                stone.getDamage();
            }
        }
        if (collision.GetComponent<RoomKey>())
        {
            inventory["key"] = 1;
            Destroy(collision.gameObject);
        }
    }
    protected override void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        animator.SetTrigger("die");

        //AudioManager.Instance.playGameOver();
        FModSoundManager.Instance.SetParam("Game Over", 1);
        Invoke("gameover", 1);
    }
    void gameover()
    {
        //GameOver.Instance .Gameover();
        gameOverObject.SetActive(true);
    }
    void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void getAlly(AllyController ac)
    {
        if(allyCount>= maxAllyCount)
        {

        }
        else
        {
            ac.gameObject.transform.parent = transform;
            ac.isAttachedToPlayer = true;
            ac.playerController = this;
            allyList.Add(ac);
            ac.getAttached();
            allyCoundAdd(1);
        }

    }
    public void allyCoundAdd(int v)
    {
        allyCount += v;

        MusicManager.Instance.allyChanged(allyCount);
    }

    public void disemble()
    {
        foreach(AllyController ac in allyList)
        {
            if (ac && !ac.isDead)
            {
                ac.getDisembled();
            }
        }
        allyList.Clear();

        allyCount =0;

        MusicManager.Instance.allyChanged(allyCount);
    }
    
}
