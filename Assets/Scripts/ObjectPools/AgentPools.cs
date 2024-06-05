using System;
using System.Collections.Generic;
using System.Linq;
using Controller.Utilities;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class AgentPools : MonoSingleton<AgentPools>
    {
        public List<GameObject> Agents; // Agent'ların tutulduğu liste
        public int AgentCount = 0; // Dinamik olarak değişen agent sayısı
        
        /// <summary>
        /// Parametre olarak gelen işaret, sayı, ve Transform'a göre Agent'ı aktif eder.
        /// </summary>
        /// <param name="sign"> Count değerinin işareti </param>
        /// <param name="count"> Aktif edilecek agent sayısı </param>
        /// <param name="point"> Aktif edilecek agent'ların transform'u</param>
        public void AgentObjectPoolManager(char sign, int count, Transform point)
        {
            switch (sign)
            {
                case 'x':
                    AgentObjectPool((count * AgentCount) - AgentCount, point);
                    break;
                case '+':
                    AgentObjectPool(count, point);
                    break;
                case '-':
                    if (count >= AgentCount)
                        AgentObjectPool(-AgentCount, point);
                    else
                        AgentObjectPool(-count, point);
                    break;
                case '/':
                    if (count > AgentCount)
                        return;
                    if (AgentCount % count == 0)
                        AgentObjectPool(-(AgentCount - (AgentCount / count)), point);
                    else
                    {
                        decimal result = Math.Ceiling((decimal)AgentCount / (decimal)count);
                        AgentObjectPool((int)(result - AgentCount), point);
                    }

                    break;
            }
        }
        
        /// <summary>
        /// Ana karakteri agents listesine ekler.
        /// Sadece anakarakter'i eklemek için kullanılmalı. Agent eklemek için
        /// AddList() fonksiyonu kullanılmalı.
        /// </summary>
        /// <param name="character">Eklenecek olan karakter.</param>
        public void AddMainCharacter(GameObject character)
        {
            Agents.Add(character);
            AgentCount = 1;
        }
        
        /// <summary>
        /// Agents listesine yeni eleman ekler. Eklenen elemanlar son elemanın bir önceki indexine eklenir.
        /// Son eleman'ın daima ana karakter olmasını sağlar. Böylelikle anakarakter en son yok edilir.
        /// </summary>
        /// <param name="agent"></param>
        public void AddList(GameObject agent)
        {
            Agents.Insert(Agents.Count - 1, agent);
            AgentCount++;
        }
        
        // Limit değerine göre agent oluşturur veya yok eder
        private void AgentObjectPool(int limit, Transform point)
        {
            while (limit != 0)
            {
                foreach (var item in Agents)
                {
                    if ((!item.activeInHierarchy && limit > 0) || (item.activeInHierarchy && limit < 0))
                    {
                        item.transform.position = point.position;
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
                            AgentCount--;
                            AgentDeathHandler.DeathHandel(item.transform);
                            limit++;
                        }

                        break;
                    }
                }
            }
        }
    }
}