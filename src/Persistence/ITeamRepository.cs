using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;

namespace   StatlerWaldorfCorp.TeamService.Persistence
{
    public interface ITeamRepository{
        IEnumerable<Team> List();
        Team Add(Team team);
        Team Delete(Guid id);
        Team Update(Team team);
        Team Get(Guid id);
    }
}