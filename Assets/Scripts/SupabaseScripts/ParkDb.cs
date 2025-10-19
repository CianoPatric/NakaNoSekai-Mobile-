using System;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using UnityEngine;

namespace SupabaseScripts
{
[Table("Parks")]
public class ParkDb : BaseModel
{
    [PrimaryKey("IDPark")]
    public int idpark { get; }

    [Column("NamePark")]
    public string namepark { get; set; }

    [Column("CardList")]
    public string cardlist { get; set; }

    [Column("ParkTransform")]
    public string parktransform { get; set; }

    [Column("IDCreator")]
    public int idcreator { get; set; }

    public ParkDb() { }

    public ParkDb(string Namepark, string Cardlist, string Parktransform, int IdCreator)
    {
        namepark = Namepark;
        cardlist = Cardlist;
        parktransform = Parktransform;
        idcreator = IdCreator;
    }

    public static async Task<bool> SavePark(Supabase.Client client, ParkDb park)
    {
        try
        {
            var responce = await client.From<ParkDb>().Insert(park);
            if(responce != null && responce.Models.Count > 0)
            {
                Debug.Log("Парк успешно сохранён");
                return true;
            }
            Debug.Log("Возникла ошибка");
            return false;
        }
        catch (Exception ex)
        {
            Debug.Log("Ошибка2: " + ex.Message);
            return false;
        }
    }
    public static async Task<Supabase.Postgrest.Responses.ModeledResponse<ParkDb>> LoadParks(Supabase.Client client)
    {
        try
        {
            var park = await client
                .From<ParkDb>()
                .Select("NamePark, IDCreator")
                .Get();
            if(park != null)
            {
                Debug.Log("Парк был найден");
                return park;
            }

            return null;
        }
        catch(Exception ex)
        {
            Debug.Log("" + ex);
            return null;
        }
    }

    public static async Task<Supabase.Postgrest.Responses.ModeledResponse<ParkDb>> SearchParksByName(Supabase.Client client, string name)
    {
        try
        {
            var park = await client
                .From<ParkDb>()
                .Select("NamePark")
                .Filter("NamePark", Supabase.Postgrest.Constants.Operator.Equals, name)
                .Get();
            if (park != null)
            {
                return park;
            }

            return null;
        }
        catch (Exception ex)
        {
            Debug.Log("" + ex);
            return null;
        }
    }
    public static async Task<Supabase.Postgrest.Responses.ModeledResponse<ParkDb>> LoadParkByUserId(Supabase.Client client, int id)
    {
        try
        {
            var park = await client
                .From<ParkDb>()
                .Select("NamePark")
                .Filter("IDCreator", Supabase.Postgrest.Constants.Operator.Equals, id)
                .Get();
            if(park != null)
            {
                Debug.Log("Парк был найден");
                return park;
            }

            return null;
        }
        catch(Exception ex)
        {
            Debug.Log("" + ex);
            return null;
        }
    }

}
}