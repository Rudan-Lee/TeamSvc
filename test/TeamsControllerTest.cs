using System;
using Xunit;
using StatlerWaldorfCorp.TeamService.Controllers;
using StatlerWaldorfCorp.TeamService.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace StatlerWaldorCorp.TeamService.Tests;

public class TeamsControllerTest
{
    TeamsController controller = new TeamsController(new ControllerTestTeamRepository());

    [Fact]
    public async void QueryTeamListReturnsCorrectTeams()
    {
        var result = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value ;
        List<Team> teams = new List<Team>(result);
        Assert.Equal(teams.Count, 2);
    }

    [Fact]
    public async void CreateTeamAddsTeamToList(){
        var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;

        List<Team> original = new List<Team>(teams);

        Team t = new Team("Sample");
        var result = await controller.CreateTeam(t);

        var newTeamsRaw = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;

        List<Team> newTeams = new List<Team>(newTeamsRaw);

        Assert.Equal(newTeams.Count, original.Count + 1);

        var sampleTeam = newTeams.FirstOrDefault(target => target.Name == "Sample");
        Assert.NotNull(sampleTeam);


    }
}