using Google.Cloud.Datastore.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WmNosql.Proxy.Model;

namespace WmNosql.Proxy.Core
{
    public class ItemMasterProxy : BaseProxy
    {
        private const string KIND = "ItemMaster";

        private const string F_CODE = "Code";
        private const string F_DESC = "Description";
        private const string F_BRAND = "Brand";
        private const string F_MODEL = "Model";
        private const string F_BASEUOM = "BaseUOM";
        private const string F_USEREMAIL = "UserEmail";
        private const string F_TIMESTAMP = "Timestamp";

        public ItemMasterProxy(string tenantId, string environment)
            :base(tenantId, environment){}

        public async Task<long> InsertAsync(
            string code, 
            string desc, 
            string brand,
            string model,
            string baseUOM,
            string username)
        {
            try
            {
                Key key = new KeyFactory(_envKey, KIND).CreateIncompleteKey();
                var itemMaster = new Entity
                {
                    Key = key,
                    [F_CODE] = code,
                    [F_DESC ] = desc,
                    [F_BRAND] = brand,
                    [F_MODEL] = model,
                    [F_BASEUOM] = baseUOM,
                    [F_USEREMAIL] = username,
                    [F_TIMESTAMP] = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                };

                using (var transaction = _db.BeginTransaction())
                {
                    transaction.Insert(itemMaster);
                    await transaction.CommitAsync();
                    return itemMaster.Key.Path[1].Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(long Id,
            string code,
            string desc,
            string brand,
            string model,
            string baseUOM,
            string username)
        {
            try
            {
                Key key = new KeyFactory(_envKey, KIND).CreateKey(Id);
                using (var transaction = _db.BeginTransaction())
                {
                    var itemMaster = transaction.Lookup(key);
                    if (itemMaster != null)
                    {
                        itemMaster[F_CODE] = code;
                        itemMaster[F_DESC] = desc;
                        itemMaster[F_BRAND] = brand;
                        itemMaster[F_MODEL] = model;
                        itemMaster[F_BASEUOM] = baseUOM;
                        itemMaster[F_USEREMAIL] = username;

                        transaction.Update(itemMaster);
                    }
                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(long Id)
        {
            try
            {
                Key key = new KeyFactory(_envKey, KIND).CreateKey(Id);
                using (var transaction = _db.BeginTransaction())
                {
                    transaction.Delete(key);
                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(long[] Ids)
        {
            try
            {
                using (var transaction = _db.BeginTransaction())
                {
                    if (Ids.Length > 0)
                    {
                        for (int i = 0; i < Ids.Length; i++)
                        {
                            Key key = new KeyFactory(_envKey, KIND).CreateKey(Ids[i]);
                            transaction.Delete(key);
                        }
                        await transaction.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemMaster> DetailAsync(long Id)
        {
            Key rootKey = _db.CreateKeyFactory(ENVIRONMENT).CreateKey(_environment);
            Key key = new KeyFactory(rootKey, KIND).CreateKey(Id);
            var entity = await _db.LookupAsync(key);

            if (entity == null)
                return null;

            var item = new ItemMaster
            {
                Id = entity.Key.Path[1].Id,
                Code = (string)entity[F_CODE],
                Description = (string)entity[F_DESC],
                Brand = (string)entity[F_BRAND],
                Model = (string)entity[F_MODEL],
                BaseUOM = (string)entity[F_BASEUOM],
                UserEmail = (string)entity[F_USEREMAIL],
                Timestamp = (DateTime)entity[F_TIMESTAMP]
            };

            return item;
        }

        public async Task<List<ItemMaster>> ListAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string code = "",
            string name = "",
            string userEmail = "",
            bool descending = false)
        {
            try
            {
                Query query;

                //Filter filter = null;
                Filter filter = Filter.HasAncestor(_db.CreateKeyFactory(ENVIRONMENT).CreateKey(_environment));

                //filter = new Filter(Filter.GreaterThanOrEqual("Timestamp", dtFrom));
                //filter = Filter.And(filter, new Filter(Filter.LessThanOrEqual("Timestamp", dtTo)));
                //filter = Filter.And(filter, new Filter(Filter.Equal("Environment", _environment)));

                //if (code.Length > 0)
                //    filter = Filter.And(filter, new Filter(Filter.Equal("Code", code)));

                //if (name.Length > 0)
                //    filter = Filter.And(filter, new Filter(Filter.Equal("Name", name)));

                //if (userEmail.Length > 0)
                //    filter = Filter.And(filter, new Filter(Filter.Equal("UserEmail", userEmail)));

                if (descending)
                {
                    query = new Query(KIND)
                    {
                        Order = {
                        { F_TIMESTAMP, PropertyOrder.Types.Direction.Descending }
                     }
                    };
                }
                else
                {
                    query = new Query(KIND)
                    {
                        Order = {
                        { F_TIMESTAMP, PropertyOrder.Types.Direction.Ascending }
                     }
                    };
                }

                query.Filter = filter;
                query.Offset = (pageNumber - 1) * pageSize;
                query.Limit = pageSize;

                var result = await _db.RunQueryAsync(query);
                var entities = result.Entities;
                List<ItemMaster> items = new List<ItemMaster>(); ;
                for (var i = 0; i < entities.Count; i++)
                {
                    items.Add(new ItemMaster
                    {
                        Id = entities[i].Key.Path[1].Id,
                        Code = (string)entities[i][F_CODE],
                        Description = (string)entities[i][F_DESC],
                        Brand = (string)entities[i][F_BRAND],
                        Model = (string)entities[i][F_MODEL],
                        BaseUOM = (string)entities[i][F_BASEUOM],
                        UserEmail = (string)entities[i][F_USEREMAIL],
                        Timestamp = (DateTime)entities[i][F_TIMESTAMP]
                    });
                }

                return items;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
