using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.Commands
{
    public interface IGenericsRepository
    {
       void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
    }
}
