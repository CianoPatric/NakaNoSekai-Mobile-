using UnityEngine;
using Supabase;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Threading.Tasks;
using System;

namespace SupabaseScripts
{
[Table("Users")]
public class UserDb : BaseModel
{
    [PrimaryKey("UserID")]
    public int userid { get; }

    [Column("NickName")]
    public string nickname { get; set; }

    [Column("Password")]
    public string password { get; set; }

    public UserDb() { }

    public UserDb(string NickName, string Password)
    {
        nickname = NickName;
        password = Password;
    }

    public static async Task<int> SearchUserID(Client client, UserDb userDb)
    {
        try
        {
            var ID = await client
                .From<UserDb>()
                .Select("UserID")
                .Filter("NickName", Supabase.Postgrest.Constants.Operator.Equals, userDb.nickname)
                .Filter("Password", Supabase.Postgrest.Constants.Operator.Equals, userDb.password)
                .Single();
            return ID.userid;
        }
        catch (Exception ex)
        {
            Debug.Log("Ошибка: " + ex.Message);
            return -1;
        }
    }
    public static async Task<bool> AuthenticateUserAsync(Client client, UserDb userA)
    {
        try
        {
            var user = await client
                .From<UserDb>()
                .Select("*")
                .Filter("NickName", Supabase.Postgrest.Constants.Operator.Equals, userA.nickname)
                .Filter("Password", Supabase.Postgrest.Constants.Operator.Equals, userA.password)
                .Single();

            if (user == null)
            {
                Debug.Log($"Ошибка: проверьте правильность указания логина и/или пароля");
                return false;
            }
            Debug.Log($"Вход произведён");
            return true;
        }
        catch (Exception ex)
        {
            Debug.Log($"Ошибка: {ex.Message}");
            return false;
        }
    }
    public static async Task<bool> RegisterUserAsync(Client client, UserDb user)
    {
        if(user.nickname != null && user.nickname != "")
        {
            if(user.password != null && user.password.Length >= 6 && user.password != "")
            {
                try
                {
                    var response = await client.From<UserDb>().Insert(user);

                    if (response != null && response.Models.Count > 0)
                    {
                        Debug.Log("Успешна регистрация нового пользователя");
                        return true;
                    }
                    else
                    {
                        Debug.Log("Ошибка подключения");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log("Ошибка: " + ex.Message);
                    return false;
                }
            }
            else
            {
                Debug.Log("Ошибка: Пароль должен создержать как минимум 6 символов");
                return false;
            }
        }
        else
        {
            Debug.Log("Ошибка: Логин не должен быть пустым");
            return false;
        }

    }
}
}