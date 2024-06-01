using System;
using System.Collections.Generic;
using Controller.Utilities;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class AgentPools : MonoSingleton<AgentPools>
    {
        public List<GameObject> Agents; // Agent'ların tutulduğu liste

        public int AgentCount = 0; // Dinamik olarak değişen agent sayısı
        
        // Number panellerine anakarakterin çarpması durumunda ilgili işaret ve değere göre
        // agent oluşturur.
        public void AgentObjectPoolManager(char sign, int count, Transform spawnPoint)
        {
            switch (sign)
            {
                case 'x':
                    AgentObjectPool((count * AgentCount) - AgentCount, spawnPoint);
                    break;
                case '+':
                    AgentObjectPool(count, spawnPoint);
                    break;
                case '-':
                    if (count >= AgentCount)
                        AgentObjectPool(-AgentCount, spawnPoint);
                    else
                        AgentObjectPool(-count, spawnPoint);
                    break;
                case '/':
                    if (count > AgentCount)
                        return;
                    if (AgentCount % count == 0)
                        AgentObjectPool(-(AgentCount - (AgentCount / count)), spawnPoint);
                    else
                    {
                        decimal result = Math.Ceiling((decimal)AgentCount / (decimal)count);
                        AgentObjectPool((int)(result - AgentCount), spawnPoint);
                    }

                    break;
            }
        }
        
        // Ana karakteri agents listesine ekler.
        // Oyun başlar başlamaz CharacterController tarafından çağrılır.
        public void AddMainCharacter(GameObject character)
        {
            Agents.Add(character);
            AgentCount = 1;
        }
        
        // Agents listesine yeni eleman ekler.
        public void AddList(GameObject agent)
        {
            Agents.Insert(Agents.Count - 1, agent);
            AgentCount++;
        }
        
        // Limit değerine göre agent oluşturur veya yok eder
        private void AgentObjectPool(int limit, Transform spawnPoint)
        {
            while (limit != 0)
            {
                foreach (var item in Agents)
                {
                    if ((!item.activeInHierarchy && limit > 0) || (item.activeInHierarchy && limit < 0))
                    {
                        item.transform.position = spawnPoint.position;
                        if (limit > 0)
                        {
                            item.SetActive(true);
                            ParticleEffectPool.Instance.SpawnEffectPool(item.transform);
                            DeathStainPool.Instance.DeathStainObjectPool(false, item.transform);
                            AgentCount++;
                            limit--;
                        }
                        else if (limit < 0)
                        {
                            AgentDeathHandler.DeathHandel(item.transform);
                            AgentCount--;
                            limit++;
                        }

                        break;
                    }
                }
            }
        }
    }
}