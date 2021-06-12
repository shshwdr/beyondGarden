using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : HPCharacterController
{
    NavMeshAgent agent;

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

    RoomController room;

    Vector3 originalPosition;

    public void Init(DungeonLevelInfo dungeonInfo,int index)
    {
        enemyId = dungeonInfo.enemies[index];
        level = Random.Range(dungeonInfo.levels[0], dungeonInfo.levels[1]+1);

    }

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        originalPosition = transform.position;
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
        originSpeed = agent.speed;
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
        //if(DungeonManager.Instance.currentLevel == 7)
        //{
        //    //agent.isStopped = true;
        //    return;
        //}
        if (isDead || EnemyManager.instance.player.isDead)
        {
            agent.isStopped = true;
            return;
        }
        base.Update();
        if (isStuned)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            //return;
        }

        else if (room && !room.isRoomActive)
        {
            //walk back?

            agent.isStopped = false;
            agent.SetDestination(originalPosition);
            testFlip(agent.velocity);
        }

        //move
        else
        {
            //find the cloest target, either player or enemy with same type and merge level
            float shortestDistance = 10000f;
            Transform shortestTarget = transform;
            bool foundTarget = false;
            if (!FModSoundManager.Instance.isMerged)
            {
                getDistanceToTarget(EnemyManager.instance.player.transform);
                shortestTarget = EnemyManager.instance.player.transform;
                foundTarget = true;
            }
            if (m_Renderer.isVisible)
            {
                agent.speed = originSpeed;
            }
            else
            {
                agent.speed = originSpeed * invincibleSpeedScale;

                shortestTarget = transform;
                shortestDistance = float.MaxValue;
            }
            //foreach (EnemyController enemy in EnemyManager.instance.enemiesDictionary[enemyType])
            //{
            //    if (!enemy || enemy.isDead)
            //    {
            //        continue;
            //    }
            //        if (enemy == this)
            //    {
            //        continue;
            //    }
            //    if (!enemy.canBePaired())
            //    {
            //        continue;
            //    }
            //    if (enemy.mergeLevel != mergeLevel)
            //    {
            //        continue;
            //    }
            //    float distance = getDistanceToTarget(enemy.transform);
            //    if (distance < shortestDistance)
            //    {
            //        shortestTarget = enemy.transform;
            //        shortestDistance = distance;
            //        foundTarget = true;
            //    }
            //}
            if (foundTarget)
            {

                agent.isStopped = false;
                agent.SetDestination(shortestTarget.position);
                testFlip(agent.velocity);
            }
            else
            {
                agent.isStopped = true;
            }
        }
        animator.SetFloat("speed", agent.velocity.magnitude);
    }

    bool canBePairedWith(EnemyController other)
    {
        return false;
        if (!other.canBePaired() || !canBePaired())
        {
            return false;
        }
        if (other.mergeLevel != mergeLevel)
        {
            return false;
        }
        if (other.enemyType != enemyType)
        {
            return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        if(collision.GetComponent<EnemyController>()&& canBePairedWith(collision.GetComponent<EnemyController>()))
        {
            StartCoroutine( Merge(collision.GetComponent<EnemyController>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        //if(isMerging &&  collision.GetComponent<EnemyController>() == mergingOther)
        //{
        //    StopMerging();
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (isBoss() && collision.GetComponent<PlayerController>())
        //{
        //    collision.GetComponent<PlayerController>().getDamage();
        //}
    }
    void StopMerging() {
        if (mergingOther)
        {
            mergingOther.isMerging = false;
            mergingOther.emotesController.showEmote(EmoteType.heartBreak);
        }
        isMerging = false;
        emotesController.showEmote(EmoteType.heartBreak);
        mergingOther.mergingOther = null;
        mergingOther = null;
        StopAllCoroutines();
    }

    IEnumerator Merge(EnemyController other)
    {
        other.isMerging = true;
        isMerging = true;
        mergingOther = other;
        other.mergingOther = this;
        emotesController.showEmote(EmoteType.heart,true);
        other.emotesController.showEmote(EmoteType.heart,true);
        yield return new WaitForSeconds(2);
        if (isDead)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (isMerging)
        {

            //this is the main merger.
            //show merging effect
            //destory these two and create new monster
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameObject mergedMonster = Instantiate(mergedToMonster, (transform.position + other.transform.position) / 2.0f, Quaternion.identity);

            mergedMonster.GetComponent<EnemyController>().emotesController.showEmote(EmoteType.happy);
            if (EnemyManager.instance.bossController)
            {
                EnemyManager.instance.bossController.spawnersMerge();
            }
            AudioManager.Instance.playMerge();
        }


    }

    void generateDrop()
    {
        List<PairInfo<float>> drops = enemyInfo.drop;
        var selectedDrop = Utils.randomList(drops);
        if(selectedDrop == null)
        {
            return;
        }
        List<PairInfo<int>> res  = new List<PairInfo<int>>();
        int randId;
        switch (selectedDrop)
        {
            case "seed":
                var unlockedSeeds = ResourceManager.Instance.unlockedSeed();
                if(unlockedSeeds.Count == 0)
                {
                    return;
                }
                randId = Random.Range(0, unlockedSeeds.Count);
                
                var pairInfo = new PairInfo<int>(unlockedSeeds[randId], 1);
                res = new List<PairInfo<int>>() { pairInfo };
                break;
            case "resource":
                var dropableResource = ResourceManager.Instance.unlockedSeed();
                if (dropableResource.Count == 0)
                {
                    return;
                }
                randId = Random.Range(0, dropableResource.Count);

                var pairInfo2 = new PairInfo<int>(dropableResource[randId], 1);
                res = new List<PairInfo<int>>() { pairInfo2 };
                break;
        }

        ClickToCollect.createClickToCollectItem(res, transform.position);
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
        AudioManager.Instance.playMonsterDie(mergeLevel);
        //deathAnimator.enabled = true;
        Destroy(gameObject, 0.3f);
    }

}
