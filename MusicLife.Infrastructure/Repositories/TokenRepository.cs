﻿using MusicLife.Application.IRepositories;
using MusicLife.Domain.Entities;
using MusicLife.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Infracstructure.Repositories
{
    public class TokenRepository(DataContext context) 
        : BaseRepository<Token>(context), ITokenRepository
    {
    }
}
