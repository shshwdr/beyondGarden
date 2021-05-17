using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using PixelCrushers.DialogueSystem;

public class BossController : HPCharacterController
{
    NavMeshAgent agent;

    [ConversationPopup] public string conversation;
    GameObject bossTargets;
    GameObject spawnPositions;
    int spawnId = -1;
    int previousId = -1;
    public bool isSpawning = false;
    public int[] maxHps;
    int maxStage = 2;
    int stage = 0;
    public bool isDashing;
    float dashTime = 1.0f;
    //float dashLogicalFinishTime = 1.5f;
    float currentDashTimer;
    BoxCollider2D solidCollider;
    public GameObject dashColliderObject;
    // Start is called before the first frame update

    public GameObject[] abilities;
    float abilitySwitchTime = 5f;
    float currentAiblitySwitchTimer = 0;
    int currentAbilityId = 0;

    public void spawnPublic()
    {
        StartCoroutine(spawn());
    }

    public void ChasePlayer()
    {
        agent.enabled = true;
        agent.isStopped = false;
        agent.SetDestination(EnemyManager.instance.player.transform.position);
        solidCollider.offset = new Vector2(0f, 0.1f);
        solidCollider.size = new Vector2(0.2f, 0.25f);
        var newEmotePosition = emotesController.gameObject.transform.localPosition;
        newEmotePosition.y = 0.3f;
        emotesController.gameObject.transform.localPosition = newEmotePosition;
        solidCollider.enabled = true;
        switchAbility();
    }

    void switchAbility()
    {
        emotesController.showEmote(EmoteType.angry);
        abilities[currentAbilityId].SetActive(false);
        currentAbilityId++;
        if (currentAbilityId >= abilities.Length)
        {
            currentAbilityId = 0;
        }
        abilities[currentAbilityId].SetActive(true);
    }

    public void spawnPublic2()
    {
        Debug.Log("spawn public 2");
        StartCoroutine(spawn2());
    }

    public void spawnPublic3()
    {
        Debug.Log("spawn public 3");
        StartCoroutine(spawn3());
    }

    public void selectNextSpawnId()
    {
        previousId = spawnId;
        spawnId += 1;//Random.Range(0, 3);
        if(spawnId == 3)
        {
            spawnId = 0;
        }
        //while (spawnId == previousId)
        //{
        //    spawnId = Random.Range(0, 3);
        //}
    }

    public void selectNextSpawnIdAndMove()
    {
        selectNextSpawnId();
        Debug.Log("previous " + previousId + " now " + spawnId);
        agent.SetDestination(bossTargets.transform.GetChild(spawnId).transform.position);
        Debug.Log("remaining after select " + agent.remainingDistance);
    }

    IEnumerator spawn()
    {
        Debug.Log("start spawn");
        isSpawning = true;
        animator.SetBool("spawn", true);
        EnemyManager.instance.spawnMinions(spawnPositions.transform.GetChild(spawnId).transform.position);
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1f);

