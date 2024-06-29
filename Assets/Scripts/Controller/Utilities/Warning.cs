using System.Collections;
using MonoSingleton;
using ObjectPools;
using TMPro;
using UnityEngine;
using Utilities.SaveLoad;
using Random = UnityEngine.Random;

namespace Controller.Utilities
{
    public class Warning : MonoSingleton<Warning>
    {
        private float _maxTime; // Hesaplamanın maximum süresi
        private float _currentTime; // Çarpışmadan sonraki anlık süre
        private bool _isStaying; // Çarpışmanın devam edip etmediğinin kontrolü

        private Coroutine _calculateCoroutine; // Çarpışma süresini hesaplayan coroutine

        [SerializeField]private TextMeshProUGUI _warningText; // Uyarı mesajının yazılacağı text
        [SerializeField]private GameObject _warningPanel; // Uyarı mesajının paneli
        private int _isFirst; // Uyarıların sıralamasını tutan değer
        
        private string[] _warningMessages; // Uyarı mesajlarını tutan array

        public static bool CharacterCanMove; // Karakterin hareket edip etmeyeceğini belirleyen değişken

        #region Awake, Set Functions

        private void Awake()
        {
            SetReferences();
            SetFirstMessages();
        }
        
        // Değişkenlerin default değerlerini ayarlar
        private void SetReferences()
        {
            _maxTime = 2f;
            _currentTime = 0f;
            _isStaying = false;
            _isFirst = PlayerPrefsData.GetInt("IsFirst");
            CharacterCanMove = true;
        }
        #endregion

        #region Set Warning Messages

        // İlk uyarı mesajlarını belirliyor 
        private void SetFirstMessages()
        {
            _warningMessages = new string[4];
            _warningMessages[0] = "No no no... Bence hareket etsen iyi olur :)";
            _warningMessages[1] = "Yoruldu isen oyunu kapatarak dinlenebilirsin bence.";
            _warningMessages[2] = "Şimdilik birşey yapmıyorum bir daha yaparsan sürprizim var sanaa :))";
            _warningMessages[3] = "Kontrollerini geri veriyorum bir daha görmeyeyim";
        }
        
        // İkinci uyarı mesajlarını belirliyor
        private void SetSecondWarningMessages()
        {
            _warningMessages = new string[5];
            _warningMessages[0] = "Ama ben sana bir daha yapma demedim mi?";
            _warningMessages[1] = "Beni hiç dinlemiyorsun ki.";
            _warningMessages[2] = "Bundan sonra uyarmayacağım seni.";
            _warningMessages[3] = "Canımın istediği kadar karakterini yok edeceğim";
            _warningMessages[4] = "Nasıl mı? Tam da böyle. hahahahahaha";
        }

        #endregion

        #region OnCollisionEnter, OnCollisionStay

        private void OnCollisionStay(Collision other)
        {
            if ((other.gameObject.CompareTag("ThornBox") || other.gameObject.CompareTag("Saw") ||
                 other.gameObject.CompareTag("ThornWall") || other.gameObject.CompareTag("Hammer") ||
                 other.gameObject.CompareTag("Column")) && !_isStaying)
            {
                _isStaying = true;
                if (_isFirst == 2)
                    _calculateCoroutine = StartCoroutine(CalculateCollisionTime());
                else if (_isFirst == 1)
                {
                    SetSecondWarningMessages();
                    StartCoroutine(WriteWarning(_warningMessages));
                }
                else if (_isFirst == 0)
                    StartCoroutine(WriteWarning(_warningMessages));
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("ThornBox") || other.gameObject.CompareTag("Saw") ||
                other.gameObject.CompareTag("ThornWall") || other.gameObject.CompareTag("Hammer") ||
                other.gameObject.CompareTag("Column"))
            {
                if (_isFirst == 2 && _calculateCoroutine is not null)
                    StopCoroutine(_calculateCoroutine);
                _isStaying = false;
                _currentTime = 0f;
            }
        }

        #endregion
        
        // Çarpışma başladığında geçen süreyi hesaplar
        private IEnumerator CalculateCollisionTime()
        {
            while (_currentTime < _maxTime)
            {
                yield return new WaitForSeconds(1f);
                _currentTime += 1f;
            }

            if (_currentTime >= _maxTime)
            {
                DestroyAgent();
                CheckWar.CheckWarResult();
            }

            _currentTime = 0f;
            _isStaying = false;
        }
        
        // Uyarı mesajını ekrana yazdırıyor
        private IEnumerator WriteWarning(string[] messages)
        {
            // Saniye başı kontrol edilmesi gerekiyor (Daha düşük de olabilir). Süre üzerinden kontrol
            // edildiği zaman thread'ler bir nevi çakışıyor hatalı kontrol yapılabiliyor.
            for (int i = 0; i < 3; i++)
            {
                if (!_isStaying) yield break;
                yield return new WaitForSeconds(1f);
            }
            // Oyun ömrü boyunca sadece 2 kere çalışacak bir fonksiyon. GetComponent kullanımı bu yüzden pek sıkıntı olmaz.
            CharacterControl.Instance.GetComponent<Animator>().applyRootMotion = false;
            CharacterCanMove = false;
            SetWarningPanel(true);
            for (int i = 0; i < messages.Length; i++)
            {
                StartCoroutine(ShowText(messages[i]));
                yield return new WaitForSeconds(messages[i].ToCharArray().Length * .08f + .9f);
            }
            SetWarningPanel(false);
            SaveFirstStatus();
            if (_isFirst == 2)
                DestroyAgent();
            CharacterControl.Instance.GetComponent<Animator>().applyRootMotion = true;
            CharacterCanMove = true;
        }

        // Uyarı mesajını karakter karakter ekrana yazdırır
        private IEnumerator ShowText(string text)
        {
            _warningText.text = "";
            char[] character = text.ToCharArray();
            for (int i = 0; i < character.Length; i++)
            {
                _warningText.text += character[i];
                yield return new WaitForSeconds(.08f);
            }
        }
        
        // IsFirst değişkenini kaydeder
        private void SaveFirstStatus()
        {
            PlayerPrefsData.SetInt("IsFirst", ++_isFirst);
        }

        // Uyarı panelinin aktiflik, pasifliğini ayarlar
        private void SetWarningPanel(bool state)
        {
            _warningPanel.SetActive(state);
        }
        
        // Random belirlenen limit miktarı kadar agent'ı yok ediyor.
        private void DestroyAgent()
        {
            int agentCount = AgentPools.Instance.AgentCount;
            int limit = Random.Range(1, agentCount + 1);

            for (int i = 0; i < limit && agentCount > 0; i++)
            {
                AgentDeathHandler.DeathHandel(agentCount > 1 ? AgentController.GetActiveAgent() : transform);
                agentCount--;
            }
            AgentPools.Instance.AgentCount = agentCount;
        }
    }
}