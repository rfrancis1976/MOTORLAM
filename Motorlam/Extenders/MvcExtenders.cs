using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motorlam.Extenders
{
    public  static class MvcExtenders
    {
        public static IEnumerable<ClientModelState> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState.Where(kv => kv.Value.Errors != null && kv.Value.Errors.Count > 0)
                .Select(kv => new ClientModelState { 
                    PropertyName = kv.Key, 
                    Errors = kv.Value.Errors.Select(e => e.ErrorMessage) 
                });
        }
    }

    [Serializable]
    public class ClientModelState
    {
        public string PropertyName { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}