        animator.SetBool("spawn", false);
        Debug.Log("select next");
        //selectNextSpawnIdAndMove();
        isSpawning = false;
    }
    IEnumerator spawn2()
    {
        Debug.Log("start spawn 2");
        isSpawning = true;
        isDashing = false;
        dashColliderObject.SetActive(false);
        solidCollider.enabled = true;
        animator.SetBool("spawn", true);
        selectNextSpawnId();
        EnemyManager.instance.spawnMinions(spawnPositions.transform.GetChild(spawnId).transform.position);

        yield return new WaitForSeconds(0.5f);
        emotesController.showEmote(EmoteType.confused);
        yield return new WaitForSeconds(2f);

        //yield return new WaitForSeconds(1f);

        Debug.Log("finish spawn 2");
        animator.SetBool("spawn", false);
        //Debug.Log("select next");
        //Debug.Log("spawn finished");

        //Debug.Log("remaining after finishe " + agent.remainingDistance);
        isSpawning = false;
        //dashToPlayer();
    }

    IEnumerator spawn3()
    {
        Debug.Log("start spawn 3");
        currentAiblitySwitchTimer = 0; 
        selectNextSpawnId();
        EnemyManager.instance.spawnMinions(spawnPositions.transform.GetChild(spawnId).transform.position);
        switchAbility();
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(2f);

        //yield return new WaitForSeconds(1f);

        //Debug.Log("finish spawn 2");
        //animator.SetBool("spawn", false);
        //Debug.Log("select next");
       // selectNextSpawnId();
        //Debug.Log("spawn finished");

        //Debug.Log("remaining after finishe " + agent.remainingDistance);
        //isSpawning = false;
        //dashToPlayer();
    }
    protected override void Start()
    {
        maxHp = maxHps[stage];
        base.Start();

        animator = transform.Find("test").GetComponentInChildren<Animator>();
        spriteObject = animator.transform.parent.gameObject;
        solidCollider = GetComponent<BoxCollider2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        bossTargets = GameObject.Find("bossTarget");
        spawnPositions = GameObject.Find("spawnPosition");
        EnemyManager.instance.bossController = this;
    }

    public void spawnersDie()
    {
        getDamage();
    }
    public void spawnersMerge()
    {
        emotesController.showEmote(EmoteType.happy);
        Heal();
        //show heal effect
    }

    // Update is called once per framee
    protected override void Update()
    {
        if (isDead || EnemyManager.instance.player.isDead)
        {
            //agent.isStopped = true;
            rb.velocity = Vector3.zero;
            return;
        }
        base.Update();

        //if (isDashing && rb.velocity.magnitude < 0.01f)
        //{
        //    isDashing = false;
        //    dashColliderObject.SetActive(false);

        //    solidCollider.enabled = true;
        //}
        currentDashTimer += Time.deltaTime;
        currentAiblitySwitchTimer += Time.deltaTime;
        if (stage != 1)
        {

            testFlip(agent.velocity);
        }
        else
        {
            testFlip(rb.velocity);
        }
    }
    protected override void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        agent.isStopped = true;
        isSpawning = false;
        animator.SetBool("spawn", false);
        isDashing = false;
        dashColliderObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
        rb.velocity = Vector3.zero;
        StopAllCoroutines();

        EnemyManager.instance.updateEnemies();
        if (stage != maxStage)
        {
            AudioManager.Instance.playBossDamage(stage);
            //go to next stage
            animator.SetTrigger("die");

            //DialogueManager.StartConversation(conversation, null, null);
        }
        else
        {
           // Destroy(gameObject);
            AudioManager.Instance.playBossDefeat();
            EnemyManager.instance.destoryAllEnemies();
            DialogueManager.StartConversation(conversation, null, null);

        }
    }
    

    public void dashToPlayer()
    {
        Debug.Log("dash to player");
        var playerPosition = EnemyManager.instance.player.transform.position;
        var dir = (playerPosition - transform.position);
        dir = new Vector3(dir.x, dir.y, 0);
        dir = dir.normalized;
        //int layerMask = 1 << 12;
        //layerMask = ~layerMask;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 12.0f, layerMask);
        //var targetPosition = playerPosition+dir*0.2f;
        //rb.DOMove(targetPosition, dashTime);
        emotesController.showEmote(EmoteType.angry);
        //GetComponent<BoxCollider2D>().enabled = false;
        dashColliderObject.SetActive(true);
        solidCollider.enabled = false;
        currentDashTimer = 0;
        Debug.Log("dir " + dir);
        StartCoroutine(dash(dir));
    }

    IEnumerator dash(Vector3 dir)
    {
        yield return new WaitForSeconds(0.5f);
        isDashing = true;
        currentDashTimer = 0;
        rb.AddForce(dir * 100);
    }

    public bool isDashFinished()
    {
        return currentDashTimer >= dashTime;
    }


    public bool shouldSwitchAbility()
    {
        return currentAiblitySwitchTimer >= abilitySwitchTime;
    }

    public void Heal(int healing = 1)
    {
        hp += healing;
        //GetComponentInChildren<ParticleSystem>().gameObject.SetActive(true);
        GetComponentInChildren<ParticleSystem>().Play();
        updateHP();
    }

    public void Revive()
    {

        stage++;
        maxHp = maxHps[stage];
        isDead = false;
        hp = maxHp;
        updateHP();
    }
    public bool isReallyDead()
    {
        return isDead && stage == 2;
    }
}
