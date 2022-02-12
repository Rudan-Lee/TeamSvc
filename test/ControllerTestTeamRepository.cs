using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
namespace StatlerWaldorCorp.TeamService.Tests;

public class ControllerTestTeamRepository: MemoryTeamRepository
{
    public ControllerTestTeamRepository(): base(new List<Team>{
        new Team("One"),
        new Team("Two")
    }){

    }
}
