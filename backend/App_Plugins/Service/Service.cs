using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whatTheHack.App_Plugins.Dao;
using whatTheHack.Models.Database;

namespace whatTheHack.App_Plugins.Service
{
    public abstract class Service<TObject, TDao>
        where TDao : IDao<TObject>, new()
        where TObject : IModel, new()
    {
        protected TDao tDao;

        public Service()
        {
            tDao = new TDao();
        }
        public Service(TDao tDao)
        {
            this.tDao = tDao;
        }

        public List<TObject> find()
        {
            return tDao.select();
        }
        public TObject find(int id)
        {
            return tDao.select(id);
        }
        public TObject create(TObject tObject)
        {
            return tDao.create(tObject);
        }
        public TObject update(TObject tObject)
        {
            return tDao.update(tObject);
        }
        public bool delete(int id)
        {
            return tDao.delete(id);
        }

        public async Task<List<TObject>> findAsync()
        {
            return await tDao.selectAsync();
        }
        public async Task<TObject> findAsync(int id)
        {
            return await tDao.selectAsync(id);
        }
        public async Task<TObject> createAsync(TObject tObject)
        {
            return await tDao.createAsync(tObject);
        }
        public async Task<TObject> updateAsync(TObject tObject)
        {
            return await tDao.updateAsync(tObject);
        }
        public async Task<bool> deleteAsync(int id)
        {
            return await tDao.deleteAsync(id);
        }
    }
}
