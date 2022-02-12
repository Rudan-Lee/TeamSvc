using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Persistence;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    public class TeamsController : ControllerBase{

        ITeamRepository repository;
        public TeamsController(ITeamRepository repo) {
           repository = repo;
        }

        [HttpGet]
        public async virtual Task<IActionResult>  GetAllTeams()
        {
            return this.Ok(repository.GetTeams());
            //return Enumerable.Empty<Team>();
            //return new Team[]{new Team("one"), new Team("two")};
        }

        public async Task<IActionResult> CreateTeam(Team t){
            repository.AddTeam(t);
            return this.Ok();
        }

    }
}