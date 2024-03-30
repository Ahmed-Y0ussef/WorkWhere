﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Contract
{
    public interface IUnitofwork : IAsyncDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
        //public T GetRepositoryTwo<T>() where T : class;
        Task<int> CommitAsync();
    }
}