using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.LogPersonalData
{
    public interface InterfaceRepo<IEntity>
    {
        IEnumerable<IEntity> list { get; }
        IEnumerable<IEntity> getListBelongingToParent(int entityId);
        void Add(IEntity entity);
        void Delete(IEntity entity);
        void Update();
        IEntity FindById(string id);
    }
}
