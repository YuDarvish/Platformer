using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PowerUpSpawn : MonoBehaviour
    {
        public static PowerUpSpawn instance = null;

        int hitCount;

        float timeRate = 1f;
        float nextTime;

        static Transform spawnPosition;

        private Stack<PowerUp> superJumpStack = new Stack<PowerUp>();
        private Stack<PowerUp> speedUpStack = new Stack<PowerUp>();
        private Stack<PowerUp> extraLifeStack = new Stack<PowerUp>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            StorePowerUp("SuperJump", 10);
            StorePowerUp("SpeedUp", 10);
            StorePowerUp("ExtraLife", 10);
        }
               
        // Update is called once per frame
        void Update()
        {
            CheckTimeLimit();
        }

        public void CountingHits(Transform tokenPos)
        {
            if (hitCount == 0)
                nextTime = Time.time;
            
            if ((Time.time - nextTime) < timeRate)
            {
                hitCount++;
                nextTime = Time.time;
            }
            spawnPosition = tokenPos;
        }

        void CheckTimeLimit()
        {
            if ((Time.time - nextTime) > timeRate)
            {
                if (hitCount >= 3 && hitCount <= 5)
                {
                    SpawnSuperJump();
                    hitCount = 0;
                }
                else if (hitCount >= 6 && hitCount <= 7)
                {
                    SpawnSpeedUp();
                    hitCount = 0;
                }
                else if (hitCount >= 8)
                {
                    SpawnExtraLife();
                    hitCount = 0;
                }
                else
                {
                    hitCount = 0;
                }
            }
        }

        private void SpawnSuperJump()
        {
            PowerUp newPowerUp = superJumpStack.Pop();
            SpawnPowerUp(newPowerUp);
        }

        private void SpawnSpeedUp()
        {
            PowerUp newPowerUp = speedUpStack.Pop();
            SpawnPowerUp(newPowerUp);
        }

        private void SpawnExtraLife()
        {
            PowerUp newPowerUp = extraLifeStack.Pop();
            SpawnPowerUp(newPowerUp);
        }

        private void SpawnPowerUp(PowerUp power)
        {
            power.gameObject.SetActive(true);
            power.gameObject.transform.position = spawnPosition.position;
        }

        private void StorePowerUp(string tag, int amount)
        {
            GameObject powerUpPrefab = GameObject.FindGameObjectWithTag(tag);
            for (int i = 0; i < amount; i++)
            {
                GameObject powerUpInstan = Instantiate(powerUpPrefab, transform.position, transform.rotation);
                powerUpInstan.SetActive(false);
                PushOnStack(powerUpInstan.GetComponent<PowerUp>());
            }
        }

        internal void PushOnStack(PowerUp powerUp)
        {
            powerUp.gameObject.SetActive(false);
            if (powerUp.GetType() == typeof(PowerUpSuperJump))
            {
                superJumpStack.Push(powerUp);
            }
            else if (powerUp.GetType() == typeof(PowerUpSpeedUp))
            {
                speedUpStack.Push(powerUp);
            }
            else if(powerUp.GetType() == typeof(PowerUpExtraLife))
            {
                extraLifeStack.Push(powerUp);
            }
        }
    }
}
