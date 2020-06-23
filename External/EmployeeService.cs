using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace External
{
    public interface IEmployeeService
    {
      Task<IEnumerable<Employee>> GetEmployees();
    }
    public class EmployeeService : IEmployeeService
    {
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync("http://dummy.restapiexample.com/api/v1/employees"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                       var responseString = response.Content.ReadAsStringAsync().Result;
                       var responseObj =  JsonConvert.DeserializeObject<Response>(responseString);
                       if(responseObj.status=="success")
                        return responseObj.data;
                    }
                }
            }
            return new List<Employee>{};
        }
    }
}
