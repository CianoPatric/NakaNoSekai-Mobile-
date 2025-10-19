using System;
using UnityEngine;
using TMPro;
using DefaultNamespace;
using Game.UI;

namespace SupabaseScripts
{
    public class Connection: MonoBehaviour
    {
        public static string url = ClientInfo.CLIENT_URL;
        public static string key = ClientInfo.CLIENT_KEY;
        
        static Supabase.SupabaseOptions options = new()
        {
            AutoConnectRealtime = true
        };

        Supabase.Client supabase = new(url, key, options);
        
        public GameObject AULogin;
        public GameObject AUPassword;
        
        public GameObject RELogin;
        public GameObject REPassword;
        public GameObject REPasswordRepit;
        
        public GameObject AuthPrefab;
        public GameObject RegPrefab;

        public GameObject CardPrefab;
        public RectTransform ContextView;

        private string ParkN;
        private int IDC;

        public async void Awake()
        {
            try
            {
                await supabase.InitializeAsync();
            }
            catch (Exception e)
            {
                Debug.Log("" + e.Message);
            }
        }
        public async void Auth()
        {
            try
            {
                UserDb userDb = new()
                {
                    nickname = AULogin.GetComponent<TextMeshProUGUI>().text,
                    password = AUPassword.GetComponent<TextMeshProUGUI>().text
                };
                var conn = await UserDb.AuthenticateUserAsync(supabase, userDb);
                if (conn)
                {
                    AuthPrefab.SetActive(false);
                }
            }
            catch (Exception e)
            {
                Debug.Log("" + e.Message);
            }
        }
        public async void Registr()
        {
            try
            {
                var RepitPass = REPasswordRepit.GetComponent<TextMeshProUGUI>().text;
                var Pass = REPassword.GetComponent<TextMeshProUGUI>().text;
                if(RepitPass == Pass)
                {
                    UserDb userDb = new()
                    {
                        nickname = RELogin.GetComponent<TextMeshProUGUI>().text,
                        password = Pass
                    };
                    var reg = await UserDb.RegisterUserAsync(supabase, userDb);
                    if(reg)
                    {
                        await UserDb.AuthenticateUserAsync(supabase, userDb);
                        RegPrefab.SetActive(false);
                    }
                }
                else
                {
                    Debug.Log("Проверьте соответсвия пароля и его подтверждения");
                }
            }
            catch (Exception e)
            {
                Debug.Log("" + e.Message);
            }
        }
        
        public async void LoadParks()
        {
            try
            {
                var parks = await ParkDb.LoadParks(supabase);
                foreach (var park in parks.Models)
                {
                    var card = Instantiate(CardPrefab, ContextView);
                    Debug.Log("" + park.idcreator);
                    var cardView = card.GetComponent<ParkCardView>();
                    var nickname = await UserDb.SearchNicknameUser(supabase, park.idcreator);
                    cardView.SetData(null, park.namepark, nickname);
                    Debug.Log($"{await UserDb.SearchNicknameUser(supabase, park.idcreator)}, {park.namepark}");
                }
            }
            catch (Exception e)
            {
                Debug.Log("" + e.Message);
            }
        }
    }
}