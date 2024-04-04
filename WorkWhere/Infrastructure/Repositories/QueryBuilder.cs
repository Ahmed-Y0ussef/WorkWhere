using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class QueryBuilder<T> : IQueryBuilder<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


    }
}
