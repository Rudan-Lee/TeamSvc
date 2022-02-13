using System.Reflection;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Tests.Integration;

public class SimpleIntegrationTests
{
    private readonly TestServer testServer;
    private readonly HttpClient testClient;

    private readonly Team teamZombie;

    
    public SimpleIntegrationTests()
    {
        testServer = new TestServer(new WebHostBuilder()
        .UseStartup<Startup>());

        testClient = testServer.CreateClient();

        teamZombie = new Team(){
            ID = Guid.NewGuid(),
            Name = "Zombie"
        };
    }

    [Fact]
    public async void TestTeamPostAndGet(){
        StringContent stringContent = new StringContent(
            JsonConvert.SerializeObject(teamZombie),
            UnicodeEncoding.UTF8,
            "application/json"
        );

        //Act
        HttpResponseMessage postResponse = await testClient.PostAsync(
            "/teams/CreateTeam",
            stringContent);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await testClient.GetAsync("/teams/GetAllTeams");
        getResponse.EnsureSuccessStatusCode();

        string raw = await getResponse.Content.ReadAsStringAsync();
        List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(raw);
        
        Assert.Equal(1, teams.Count());
        Assert.Equal("Zombie", teams[0].Name);
        Assert.Equal(teamZombie.ID, teams[0].ID);
    }
}