using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using BusinessLayer.Models.SCSupplyModels;
using Library._SCSupply.Methods;
using System;

namespace BusinessLayer.Functions.SCSupply
{
    public class SCSupplyFunctions : ISCSupply
    {
        #region Injection
        private _SCSupply _scsupply;
        private MapSCSuppy _mapSCSuppy;
        private MapResponseBase _mapResponseBase;

        public SCSupplyFunctions()
        {
            _scsupply = new _SCSupply();
            _mapSCSuppy = new MapSCSuppy();
            _mapResponseBase = new MapResponseBase();

        }
        #endregion

        public ResponseBase Add(SCSupply_Models sc_Supply)
        {
            return _mapResponseBase.MapToUI(_scsupply.Add(_mapSCSuppy.MapToLibrary(sc_Supply)));
        }

        public ResponseBase Delete(int ID)
        {
            return _mapResponseBase.MapToUI(_scsupply.Delete(ID));
        }

        public Generic<SCSupply_Models> GetAll()
        {
            var Supply =_scsupply.GetAll();
            Generic<SCSupply_Models> model = new Generic<SCSupply_Models>();
            model.ResponseInt = Supply.ResponseInt;
            model.ResponseListInt = Supply.ResponseListInt;
            model.ResponseListString = Supply.ResponseListString;
            model.ResponseMessage = Supply.ResponseMessage;
            model.ResponseString = Supply.ResponseString;
            model.ResponseSuccess = Supply.ResponseSuccess;
            foreach (var item in Supply.GenericClassList)
            {
                model.GenericClassList.Add(_mapSCSuppy.MapToUI(item));
            }
            return model;
        }

        public Generic<SCSupply_Models> GetAllByRange(DateTime StartDate, DateTime EndDate)
        {
            var Supply = _scsupply.GetAllByRange(StartDate, EndDate);
            Generic<SCSupply_Models> model = new Generic<SCSupply_Models>();
            model.ResponseInt = Supply.ResponseInt;
            model.ResponseListInt = Supply.ResponseListInt;
            model.ResponseListString = Supply.ResponseListString;
            model.ResponseMessage = Supply.ResponseMessage;
            model.ResponseString = Supply.ResponseString;
            model.ResponseSuccess = Supply.ResponseSuccess;
            foreach (var item in Supply.GenericClassList)
            {
                model.GenericClassList.Add(_mapSCSuppy.MapToUI(item));
            }
            return model;
        }

        public Generic<SCSupply_Models> GetByID(int ID)
        {
            var Supply = _scsupply.GetByID(ID);
            Generic<SCSupply_Models> model = new Generic<SCSupply_Models>();
            model.ResponseInt = Supply.ResponseInt;
            model.ResponseListInt = Supply.ResponseListInt;
            model.ResponseListString = Supply.ResponseListString;
            model.ResponseMessage = Supply.ResponseMessage;
            model.ResponseString = Supply.ResponseString;
            model.ResponseSuccess = Supply.ResponseSuccess;
            model.GenericClass = _mapSCSuppy.MapToUI(Supply.GenericClass) ?? new SCSupply_Models();
            return model;
        }

        public ResponseBase Update(SCSupply_Models sc_Supply)
        {
            return _mapResponseBase.MapToUI(_scsupply.Update(_mapSCSuppy.MapToLibrary(sc_Supply)));
        }
    }
}
