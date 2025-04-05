using MusicApi.Infracstructure.Repositories;
using MusicLife.Application.IRepositories;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure.Repositories
{
    public class CategoryRepository(DataContext context) 
        : BaseRepository<Category>(context), ICategoryRepository
    {
    }
}
