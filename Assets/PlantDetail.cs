using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantDetail : Singleton<PlantDetail>
{
    public TMP_Text plantName;
    public List<GameObject> oneDetailEntrys;
    public GameObject oneState;
    PlantsManager plantManager;
    public TMP_Text healthText;

    public GameObject actionUI;
    GameObject plant;
    // Start is called before the first frame update
    void Start()
    {
        plantManager = PlantsManager.Instance;
        gameObject.SetActive(false);
    }

    public void UpdateValue()
    {
        if (!plant || !gameObject.active)
        {
            return;
        }
        oneDetailEntrys[0].SetActive(false);

        oneDetailEntrys[1].SetActive(false);
        var plantButton = plant.GetComponent<PlantsButton>();
        if (plantButton)
        {
            //this is a plant button
            var helperPlant = plantButton.helperPlant;
            plantName.text = getName(helperPlant);
            UpdateStaticHealth(helperPlant);
            getOnetimeCost(helperPlant); 
            getProduction(helperPlant);
            //stats.text += getOnetimeCost(helperPlant);
            //stats.text += getDurationCost(helperPlant, true);
            //stats.text += getProduction(helperPlant);
            if (plantManager.IsPlantable(helperPlant.type))
            {

                actionUI.GetComponent<TMP_Text>().text = Dialogues.PlantFlowerInst;
            }
            else
            {

                actionUI.GetComponent<TMP_Text>().text = Dialogues.InsufficientResource;
            }
        }
        else
        {
            var maintree = plant.GetComponent<MainTree>();
            if (maintree)
            {
                plantName.text = getName(maintree);
                UpdateHealth(maintree);
                //stats.text += getDurationCost(maintree);
                //getProduction(maintree);
                getUpgrade(maintree);
                //stats.text += getProduction(maintree);
                //stats.text += getUpgrade(maintree);
                if (maintree.isAtMaxLevel())
                {
                    if (maintree.isAtMaxFlower())
                    {

                        actionUI.GetComponent<TMP_Text>().text = Dialogues.AttactBeeInst;
                    }
                    else
                    {

                        updateEntry("Cost", JsonManager.Instance.getPlant(maintree.treeInfo.flowerId).plantCost, true);
                       // getOnetimeCost(maintree.treeInfo.flowerId);
                        actionUI.GetComponent<TMP_Text>().text = Dialogues.SpawnTreeFlowerInst;
                    }
                }
                else
                {
                    if (maintree.upgradable())
                    {

                        actionUI.GetComponent<TMP_Text>().text = Dialogues.UpgradeTreeInst;
                    }
                    else
                    {

                        actionUI.GetComponent<TMP_Text>().text = Dialogues.InsufficientResource;
                    }
                }
            }
            else
            {
                //this is planted plant
                //stats.text += getOnetimeCost(helperPlant);
                var helperPlant = plant.GetComponent<HelperPlant>();
                UpdateHealth(helperPlant);
                plantName.text = getName(helperPlant);
                getProduction(helperPlant);
                //stats.text += getProduction(helperPlant);
                actionUI.GetComponent<TMP_Text>().text = Dialogues.RemovePlantResource;

            }
        }
    }

    public void updateValue(GameObject p)
    {
        plant = p;
        UpdateValue();
    }

    void UpdateHealth(HelperPlant plant)
    {
        healthText.text = "Health: "+plant.getCurrentHp() + "/" + plant.maxHP;
    }

    void UpdateStaticHealth(HelperPlant plant)
    {
        healthText.text = "Health: " + plant.maxHP;
    }

    void updateEntry(string title, List<PairInfo<int>> dict,bool checkValue = false)
    {
        if (dict == null || dict.Count == 0)
        {
            return;
        }
        var go = oneDetailEntrys[0].active ? oneDetailEntrys[1] : oneDetailEntrys[0];
        go.SetActive(true);
        var entry = go.GetComponent<PlantInfoEntry>();
        entry.title.text = title;
        int i = 0;
        foreach (var pair in dict)
        {
            entry.stats[i].gameObject.SetActive(true);
            //var goo = Instantiate(oneState, entry.statePanel);
            var goo = entry.stats[i];
            goo.image.sprite = JsonManager.Instance.getCurrency(pair.Key).sprite;
            goo.value.text = pair.Value.ToString();
            goo.value.color = Color.black;
            if (checkValue)
            {

                bool isResourceAvailable = plantManager.IsResourceAvailable(pair.Key, pair.Value);
                if (!isResourceAvailable)
                {
                    goo.value.color = Color.red;
                }
            }
            else
            {

            }
            i++;
        }
        for (; i < entry.stats.Count; i++)
        {
            entry.stats[i].gameObject.SetActive(false);
        }
    }

    void getProduction(HelperPlant plant)
    {

        var prodDictionary = JsonManager.Instance.getFlower(plant.type).produces;// plantManager.helperPlantProd[plant.type];

        updateEntry("Produce", prodDictionary);
    }
    void getUpgrade(MainTree plant)
    {
        if (plant.isAtMaxLevel())
        {
            if (!plant.isAtMaxFlower())
            {

                //updateEntry("Flower Cost", JsonManager.Instance.getPlant(plant.flowerPlantType).plantCost, true);
            }
            return;

        }
        updateEntry("Upgrade Cost", JsonManager.Instance.getPlant(plant.nextLevelType()).plantCost,true);
        //string res = "\nUpgrade Cost:\n";
        //var prodDictionary = plantManager.helperPlantCost[plant.nextLevelType()];
        //foreach (var pair in prodDictionary)
        //{
        //    bool isResourceAvailable = plantManager.IsResourceAvailable(pair.Key, pair.Value);
        //    res += isResourceAvailable ? "" : InsufficientResourcePrefix;
        //    res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
        //    res += isResourceAvailable ? "" : InsufficientResourceSurfix;
        //    res += "\n";
        //}
        ////res += "\nNext Level Keep Cost:\n";
        ////var keepCostDictionary = plantManager.helperPlantKeepCost[plant.nextLevelType()];
        //foreach (var pair in keepCostDictionary)
        //{

        //    bool isResourceAvailable = plantManager.IsResourceRateAvailable(pair.Key, pair.Value);
        //    res += isResourceAvailable ? "" : InsufficientResourceRatePrefix;
        //    res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
        //    res += isResourceAvailable ? "" : InsufficientResourceSurfix;
        //    res += "\n";
        //}
    }
    string getName(HelperPlant plant)
    {
        if (!plantManager)
        {

            plantManager = PlantsManager.Instance;
        }
        return JsonManager.Instance.getPlant(plant.type).name+"\n";
    }
    void getOnetimeCost(HelperPlant plant)
    {

        updateEntry("Cost", JsonManager.Instance.getPlant(plant.type).plantCost, true);

        //Instantiate(oneDetailEntryPrefab, transform);

        //string res = "\nOne Time Cost:\n";
        //var prodDictionary = plantManager.helperPlantCost[plant.type];
        //foreach (var pair in prodDictionary)
        //{
        //    bool isResourceAvailable = plantManager.IsResourceAvailable(pair.Key, pair.Value);
        //    res += isResourceAvailable ? "" : InsufficientResourcePrefix;
        //    res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
        //    res += isResourceAvailable ? "" : InsufficientResourceSurfix;
        //    res += "\n";
        //}
        //return res;
    }

    //string getDurationCost(HelperPlant plant,bool showInsufficient = false)
    //{
    //    string res = "\nDuration Cost\n";
    //    //var prodDictionary = plantManager.helperPlantKeepCost[plant.type];
    //    foreach (var pair in prodDictionary)
    //    {
    //        bool isResourceAvailable = plantManager.IsResourceRateAvailable(pair.Key, pair.Value);
    //        res += (!isResourceAvailable&&showInsufficient) ?  InsufficientResourceRatePrefix:"";
    //        res += plantManager.resourceName[pair.Key] + "\t" + pair.Value.ToString();
    //        res += (!isResourceAvailable && showInsufficient) ? InsufficientResourceSurfix:"";
    //        res += "\n";
    //    }
    //    return res;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
