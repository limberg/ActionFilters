using ActionFilters.Contracts;
using ActionFilters.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.ActionFilters
{
    public class ValidationEntitiyExistsAttribute<T> : IActionFilter where T : class, IEntity
    {
        private readonly MovieContext _context;
        public ValidationEntitiyExistsAttribute(MovieContext context)
        {
            _context = context;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey("Id"))
            {
                context.Result = new BadRequestObjectResult("Bad id Parameter");
                return;
            }
            
            Guid ID = (Guid) context.ActionArguments["Id"];

            var entity = _context.Set<T>().SingleOrDefault(m => m.Id.Equals(ID));

            if (entity == null)
            {
                context.Result = new NotFoundResult();
                return;
            }

            context.HttpContext.Items.Add("entity", entity);
        }
    }
}
