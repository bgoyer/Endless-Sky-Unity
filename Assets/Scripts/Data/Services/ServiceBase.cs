using ES.Data.Models;
using JsonFlatFileDataStore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES.Data.Services
{
    public class ServiceBase<TModel>
        where TModel : ModelBase
    {
        DataStore store;
        string tableName;

        public ServiceBase(string databaseFileName) 
        {
            tableName = databaseFileName;
            store = new DataStore($"Assets\\Scripts\\Data\\Db\\{databaseFileName}.json");
        }

        public IDocumentCollection<TModel> Collection 
        {
            get 
            {
                return store.GetCollection<TModel>();
            }
        }

        public TModel Get(string name) 
        {
            return Collection.AsQueryable()
                .Where(m => m.Name == name)
                .FirstOrDefault();
        }

        public TModel Insert(TModel item)
        {
            TModel _itm = Get(item.Name);

            if (_itm == null)
            {
                Collection.InsertOne(item);
            }
            else
            {
                throw new System.Exception($"{item.Name} already exists in the {tableName} table.");
            }

            return item;
        }

        public IEnumerable<TModel> InsertMany(IEnumerable<TModel> items)
        {
            Collection.InsertMany(items);
            return items;
        }

        public TModel Update(TModel item)
        {
            Collection.UpdateOne(item.Name, item);
            return item;
        }

        public IEnumerable<TModel> UpdateMany(IEnumerable<TModel> items)
        {
            foreach (var item in items) 
            {
                Update(item);
            }

            return items;
        }

        public bool Delete(TModel item)
        {
            return Collection.DeleteOne(item.Name);
        }
    }
}
