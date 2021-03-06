﻿using Microsoft.Extensions.Options;
using DataAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using DataAccess.Models;
using MongoDB.Bson;
using DataAccess.Repository.Base;

namespace DataAccess.Repository
{
    /// <summary>
    /// Class for policy's repository with async methods. Basic CRUD operations
    /// </summary>
    public class PolicyRepository 
        : AbstractRepository, IPolicyRepository
    {
        public PolicyRepository(IOptions<Settings> settings) 
            : base(settings)
        {
        }

        public async Task Add(Policy policy)
        {
            await base._context
                .Policies
                .InsertOneAsync(policy)
                .ConfigureAwait(false);           
        }

        public async Task<IEnumerable<Policy>> Get()
        {
            return await base._context
                .Policies
                .Find(x => true)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Policy> Get(string id)
        {
            var policy = Builders<Policy>.Filter.Eq("NameOfInsuredObject", id);
            return await base._context
                .Policies
                .Find(policy)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<DeleteResult> Remove(string id)
        {
            return await base._context
                .Policies
                .DeleteOneAsync(Builders<Policy>.Filter.Eq("NameOfInsuredObject", id))
                .ConfigureAwait(false);
        }
        
        public async Task<DeleteResult> RemoveAll()
        {
            return await base._context
                .Policies
                .DeleteManyAsync(new BsonDocument())
                .ConfigureAwait(false);
        }

        public async Task<string> Update(string id, Policy policy)
        {
            await base._context
                .Policies
                .ReplaceOneAsync(x => x.NameOfInsuredObject == id, policy)
                .ConfigureAwait(false);
            return "";
        }
    }
}
