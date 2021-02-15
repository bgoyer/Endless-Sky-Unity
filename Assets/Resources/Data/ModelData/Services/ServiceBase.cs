using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Assets.Resources.Data.ModelData.Models;
using JsonFlatFileDataStore;
using UnityEngine;

namespace Assets.Resources.Data.ModelData.Services
{
    public class ServiceBase<TModel>
        where TModel : ModelBase, new()
    {
        DataStore store;
        string tableName;

        public ServiceBase(string tableName)
        {
            SetDatabase(tableName, "en");
        }

        public ServiceBase(string tableName, string lang) 
        {
            SetDatabase(tableName, lang);
        }

        public TModel NewModel() {
            return new TModel(); 
        }

        public IDocumentCollection<TModel> Collection 
        {
            get 
            {
                return store.GetCollection<TModel>();
            }
        }

        public virtual bool Exists(TModel model)
        {
            return Get(model.Id) != null;
        }

        public bool Exists(string name)
        {
            return GetByName(name) != null;
        }

        public TModel Get(string id)
        {
            return Collection.AsQueryable()
                .Where(m => m.Id == id)
                .FirstOrDefault();
        }

        public TModel GetByName(string name)
        {
            var collection = Collection.AsQueryable();



            return collection.Where(m => m.Name == name)
                .FirstOrDefault();
        }

        public TModel Save(TModel item)
        {
            if (item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }
            TModel _itm = Get(item.Id);

            if (_itm != null)
            {
                Collection.UpdateOne(item.Id, item);
            }
            else
            {
                Collection.InsertOne(item);
            }

            return item;
        }

        public IEnumerable<TModel> SaveMany(IEnumerable<TModel> items)
        {
            foreach (var item in items) 
            {
                Save(item);
            }

            return items;
        }

        public bool Delete(TModel item)
        {
            return Collection.DeleteOne(item.Name);
        }

        private void SetDatabase(string tableName, string lang)
        {
            var fileName = $"{tableName}-{lang}.json";
            this.tableName = tableName;
            store = new DataStore(Path.Combine(Application.streamingAssetsPath, fileName));
        }
    }
}
