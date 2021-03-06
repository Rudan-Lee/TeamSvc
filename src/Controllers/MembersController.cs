using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService
{
    [Route("/teams/{teamId}/[controller]")]
    [ApiController]
    public class MembersController: ControllerBase{
        ITeamRepository repository;

        public MembersController(ITeamRepository repo){
            repository = repo;
        }
        
        [HttpGet]
        public virtual IActionResult GetMembers(Guid teamID){
            Team team = repository.Get(teamID);

            if (team == null){
                return this.NotFound();
            }
            else{
                return this.Ok(team.Members);
            }
        }

        [HttpGet]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public virtual IActionResult GetMember(Guid teamId, Guid memberId){
            Team team = repository.Get(teamId);

            if (team == null){
                return this.NotFound();
            }
            else {
                var q = team.Members.Where(m => m.ID == memberId);

                if (q.Count() < 1){
                    return this.NotFound();
                }
                else{
                    return this.Ok(q.First());
                }
            }
        }

        [HttpPut]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public virtual IActionResult UpdateMember([FromBody]Member updateMember, Guid teamId, Guid memberId){
            Team team = repository.Get(teamId);

            if (team == null){
                return this.NotFound();
            } else{
                var q = team.Members.Where(m => m.ID == memberId);

                if (q.Count() < 1){
                    return this.NotFound();
                }
                else{
                    team.Members.Remove(q.First());
                    team.Members.Add(updateMember);
                    return this.Ok();
                }
            }
        }

        [HttpPost]
        public virtual IActionResult CreateMember([FromBody]Member newMember, Guid teamId){
            Team team = repository.Get(teamId);

            if (team == null){
                return this.NotFound();
            }
            else{
                team.Members.Add(newMember);
                var teamMember = new {TeamId = team.ID, MemberId = newMember.ID};
                return this.Created($"/teams/{teamMember.TeamId}/[controller]/{teamMember.MemberId}", teamMember);
            }
        }

        [HttpGet]
        [Route("/members/{memberId}/team")]
        public IActionResult GetTeamForMember(Guid memberId){
            var teamId = GetTeamIdForMember(memberId);
            if (teamId != Guid.Empty){
                return this.Ok(new { TeamID =teamId });
            }
            else{
                return this.NotFound();
            }
        }

        private Guid GetTeamIdForMember(Guid memberId){
            foreach (var team in repository.List()){
                var member = team.Members.FirstOrDefault(m => m.ID == memberId);
                if (member != null){
                    return team.ID;
                }
            }

            return Guid.Empty;
        }

    }
}