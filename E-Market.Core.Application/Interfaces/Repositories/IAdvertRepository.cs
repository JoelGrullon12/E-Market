﻿using E_Market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Repositories
{
    public interface IAdvertRepository:IGenericRepository<Advert>
    {
        Task<Advert> AddAsync(Advert ad);
    }
}
