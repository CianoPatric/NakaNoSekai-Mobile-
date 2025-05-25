using System;
using UnityEngine;
using TMPro;
using DefaultNamespace;
using Supabase.Gotrue;

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

        public GameObject ParkName;
        private string nameU;
        private string passwordU;

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
                Debug.Log("Ой, " + e.Message);
            }
        }
        public async void Connect()
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
                    nameU = AULogin.GetComponent<TextMeshProUGUI>().text;
                    passwordU = AUPassword.GetComponent<TextMeshProUGUI>().text;
                    AuthPrefab.SetActive(false);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Ой, " + e.Message);
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
                Debug.Log("Ой, " + e.Message);
            }
        }
    }
}