using Statmath.Application.Client.Common.Abstraction;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Implementation
{
    /// <summary>
    /// Handle connection & communication with a fitting api
    /// </summary>
    public class JobConnectionHandler : IJobConnectionHandler
    {
        private readonly IRequestHelper _requestHelper;

        public JobConnectionHandler(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        /// <summary>
        /// delete all entries from database
        /// </summary>
        /// <returns>affected rows</returns>
        public async Task<int> DeleteAll()
        {
            var response = await _requestHelper.MakeDeleteRequest(Constants.ApiActionDeleteMany);
            return response;
        }

        /// <summary>
        /// delete a single row from database
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>affected rows</returns>
        public async Task<int> Delete(JobViewModel viewModel)
        {
            var response = await _requestHelper.MakeDeleteRequest(Constants.ApiActionDelete, viewModel);
            return response;
        }

        /// <summary>
        /// create a viewmodel row in database
        /// </summary>
        /// <param name="viewModel">viewmodel to create</param>
        /// <returns>affected rows</returns>
        public async Task<string> CreateJob(JobViewModel viewModel)
        {
            var response = await _requestHelper.MakePostRequest(Constants.ApiActionCreate, viewModel);
            return response;
        }

        /// <summary>
        /// create a bunch of viewmodel rows in database
        /// </summary>
        /// <param name="viewModels">viewmodels to create</param>
        /// <returns>affected rows</returns>
        public async Task<string> CreateJobs(IEnumerable<JobViewModel> viewModels)
        {
            var response = await _requestHelper.MakePostRequest(Constants.ApiActionCreateMany, viewModels);
            return response;
        }

        /// <summary>
        /// get all jobs from database
        /// </summary>
        /// <returns>list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetAll()
        {
            var response = await _requestHelper.MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetAll);
            return response;
        }

        /// <summary>
        /// get a single row from db by job id
        /// </summary>
        /// <param name="job">job id</param>
        /// <returns>jobs viewmodel</returns>
        public async Task<JobViewModel> GetByJob(int job)
        {
            // store necessary parameter stuff in a list with key value package
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamJob, job)
            };
            var response = await _requestHelper.MakeGetRequest<JobViewModel>(Constants.ApiActionGetByJob, queryParameters);
            return response;
        }


        /// <summary>
        /// get jobs by machine
        /// </summary>
        /// <param name="machine">name of machine</param>
        /// <returns>a list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetByMachine(string machine)
        {
            // store necessary parameter stuff in a list with key value package
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamMachine, machine)
            };
            var response = await _requestHelper.MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetByMachine, queryParameters);
            return response;
        }


        /// <summary>
        /// get jobs by date
        /// </summary>
        /// <param name="type">start or end</param>
        /// <param name="date">date</param>
        /// <returns>a list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetByDate(string type, string date)
        {
            // store necessary parameters stuff in a list with key value packages
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamTime, type),
                new KeyValuePair<string, dynamic>(Constants.ApiParamDate, date)
            };
            var response = await _requestHelper.MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetByDate, queryParameters);
            return response;
        }

        /// <summary>
        /// get jobs by date
        /// </summary>
        /// <param name="type">start or end</param>
        /// <param name="date">date</param>
        /// <returns>a list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetByDateTime(string type, string datetime)
        {
            // store necessary parameters stuff in a list with key value packages
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamTime, type),
                new KeyValuePair<string, dynamic>(Constants.ApiParamDate, datetime)
            };
            var response = await _requestHelper.MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetByDateTime, queryParameters);
            return response;
        }
    }
}
