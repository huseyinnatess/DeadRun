using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class AgentPools : MonoSingleton<AgentPools>
    {
        public List<GameObject> agents;

        public static int CharacterCount = 0;

        public void AgentObjectPoolManager(char sign, int count, Transform spawnPoint)
        {
            switch (sign)
            {
                case 'x':
                    AgentObjectPool((count * CharacterCount) - CharacterCount, spawnPoint);
                    break;
                case '+':
                    AgentObjectPool(count, spawnPoint);
                    break;
                case '-':
                    if (count >= CharacterCount)
                        AgentObjectPool(-CharacterCount, spawnPoint);
                    else
                        AgentObjectPool(-count, spawnPoint);
                    break;
                case '/':
                    if (count > CharacterCount)
                        return;
                    if (CharacterCount % count == 0)
                        AgentObjectPool(-(CharacterCount - (CharacterCount / count)), spawnPoint);
                    else
                    {
                        decimal result = Math.Ceiling((decimal)CharacterCount / (decimal)count);
                        AgentObjectPool((int)(result - CharacterCount), spawnPoint);
                    }

                    break;
            }
        }

        public void AddMainCharacter(GameObject character)
        {
            agents.Add(character);
            CharacterCount++;
        }

        public void AddList(GameObject agent)
        {
            agents.Insert(agents.Count - 1, agent);
            CharacterCount++;
        }

        private void AgentObjectPool(int limit, Transform spawnPoint)
        {
            while (limit != 0)
            {
                foreach (var item in agents)
                {
                    if ((!item.activeInHierarchy && limit > 0) || (item.activeInHierarchy && limit < 0))
                    {
                        item.transform.position = spawnPoint.position;
                        if (limit > 0)
                        {
                            item.SetActive(true);
                            ParticleEffectPool.Instance.SpawnEffectPool(item.transform);
                            DeathStainPool.Instance.DeathStainObjectPool(false, item.transform);
                            CharacterCount++;
                            limit--;
                        }
                        else if (limit < 0)
                        {
                            item.SetActive(false);
                            ParticleEffectPool.Instance.DeadEffectPool(item.transform);
                            DeathStainPool.Instance.DeathStainObjectPool(true, item.transform);
                            CharacterCount--;
                            limit++;
                        }

                        break;
                    }
                }
            }
        }
    }
}