using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace whatTheHack.App_Plugins.Dao
{
    public interface IDao<TModel>
    {
        List<TModel> select();
        TModel select(int id);
        TModel create(TModel tModel);
        TModel update(TModel tModel);
        bool delete(int id);

        Task<List<TModel>> selectAsync();
        Task<TModel> selectAsync(int id);
        Task<TModel> createAsync(TModel tModel);
        Task<TModel> updateAsync(TModel tModel);
        Task<bool> deleteAsync(int id);
    }
}
