using System.Collections;
using System.Threading.Tasks;
using DefaultNamespace;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using Supabase;
using SupabaseScripts;
using UnityEngine;
using UnityEngine.TestTools;

public class Test1
{
    [Test]
    public async Task RegisterUser()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();

        var user = new UserDb()
        {
            nickname = "TestUser0",
            password = "123123"
        };
        var result = await UserDb.RegisterUserAsync(client, user);

        Assert.IsTrue(result);
    }
    [Test]
    public async Task DoubleRegisterUser()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();

        var user = new UserDb()
        {
            nickname = "TestUser1",
            password = "securepass"
        };
        await UserDb.RegisterUserAsync(client, user);
        var result = await UserDb.RegisterUserAsync(client, user);
        Assert.IsFalse(result);
    }
    [Test]
    public async Task AuthUser_WithCorrectCredentials()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        UserDb userDb = new()
        {
            nickname = "TestUser2",
            password = "123123"
        };
        await UserDb.RegisterUserAsync(client, userDb);

        var result = await UserDb.AuthenticateUserAsync(client, userDb);

        Assert.IsTrue(result);
    }
    [Test]
    public async Task LoginUser_WithNotCorrectCredentials()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        UserDb userDb = new()
        {
            nickname = "TestUser3",
            password = "123123"
        };
        await UserDb.RegisterUserAsync(client, userDb);
        UserDb userDbnew = new()
        {
            nickname = "TestUser31",
            password = "securepass"
        };

        var result = await UserDb.AuthenticateUserAsync(client, userDbnew);

        Assert.IsFalse(result);
    }
    [Test]
    public async Task RegisterUser_WithShortPassword()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();

        var user = new UserDb()
        {
            nickname = "TestUser4",
            password = "123"
        };
        var result = await UserDb.RegisterUserAsync(client, user);

        Assert.IsFalse(result);
    }
    [Test]
    public async Task LoadPark()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        var parks = await ParkDb.LoadParks(client);

        Assert.IsNotNull(parks);
    }

    [Test]
    public async Task FiltrationLoadPark()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        string Name = "A";
        var parks = await ParkDb.SearchParksByName(client, Name);

        Assert.IsNotNull(parks);
    }

    [Test]
    public async Task LoadParkByUserId_AfterLogin()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        var user = new UserDb()
        {
            nickname = "TestUser6",
            password = "securepass"
        };
        await UserDb.RegisterUserAsync(client, user);
        await UserDb.AuthenticateUserAsync(client, user);
        var id = await UserDb.SearchUserID(client, user);
        var get = await ParkDb.LoadParkByUserId(client, id);
        Assert.IsNotNull(get);
    }
    [Test]
    public async Task SaveParks_AfterLogin()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        var user = new UserDb()
        {
            nickname = "TestUser7",
            password = "securepass"
        };
        await UserDb.RegisterUserAsync(client, user);
        await UserDb.AuthenticateUserAsync(client, user);
        var id = await UserDb.SearchUserID(client, user);
        var park = new ParkDb()
        {
            namepark = "TestPark",
            cardlist = "-1,-1,-1,-1,-1,-1,-1,-1,1",
            parktransform = "3,3",
            idcreator = id
        };

        var parks = await ParkDb.SavePark(client, park);

        Assert.IsNotNull(parks);
    }
    [Test]
    public async Task TwiceSaveParks_AfterLogin()
    {
        var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
        await client.InitializeAsync();
        var user = new UserDb()
        {
            nickname = "TestUser8",
            password = "securepass"
        };
        await UserDb.RegisterUserAsync(client, user);
        await UserDb.AuthenticateUserAsync(client, user);
        var id = await UserDb.SearchUserID(client, user);
        var park = new ParkDb()
        {
            namepark = "TestPark",
            cardlist = "-1,-1,-1,-1,-1,-1,-1,-1,1",
            parktransform = "3,3",
            idcreator = id
        };
        var park1 = new ParkDb()
        {
            namepark = "TestPark",
            cardlist = "-1,-1,-1,-1,-1,-1,-1,1,1",
            parktransform = "4,4",
            idcreator = id
        };

        await ParkDb.SavePark(client, park);
        var parks = await ParkDb.SavePark(client, park1);

        Assert.IsNotNull(parks);
    }
    [Test]
public async Task RegisterUser_1()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();

    var user = new UserDb()
    {
        nickname = "TestUser51",
        password = "123123"
    };
    var result = await UserDb.RegisterUserAsync(client, user);

    Assert.IsTrue(result);
}

[Test]
public async Task RegisterUser_2()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();

    var user = new UserDb()
    {
        nickname = "AnotherUser",
        password = "pass456"
    };
    var result = await UserDb.RegisterUserAsync(client, user);

    Assert.IsTrue(result);
}

[Test]
public async Task RegisterUser_3()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();

    var user = new UserDb()
    {
        nickname = "UniqueNick",
        password = "pass789"
    };
    var result = await UserDb.RegisterUserAsync(client, user);

    Assert.IsTrue(result);
}

[Test]
public async Task DoubleRegisterUser_1()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();

    var user = new UserDb()
    {
        nickname = "TestUser1",
        password = "securepass"
    };
    await UserDb.RegisterUserAsync(client, user);
    var result = await UserDb.RegisterUserAsync(client, user);
    Assert.IsFalse(result);
}

[Test]
public async Task DoubleRegisterUser_2()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();

    var user = new UserDb()
    {
        nickname = "RepeatNick",
        password = "repeat123"
    };
    await UserDb.RegisterUserAsync(client, user);
    var result = await UserDb.RegisterUserAsync(client, user);
    Assert.IsFalse(result);
}

[Test]
public async Task DoubleRegisterUser_3()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();

    var user = new UserDb()
    {
        nickname = "Duplicated",
        password = "dup789"
    };
    await UserDb.RegisterUserAsync(client, user);
    var result = await UserDb.RegisterUserAsync(client, user);
    Assert.IsFalse(result);
}

[Test]
public async Task AuthUser_WithCorrectCredentials_1()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();
    UserDb userDb = new()
    {
        nickname = "TestUser2",
        password = "123123"
    };
    await UserDb.RegisterUserAsync(client, userDb);

    var result = await UserDb.AuthenticateUserAsync(client, userDb);

    Assert.IsTrue(result);
}

[Test]
public async Task AuthUser_WithCorrectCredentials_2()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();
    UserDb userDb = new()
    {
        nickname = "LoginSuccess",
        password = "mypassword"
    };
    await UserDb.RegisterUserAsync(client, userDb);

    var result = await UserDb.AuthenticateUserAsync(client, userDb);

    Assert.IsTrue(result);
}

[Test]
public async Task AuthUser_WithCorrectCredentials_3()
{
    var client = new Supabase.Client(ClientInfo.CLIENT_URL, ClientInfo.CLIENT_KEY);
    await client.InitializeAsync();
    UserDb userDb = new()
    {
        nickname = "ValidUser",
        password = "passpass"
    };
    await UserDb.RegisterUserAsync(client, userDb);

    var result = await UserDb.AuthenticateUserAsync(client, userDb);

    Assert.IsTrue(result);
}
}
