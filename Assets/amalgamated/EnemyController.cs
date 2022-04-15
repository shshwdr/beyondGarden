using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
public class EnemyController : HPCharacterController
{
    //NavMeshAgent agent;
    AIPath pathFinding;
    AIDestinationSetter pathSetter;
    public bool isMelee;
    public float meleeRadius;
    public float meleeCooldown;
    float meleeCooldownTimer;


    public TMP_Text levelText;

    public EnemyType enemyType;

    public int mergeLevel;

    public bool isMerging;

    public GameObject mergedToMonster;

    public float invincibleSpeedScale = 0.3f;
    float originSpeed;
    SpriteRenderer m_Renderer;
    protected EnemyController mergingOther;
    public Animator deathAnimator;

    float offMergeDistance = 0;

    public string enemyId = "deamon0";
    EnemyInfo enemyInfo;
    int level = 0;

    public RoomController room;

    Vector3 originalPosition;

    public void Init(DungeonLevelInfo dungeonInfo,int index)
    {
        enemyId = dungeonInfo.enemies[index];
        level = Random.Range(dungeonInfo.levels[0], dungeonInfo.levels[1]+1);

    }

    protected override void Awake()
    {
        base.Awake();
        
        originalPosition = transform.position;
        pathFinding = GetComponent<AIPath>();
        pathFinding.enabled = false;
        pathSetter = GetComponent<AIDestinationSetter>();


    }
    //Rigidbody2D rb;
    // Start is called before the first frame update
    protected override void Start()
    {

        //rb = GetComponent<Rigidbody2D>();

        //transform.rotation = Quaternion.identity;
        EnemyManager.instance.enemiesDictionary[enemyType].Add(this);
        base.Start();

        animator = transform.Find("test").GetComponentInChildren<Animator>();
        spriteObject = animator.transform.parent.gameObject;
        EnemyManager.instance.updateEnemies();
        m_Renderer = animator. GetComponent<SpriteRenderer>();
        offMergeDistance = GetComponent<CircleCollider2D>().radius * 2f;

        if (FModSoundManager.Instance.isMerged && !FModSoundManager.Instance.getHelpDialogue && mergeLevel == 2)
        {
            FModSoundManager.Instance.getHelpDialogue = true;
            DialogueManager.StartConversation("getHelp", null, null);
        }

        //enemyInfo = JsonManager.Instance.getEnemy(enemyId);
        //elementType = enemyInfo.type;
        //maxHp = getMapHP();
        //hp = maxHp;
        updateHP();
        levelText.text = "LvL " + level;

    }

    int getMapHP()
    {
        var res = enemyInfo.hp;
        var levelIncrease = enemyInfo.hpIncrease*(level-1);
        return Mathf.CeilToInt(res);
    }

    bool isBoss()
    {
        return mergeLevel >= EnemyManager.instance.enemyMaxLevel;
    }
    public bool canBePaired()
    {
        return !isDead && !isMerging && !isBoss();
    }

    float getDistanceToTarget(Transform target)
    {
        //todo use navmesh distance instead of position distance
        return ((Vector2)transform.position - (Vector2)target.position).magnitude;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDead || EnemyManager.instance.player.isDead)
        {
            pathFinding.enabled = false;
            return;
        }
        base.Update();

        if (room && !room.isRoomActive)
        {
            //walk back?

            pathFinding.enabled = false;
        }

        //move
        else
        {
            //find the cloest target, either player or enemy with same type and merge level
            float shortestDistance = 10000f;
            Transform shortestTarget = transform;
            bool foundTarget = false;
            if (isMelee)
            {
                foundTarget = true;
                if (getDistanceToTarget(EnemyManager.instance.player.transform) < meleeRadius)
                {
                    meleeCooldownTimer += Time.deltaTime;
                }
                else
                {
                    meleeCooldownTimer = 0;
                }
                if (meleeCooldownTimer >= meleeCooldown)
                {
                    meleeCooldownTimer = 0;
                    animator.SetTrigger("attack");
                    EnemyManager.instance.player.getDamage();
                    if (GetComponent<AudioSource>())
                    {
                        GetComponent<AudioSource>().PlayOneShot(MusicManager.Instance.meleeAttack);
                    }
                }
            }

            pathFinding.enabled = true;
            if (foundTarget)
            {
                if (pathSetter)
                {

                    pathSetter.target = EnemyManager.instance.player.transform;
                }

            }
        }
    }

    public void addRoom(RoomController r)
    {
        room = r;
    }
    protected override void Die()
    {
        //spawn drops if lucky!
        //always generate items
        //generate seed

        //generateDrop();


        if (EnemyManager.instance.bossController)
        {
            EnemyManager.instance.bossController.spawnersDie();
        }
        base.Die();
        room.getEnemyDie();
        EnemyManager.instance.updateEnemies();
        animator.SetTrigger("die");
       // AudioManager.Instance.playMonsterDie(mergeLevel);
        //deathAnimator.enabled = true;
        Destroy(gameObject, 1f);

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(MusicManager.Instance.killMonster);
        }
    }

}
