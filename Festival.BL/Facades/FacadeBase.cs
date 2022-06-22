using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Query;

namespace Festival.BL.Facades
{
    public abstract class FacadeBase<TEntity, TDetailModel, TListModel> 
        where TEntity: EntityBase, new()
        where TDetailModel: ModelBase
        where TListModel: ModelBase
    {
        private readonly RepositoryBase<TEntity> _repository;
        private readonly IMapper _autoMapper; //configured automapper
        protected FacadeBase(RepositoryBase<TEntity> repositoryBase, IMapper autoMapper)
        {
            _repository = repositoryBase;
            _autoMapper = autoMapper;
        }

        protected virtual Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] Includes { get; } = Array.Empty<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();

        public IEnumerable<TListModel> GetAllList() => _repository.GetAll().AsEnumerable().Select(x => _autoMapper.Map<TListModel>(x));

        public TDetailModel GetById(Guid Id)
        {
            var query = _repository.GetAll();
            foreach (var include in Includes)
            {
                query = include(query);
            }
            return _autoMapper.Map<TDetailModel>(query.SingleOrDefault(x => x.Id == Id));
        }

        public IEnumerable<TDetailModel> GetAllDetail()
        {
            var query = _repository.GetAll();
            foreach (var include in Includes)
            {
                query = include(query);
            }

            return query.AsEnumerable().Select(x => _autoMapper.Map<TDetailModel>(x));
        }

        public void Delete(Guid Id)
        {
            _repository.DeleteById(Id);
        }

        public void Delete(TListModel model) => Delete(model.Id);
        
        public void Delete(TDetailModel model) => Delete(model.Id);

        public Guid InsertOrUpdate(TDetailModel model)
        {
            return _repository.InsertOrUpdate(_autoMapper.Map<TEntity>(model)).Id;
        }




    }
}
