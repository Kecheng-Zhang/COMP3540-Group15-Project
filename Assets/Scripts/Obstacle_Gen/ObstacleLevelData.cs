using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObstacleLevelData
{
    //Details to instance an object in the Level.
    //ObstaclePositions keep track of an individual Obstacle's position, rotation, scaling and which prefab is used.
    //The ObstacleGenerator script will use this information to place an obstacle into the scene.
    public class ObstaclePosition
    {
        //Position of the Obstacle Prefab
        public float pos_x;
        public float pos_y;
        public float pos_z; 

        //Rotation of the Obstacle Prefab
        public float rot_x;
        public float rot_y;
        public float rot_z; 
        
        //Scaling of the Obstacle Prefab
        public float scl_x;
        public float scl_y;
        public float scl_z;

        //Which Obstacle Prefab this is
        public Obstacle obs;

        public ObstaclePosition(float obstaclePositionX, float obstaclePositionY, float obstaclePositionZ,
                                float obstacleRotationX, float obstacleRotationY, float obstacleRotationZ,
                                float obstacleScalingX,  float obstacleScalingY,  float obstacleScalingZ,
                                Obstacle obstacle)
        {
            this.pos_x = obstaclePositionX;
            this.pos_y = obstaclePositionY;
            this.pos_z = obstaclePositionZ;
            this.rot_x = obstacleRotationX;
            this.rot_y = obstacleRotationY;
            this.rot_z = obstacleRotationZ;
            this.scl_x = obstacleScalingX;
            this.scl_y = obstacleScalingY;
            this.scl_z = obstacleScalingZ;
            this.obs   = obstacle;
        }
    }

    //Enum List of GameObjects (prefabs) that the game can instantiate.
    public enum Obstacle
    {
        //Obstacles for Tutorial
        TutorialObstacle,
        //Example obstacles for 'Port' Levels
        ShippingContainer, Boat, Pier, Water, Building, Crate,
        //Example obstacles for 'Warehouse' Levels
        Truck, ConveyerBelt, Rack, Box, Palette,
        //Example obstacles for 'Office' Levels
        Devices, FilingCabinet, ComputerDesk, Divider, Chair,
        //Example obstacles for 'Vault' Level
        MoneyPile, Safe, Railing
    }

    public class GameLevels
    {
        //Positions of all obstacles for the Tutorial Stage (Sorted by Scaling, X position, then Z position)
        public static ObstaclePosition[] Tutorial = new ObstaclePosition[]
        {
        new ObstaclePosition(-20f,     1.5f,  -20f,    0f,  0f,  0f, 4f,   4f,   4f,   Obstacle.TutorialObstacle), new ObstaclePosition(-20f,     1.5f,   20f,    0f,  0f,  0f, 4f,   4f,   4f,   Obstacle.TutorialObstacle),
        new ObstaclePosition( 20f,     1.5f,  -20f,    0f,  0f,  0f, 4f,   4f,   4f,   Obstacle.TutorialObstacle), new ObstaclePosition( 20f,     1.5f,   20f,    0f,  0f,  0f, 4f,   4f,   4f,   Obstacle.TutorialObstacle),
        new ObstaclePosition(-20f,     0.25f, -17.25f, 0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-20f,     0.25f,  17.25f, 0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-17.25f,  0.25f, -20f,    0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-17.25f,  0.25f,  20f,    0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 17.25f,  0.25f, -20f,    0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 17.25f,  0.25f,  20f,    0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 20f,     0.25f, -17.25f, 0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 20f,     0.25f,  17.25f, 0f,  0f,  0f, 1.5f, 1.5f, 1.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-12f,     0f,    -12f,    0f, -45f, 0f, 1f,   1f,   5f,   Obstacle.TutorialObstacle), new ObstaclePosition(-12f,     0f,     12f,    0f,  45f, 0f, 1f,   1f,   5f,   Obstacle.TutorialObstacle),
        new ObstaclePosition( 12f,     0f,    -12f,    0f,  45f, 0f, 1f,   1f,   5f,   Obstacle.TutorialObstacle), new ObstaclePosition( 12f,     0f,     12f,    0f, -45f, 0f, 1f,   1f,   5f,   Obstacle.TutorialObstacle),
        new ObstaclePosition(-16f,     0f,     0f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition(-7f,      0f,    -7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition(-7f,      0f,    -6f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition(-7f,      0f,     6f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition(-7f,      0f,     7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition(-6f,      0f,    -7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition(-6f,      0f,     7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition( 0f,      0f,    -16f,    0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition( 0f,      0f,     16f,    0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition( 6f,      0f,    -7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition( 6f,      0f,     7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition( 7f,      0f,    -7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition( 7f,      0f,    -6f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition( 7f,      0f,     6f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition( 7f,      0f,     7f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle), new ObstaclePosition( 16f,     0f,     0f,     0f,  0f,  0f, 1f,   1f,   1f,   Obstacle.TutorialObstacle),
        new ObstaclePosition(-21f,    -0.25f, -17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-21f,    -0.25f,  17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-20f,     1.25f, -17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-20f,    -0.25f, -16.25f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-20f,    -0.25f,  16.25f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-20f,     1.25f,  17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-19f,    -0.25f, -17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-19f,    -0.25f,  17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-17.75f, -0.25f, -21f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-17.75f,  1.25f, -20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-17.75f, -0.25f, -19f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-17.75f, -0.25f,  19f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-17.75f,  1.25f,  20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-17.75f, -0.25f,  21f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition(-16.25f, -0.25f, -20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition(-16.25f, -0.25f,  20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 16.25f, -0.25f, -20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 16.25f, -0.25f,  20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 17.75f, -0.25f, -21f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 17.75f,  1.25f, -20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 17.75f, -0.25f, -19f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 17.75f, -0.25f,  19f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 17.75f,  1.25f,  20f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 17.75f, -0.25f,  21f,    0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 19f,    -0.25f, -17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 19f,    -0.25f,  17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 20f,     1.25f, -17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 20f,    -0.25f, -16.25f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 20f,    -0.25f,  16.25f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 20f,     1.25f,  17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle),
        new ObstaclePosition( 21f,    -0.25f, -17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle), new ObstaclePosition( 21f,    -0.25f,  17.75f, 0f,  0f,  0f, 0.5f, 0.5f, 0.5f, Obstacle.TutorialObstacle)
        };
    }
}
