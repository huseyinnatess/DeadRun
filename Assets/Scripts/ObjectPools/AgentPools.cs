using System;
using System.Collections.Generic;
using Controller.Utilities;
using Manager.Audio.Utilities;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class AgentPools : MonoSingleton<AgentPools>
    {
        public List<GameObject> Agents;
        public int AgentCount = 0;
        private FxSounds _fxSounds;

        #region Start

        private void Start()
        {
            _fxSounds = FxSounds.Instance;
        }

        #endregion


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


        public void AddMainCharacter(GameObject character)
        {
            Agents.Add(character);
            AgentCount = 1;
        }


        public void AddList(GameObject agent)
        {
            Agents.Insert(Agents.Count - 1, agent);
            AgentCount++;
        }

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
                            StartCoroutine(_fxSounds.RepeatedSound(_fxSounds.SpawnAgentFx, _fxSounds.SpawnAgentClip,
                                limit));
                            limit--;
                        }
                        else if (limit < 0)
                        {
                            AgentCount--;
                            AgentDeathHandler.DeathHandel(item.transform);
                            StartCoroutine(_fxSounds.RepeatedSound(_fxSounds.DeadAgentFx, _fxSounds.DeadAgentClip,
                                limit));
                            limit++;
                        }

                        break;
                    }
                }
            }
        }
    }
}