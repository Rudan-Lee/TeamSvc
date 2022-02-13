using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Persistence;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService.Controllers
{
    //https://localhost:5001/teams/GetAllTeams/
    [Route("[controller]/[action]")]
    [ApiController]
    public class TeamsController : ControllerBase{

        ITeamRepository repository;
        public TeamsController(ITeamRepository repo) {
           repository = repo;
        }

        [HttpGet]
        public async virtual Task<IActionResult>  GetAllTeams()
        {
            return this.Ok(repository.List());
            //return Enumerable.Empty<Team>();
            //return new Team[]{new Team("one"), new Team("two")};
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetTeam(Guid id){
            Team team = repository.Get(id);
            if (team != null){
                return this.Ok(team);
            }
            else{
                return this.NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(Team newTeam){
            repository.Add(newTeam);
            return this.Created($"/teams/{newTeam.ID}", newTeam);
        }

        [HttpPut("{id}")]
        public virtual IActionResult UpdateTeam([FromBody]Team team, Guid id){
            team.ID = id;

            if (repository.Update(team) == null){
                return this.NotFound();
            }
            else{
                return this.Ok(team);
            }
        }

        [HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id){
            Team team = repository.Delete(id);
            if (team == null){
                return this.NotFound();
            }
            else{
                return this.Ok(team.ID);
            }
        }

    }
}