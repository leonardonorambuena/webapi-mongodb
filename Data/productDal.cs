using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using mongotest.Models;

namespace mongotest.Data{

    public class ProductDal
    {
        private readonly IMongoDatabase _db;

        private IMongoCollection<Product> _collection;

        public ProductDal(){
            this._db = Connect();
            this._collection = this._db.GetCollection<Product>("products");
        }

        private IMongoDatabase Connect(){
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("products");

            return database;
        }


        public IEnumerable<Product> All(){
            var product = this._collection.Find(new BsonDocument()).ToListAsync();
            return product.Result;
        }

        public Product Find(string id)
        {
            try{
                return this._collection.Find(d => d.id == id).FirstOrDefault();
            }catch(Exception e){
                return null;
            }
            
        }

        public bool Remove(string id){
            var product = this.Find(id);
            if(id != null) {
               this._collection.DeleteOne(product.ToBsonDocument());
               return true;
            }
            return false;
        }

        public bool Save(Product model)
        {
            try{
                this._collection.InsertOne(model);
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public bool Update(Product model)
        {
            try{
                this._collection.UpdateOne(p => p.id == model.id, new BsonDocument {{"$set", model.ToBsonDocument()}});
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

    }
}