using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Text.Json;

namespace Api.Filters
{
	public class ExceptionConversionFilter
		: IActionFilter
	{
		private readonly ProblemDetailsFactory detailsFactory;
		private readonly IHttpContextAccessor contextAccessor;

		public ExceptionConversionFilter(ProblemDetailsFactory detailsFactory, IHttpContextAccessor contextAccessor)
		{
			this.detailsFactory = detailsFactory;
			this.contextAccessor = contextAccessor;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			switch (context.Exception)
			{
				case ValidationException ex:
					var modelState = new ModelStateDictionary();
					foreach (var error in ex.Errors)
					{
						modelState.AddModelError(error.PropertyName, error.ErrorMessage);
					}
					ValidationProblemDetails problem = this.detailsFactory.CreateValidationProblemDetails(
						this.contextAccessor.HttpContext,
						modelState);
					context.Result = new UnprocessableEntityObjectResult(problem);
					context.Result.ExecuteResultAsync(context);
					break;
				case UnauthorizedAccessException ex:
					context.Result = new UnauthorizedResult();
					break;
			}
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
		}
	}
}
