using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetUserHandler
    {
        private readonly IUserRepository _repository;

        public GetUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> Handle(GetUserQuery query)
        {
            return _repository.GetUsers(query.Name,query.Password);
        }
    }
}
