using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XmAssignment.API.Services;
using XmAssignment.Common.Entities;
using XmAssignment.Common.Enums;
using XmAssignment.Common.Models;
using XmAssignment.Common.ResponseModels;
using XmAssignment.Common.Utils;
using XmAssignment.Data.UnitOfWork.Interface;

namespace XmAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class BtcUsdController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BtcUsdController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get All Price History.
        /// </summary>
        /// <remarks>
        ///  DataStoreType
        ///  1=SQLite
        ///  2=InMemoryDatabase
        ///  3=Docker Container Postgres
        /// </remarks>
        /// <param name="dataStoreType"></param>
        [HttpGet(Name = "GetAllHistory")]
        public IEnumerable<BtcPriceUIModel> GetAll(DataStoreType dataStoreType)
        {
            var allHistory = _unitOfWork.BtcPrice.GetAll().Select(x=>new BtcPriceUIModel
            {
                Price = x.Price.ToString("0.00"),
                Timestamp = x.Timestamp,
                Soruce = x.Soruce.ToString()
            });
            // return all history of data saved
            return allHistory;
        }


        /// <summary>
        /// Fetch From API and store in DataStore Of Choice.
        /// </summary>
        /// <remarks>
        ///  Price source
        ///  1=Bitfinex
        ///  2=BitStamp  
        ///  DataStoreType
        ///  1=SQLite
        ///  2=InMemoryDatabase
        ///  3=Docker Container Postgres
        /// </remarks>
        /// <param name="priceSource"></param>  
        /// <param name="dataStoreType"></param>  
        [HttpPost(Name = "FetchSaveData")]
        public async Task<BtcPriceUIModel> FetchSaveData(PriceSource priceSource, DataStoreType dataStoreType)
        {
            // get data form api based on price source
            var btcPrice = await APICallFetchData.GetBtcPrice(priceSource);

            // save to database in selected dataStore Type 
            _unitOfWork.BtcPrice.Add(btcPrice);
            await _unitOfWork.SaveChanges();

            var oData = new BtcPriceUIModel
            {
                Price = btcPrice.Price.ToString("0.00"),
                Timestamp = btcPrice.Timestamp,
                Soruce = btcPrice.Soruce.ToString()
            };

            // return data
            return oData;
        }




    }
}
