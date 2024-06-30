using System.Collections;
using Controller.AgentsController;
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
        
        [SerializeField]private TextMeshProUGUI informationText; // Uyarı mesajının yazılacağı text
        [SerializeField]private GameObject informationPanel; // Uyarı mesajının paneli
        private int _isFirst; // Uyarıların sıralamasını tutan değer
        
        private string[] _informationMessages; // Uyarı mesajlarını tutan array

        public static bool CharacterCanMove; // Karakterin hareket edip etmeyeceğini belirleyen değişken

        #region Awake, Set Functions

        private void Awake()
        {
            SetReferences();
            SetFirstWarningMessages();
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
        
        // Uyarı mesajlarını başlatır.
        public void StartWriteInformation(bool speacialCase = default)
        {
            StartCoroutine(WriteInformation(speacialCase));
        }
        
        #region Set Information Messages

        // İlk uyarı mesajlarını belirliyor 
        public void SetComingSoonMessages()
        {
            _informationMessages = new string[4];
            _informationMessages[0] = "Oyunu bu aşamaya kadar oynadığın için teşekkür ederim.";
            _informationMessages[1] = "Bu aşamadan sonra yeni levellar maalesef henüz yok.";
            _informationMessages[2] = "Yeni levellar için yakında güncelleme gelecek.";
            _informationMessages[3] = "Beklemede kalınnnnn :))";
        }
        private void SetFirstWarningMessages()
        {
            _informationMessages = new string[4];
            _informationMessages[0] = "No no no... Bence hareket etsen iyi olur :)";
            _informationMessages[1] = "Yoruldu isen oyunu kapatarak dinlenebilirsin bence.";
            _informationMessages[2] = "Şimdilik birşey yapmıyorum bir daha yaparsan sürprizim var sanaa :))";
            _informationMessages[3] = "Kontrolü geri veriyorum bir daha görmeyeyim.";
        }
        
        // İkinci uyarı mesajlarını belirliyor
        private void SetSecondWarningMessages()
        {
            _informationMessages = new string[5];
            _informationMessages[0] = "Ama ben sana bir daha yapma demedim mi?";
            _informationMessages[1] = "Beni hiç dinlemiyorsun ki.";
            _informationMessages[2] = "Bundan sonra uyarmayacağım seni.";
            _informationMessages[3] = "Canımın istediği kadar karakterini yok edeceğim";
            _informationMessages[4] = "Nasıl mı? Tam da böyle. hahahahahaha";
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
                    StartCoroutine(WriteInformation());
                }
                else if (_isFirst == 0)
                    StartCoroutine(WriteInformation());
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
        private IEnumerator WriteInformation(bool specialCase = default)
        {
            // Saniye başı kontrol edilmesi gerekiyor (Daha düşük de olabilir). Süre üzerinden kontrol
            // edildiği zaman thread'ler bir nevi çakışıyor hatalı kontrol yapılabiliyor.
            if (specialCase == default)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!_isStaying) yield break;
                    yield return new WaitForSeconds(1f);
                }
            }
            // Oyun ömrü boyunca sadece 2 kere çalışacak bir fonksiyon. GetComponent kullanımı bu yüzden pek sıkıntı olmaz.
            CharacterControl.Instance.GetComponent<Animator>().applyRootMotion = false;
            CharacterCanMove = false;
            SetWarningPanel(true);
            for (int i = 0; i < _informationMessages.Length; i++)
            {
                StartCoroutine(ShowText(_informationMessages[i]));
                yield return new WaitForSeconds(_informationMessages[i].ToCharArray().Length * .09f + 1.5f);
            }
            SetWarningPanel(false);
            if (_isFirst == 2 && specialCase == default)
            {
                DestroyAgent();
                SaveFirstStatus();
            }
            CharacterControl.Instance.GetComponent<Animator>().applyRootMotion = true;
            CharacterCanMove = true;
        }

        // Uyarı mesajını karakter karakter ekrana yazdırır
        private IEnumerator ShowText(string text)
        {
            informationText.text = "";
            char[] character = text.ToCharArray();
            for (int i = 0; i < character.Length; i++)
            {
                informationText.text += character[i];
                yield return new WaitForSeconds(.09f);
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
            informationPanel.SetActive(state);
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