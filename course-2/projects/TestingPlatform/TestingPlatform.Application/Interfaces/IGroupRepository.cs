using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IGroupRepository
    {
        int Create(GroupDto group);
        List<GroupDto> GetAll();
        GroupDto GetById(int id);
        void Update(GroupDto group);
        void Delete(int id);
    }
}
