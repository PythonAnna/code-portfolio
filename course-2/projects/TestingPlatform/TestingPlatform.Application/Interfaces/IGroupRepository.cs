using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IGroupRepository
    {
        int Create(Group group);
        List<Group> GetAll();
        Group GetById(int id);
        void Update(Group group);
        void Delete(int id);
    }
}
