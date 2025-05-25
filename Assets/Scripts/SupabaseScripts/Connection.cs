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
                UserDb userDb = new UserDb
                {
                    nickname = AULogin.GetComponent<TextMeshProUGUI>().text,
                    password = AUPassword.GetComponent<TextMeshProUGUI>().text
                };
                var conn = await UserDb.AuthenticateUserAsync(supabase, userDb);
                if (conn)
                {
                    nameU = AULogin.GetComponent<TextMeshProUGUI>().text;
                    passwordU = AUPassword.GetComponent<TextMeshProUGUI>().text;
                }
            }
            catch (Exception e)
            {
                Debug.Log("Ой, "+ e.Message);
            }
        }
    }
}