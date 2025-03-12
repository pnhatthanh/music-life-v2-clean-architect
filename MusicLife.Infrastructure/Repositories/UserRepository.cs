using Microsoft.EntityFrameworkCore;
using MusicLife.Application.IRepositories;
using MusicLife.Domain.Entities;
using MusicLife.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Infracstructure.Repositories
{
    internal class UserRepository(DataContext context)
        : BaseRepository<User>(context), IUserRepository
    {
    }
}
