using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.LogPersonalData
{
    public class LogPersonalDataRepo : InterfaceRepo<LogAfloeserAeH>
    {

        DataClass_logPersonalDataDataContext context;

        public LogPersonalDataRepo(string connectionString)
        {
            this.context = new DataClass_logPersonalDataDataContext(connectionString);
        }

        public IEnumerable<LogAfloeserAeH> list
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(LogAfloeserAeH entity)
        {
            context.LogAfloeserAeHs.InsertOnSubmit(entity);
            context.SubmitChanges();
        }

        public void Delete(LogAfloeserAeH entity)
        {
            throw new NotImplementedException();
        }

        public LogAfloeserAeH FindById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogAfloeserAeH> getListBelongingToParent(int entityId)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
