﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailMapper.Impl
{
    public class DetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> : IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies>,
                IDetailBuilderProperties<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies>
    {
        private readonly Func<TMasterDTO, ICollection<TDetailDTO>> _detailDTOCollection;
        private readonly Func<TMaster, ICollection<TDetail>> _detailCollection;
        private Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> _addAction;
        private Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> _updateAction;
        private Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> _deleteAction;
        private Func<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> _create;
        private Func<TDetailDTO, TDetail, bool> _equals;

        #region Constructor
        public DetailMapperBuilder(
            Func<TMasterDTO, ICollection<TDetailDTO>> detailDTOCollection,
            Func<TMaster, ICollection<TDetail>> detailCollection
            )
        {
            _detailDTOCollection = detailDTOCollection;
            _detailCollection = detailCollection;
        }
        #endregion


        #region IDetailBuilderProperties

        public Func<TMasterDTO, ICollection<TDetailDTO>> DetailDTOCollection
        {
            get { return _detailDTOCollection; }
        }

        public Func<TMaster, ICollection<TDetail>> DetailCollection
        {
            get { return _detailCollection; }
        }

        public Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> Add
        {
            get { return _addAction; }
        }

        public Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> Update
        {
            get { return _updateAction; }
        }

        public Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> Delete
        {
            get { return _deleteAction; }
        }

        public Func<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> Create
        {
            get { return _create; }
        }

        public Func<TDetailDTO, TDetail, bool> AreEquals
        {
            get { return _equals; }
        }

        #endregion

        #region IDetailMapperBuilder
        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> AddAction(Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> addDetail)
        {
            _addAction = addDetail;
            return this;
        }

        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> UpdateAction(Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> updateDetail)
        {
            _updateAction = updateDetail;
            return this;
        }

        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> DeleteAction(Action<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> deleteDetail)
        {
            _deleteAction = deleteDetail;
            return this;
        }

        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> CreateFunc(Func<IViewModelContext<TMasterDTO, TMaster, TDependencies>, TDetail> createFunc)
        {
            _create = createFunc;
            return this;
        }


        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> EqualsFunc(Func<TDetailDTO, TDetail, bool> equalsFunc)
        {
            _equals = equalsFunc;
            return this;
        }

        public IDetailMapper<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> Build()
        {
            return new DetailMapper<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies>(this);
        }
        #endregion
    }

    public class DetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail> : IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail>
    {
        public Func<TMasterDTO, ICollection<TDetailDTO>> _detailDTOCollection;
        public Func<TMaster, ICollection<TDetail>> _detailCollection;

        public DetailMapperBuilder(
            Func<TMasterDTO, ICollection<TDetailDTO>> detailDTOCollection,
            Func<TMaster, ICollection<TDetail>> detailCollection)
        {
            _detailDTOCollection = detailDTOCollection;
            _detailCollection = detailCollection;
        }

        #region IDetailMapperBuilder
        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies> WithDependencies<TDependencies>()
        {
            return new DetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail, TDependencies>(_detailDTOCollection, _detailCollection);
        }
        #endregion
    }

    public class DetailMapperBuilder<TMasterDTO, TMaster> : IDetailMapperBuilder<TMasterDTO, TMaster>
    {
        public IDetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail> Detail<TDetailDTO, TDetail>(
            Func<TMasterDTO, ICollection<TDetailDTO>> detailDTOCollection,
            Func<TMaster, ICollection<TDetail>> detailCollection)
        {
            return new DetailMapperBuilder<TMasterDTO, TMaster, TDetailDTO, TDetail>(
                detailDTOCollection,
                detailCollection);
        }
    }
}
