using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObstacleLevelData;

public class ObstacleGenerator : MonoBehaviour
{
    //Integer to tell the game which stage the player is on. (Tutorial is named generally, but should be '0' in this case)
    private int currentScene;

    //List of Prefabs that can be instantiated by this generator. Associated with the enum list from ObstacleLevelData.
    private GameObject emptyParent;
    public GameObject tutorialObstaclePrefab;
    public GameObject shippingContainerPrefab;
    public GameObject boatPrefab;
    public GameObject pierPrefab;
    public GameObject waterPrefab;
    public GameObject buildingPrefab;
    public GameObject cratePrefab;
    public GameObject truckPrefab;
    public GameObject conveyerBeltPrefab;
    public GameObject rackPrefab;
    public GameObject boxPrefab;
    public GameObject palettePrefab;
    public GameObject devicesPrefab;
    public GameObject filingCabinetPrefab;
    public GameObject computerDeskPrefab;
    public GameObject dividerPrefab;
    public GameObject chairPrefab;
    public GameObject moneyPilePrefab;
    public GameObject safePrefab;
    public GameObject railingPrefab;

    // Start is called before the first frame update
    void Start()
    {
        emptyParent = new GameObject();
        emptyParent.name = "SceneParent";
        currentScene = 0;
        generateScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateScene()
    {
        //Takes the level the player is on (hypothetically increases infinitely)
        int generator = currentScene;
        //Reduces that level by 10 for each boss level the player has beaten (as the boss level is level 10).
        //We will repeat the layouts from stages 1-10 for ease of implementation, with a boss occurring every 10 levels, and only the stage difficulty increasing.
        //This means that the game could go on forever, but hopefully the player will eventually lose to the more difficult enemies.
        while (generator > 10)
        {
            generator -= 10;
        }
        //The layout variable will be set by the switch to select which objects to generate.
        //Remember to set the number of elements in the defined layout to the number of objects in the level with the most objects for memory reasons.
        ObstacleLevelData.ObstaclePosition[] layout = new ObstacleLevelData.ObstaclePosition[200];
        switch (generator)
        {
            //0 = Tutorial Level
            case 0:
                layout = ObstacleLevelData.GameLevels.Tutorial;
                break;
            //1 = 1st Port Level
            case 1:
                break;
            //2 = 2nd Port Level
            case 2:
                break;
            //3 = 3rd Port Level
            case 3:
                break;
            //4 = 1st Warehouse Level
            case 4:
                break;
            //5 = 2nd Warehouse Level
            case 5:
                break;
            //6 = 3rd Warehouse Level
            case 6:
                break;
            //7 = 1st Office Level
            case 7:
                break;
            //8 = 2nd Office Level
            case 8:
                break;
            //9 = 3rd Office Level
            case 9:
                break;
            //10 = Vault/Boss Level
            case 10:
                break;
        }

        //Instantiate the 'scene parent' so all prefabs can be removed later by simply removing this, instead of removing each prefab individually.
        //Also the ObstacleGenerator contains this generation code, so we can't remove this one.
        Instantiate(emptyParent, transform);

        //Instantiate each object in the layout.
        for (int i = 0; i < layout.Length; i++)
        {
            //Get the data of the current object that needs to be generated.
            ObstacleLevelData.ObstaclePosition currentobject = layout[i];
            //Prepare a variable containing the enum of the prefab to be instanced. So it can be used in switch.
            ObstacleLevelData.Obstacle prefabName = currentobject.obs;
            //Prepare a variable to store the GameObject that results from the switch.
            GameObject currentObjectPrefab = new GameObject();
            //Use the enum prefabName to determine which GameObject prefab to instance.
            switch (prefabName)
            {
                case ObstacleLevelData.Obstacle.TutorialObstacle:
                    currentObjectPrefab = tutorialObstaclePrefab;
                    break;
                case ObstacleLevelData.Obstacle.ShippingContainer:
                    currentObjectPrefab = shippingContainerPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Boat:
                    currentObjectPrefab = boatPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Pier:
                    currentObjectPrefab = pierPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Water:
                    currentObjectPrefab = waterPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Building:
                    currentObjectPrefab = buildingPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Crate:
                    currentObjectPrefab = cratePrefab;
                    break;
                case ObstacleLevelData.Obstacle.Truck:
                    currentObjectPrefab = truckPrefab;
                    break;
                case ObstacleLevelData.Obstacle.ConveyerBelt:
                    currentObjectPrefab = conveyerBeltPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Rack:
                    currentObjectPrefab = rackPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Box:
                    currentObjectPrefab = boxPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Palette:
                    currentObjectPrefab = palettePrefab;
                    break;
                case ObstacleLevelData.Obstacle.Devices:
                    currentObjectPrefab = devicesPrefab;
                    break;
                case ObstacleLevelData.Obstacle.FilingCabinet:
                    currentObjectPrefab = filingCabinetPrefab;
                    break;
                case ObstacleLevelData.Obstacle.ComputerDesk:
                    currentObjectPrefab = computerDeskPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Divider:
                    currentObjectPrefab = dividerPrefab;
                    break;
                case ObstacleLevelData.Obstacle.Chair:
                    currentObjectPrefab = chairPrefab;
                    break;
                case ObstacleLevelData.Obstacle.MoneyPile:
                    currentObjectPrefab = moneyPilePrefab;
                    break;
                case ObstacleLevelData.Obstacle.Safe:
                    currentObjectPrefab = safePrefab;
                    break;
                case ObstacleLevelData.Obstacle.Railing:
                    currentObjectPrefab = railingPrefab;
                    break;
            }
            //This GameObject keeps track of the instanced prefab so it can be scaled later.
            GameObject prefabClone = new GameObject();
            //Instantiate the prefab with position and rotation under the 'scene parent', switch for each different prefab.
            prefabClone = Instantiate(currentObjectPrefab, new Vector3(currentobject.pos_x, currentobject.pos_y, currentobject.pos_z), Quaternion.Euler(currentobject.rot_x, currentobject.rot_y, currentobject.rot_z), transform.GetChild(0));
            //Scale the prefab.
            prefabClone.transform.localScale = new Vector3(currentobject.scl_x, currentobject.scl_y, currentobject.scl_z);
        }
    }
}
