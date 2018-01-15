using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.ComponentModel.DataAnnotations;
using GeometricLayout.Model;
using GeometricLayout.Core;
using GeometricayoutApi.Models;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace GeometricLayout.Api.Controllers
{
    public class ShapeController : ApiController
    {
        /// <summary>
        /// Returns Triagle that  corresponds to the give row (A-F) and column (1-12
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>


        public IHttpActionResult GetTriangle1()
        {
            return this.Ok();
        }

        [Route("plotter/triangle/{row}/{column:int}"), HttpGet]
        public IHttpActionResult GetTriangle([FromUri]  string row, [FromUri] int column)
        {

            try
            {
                var result = new Plotter().GetTriangle(new Ordinal { Row = row, Column = column });
                return this.Ok<Triangle>(result);
            }
            catch (ArgumentNullException)
            {
                return this.BadRequest("Row  is required.");
            }
            catch (ArgumentOutOfRangeException)
            {
                return this.BadRequest("Row/Column  is not valid.");
            }
            catch (Exception)
            {
                //Log the exception details as per the standard established

                return this.InternalServerError(new Exception("Error occured while processign your request."));
            }


        }

        /// <summary>
        /// This API method lets you locate the a triange in a shape 
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        [Route("plotter/location"), HttpGet]
        [HttpPost] //This is non REST standard methos just in case there is any concern passign this big query string
        public IHttpActionResult LocateTriangle([Required, ModelBinder(typeof(TriangleModelBinder))]TriangleModel triangle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                Ordinal result = new Plotter().GetOrdinal(new Triangle
                {
                    R = new Vertex(triangle.R.X, triangle.R.Y),
                    A = new Vertex(triangle.A.X, triangle.A.Y),
                    B = new Vertex(triangle.B.X, triangle.B.Y)
                });
                return this.Ok<Ordinal>(result);
            }
            catch (ArgumentNullException)
            {
                return this.BadRequest("Row  is required.");
            }
            catch (ArgumentOutOfRangeException ao)
            {
                return this.BadRequest(ao.Message);
            }
            catch (ArgumentException ax)
            {
                return this.BadRequest(ax.Message);
            }
            catch (Exception)
            {
                //Log the exception details as per the standard established

                return this.InternalServerError(new Exception("Error occured while processign your request."));
            }
        }
    }

    public class TriangleModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(TriangleModel))
            {
                return false;
            }

            var valProvider = bindingContext.ValueProvider;
            var result = new TriangleModel();
            float ax , ay , bx , by , rx , ry ;

            if (!float.TryParse(valProvider.GetValue("ax")?.RawValue?.ToString(), out ax)) { ax = -1; }
            if(!float.TryParse(valProvider.GetValue("ay")?.RawValue?.ToString(), out ay)){ ay = -1; }
            if(!float.TryParse(valProvider.GetValue("bx")?.RawValue?.ToString(), out bx)){ bx = -1; }
            if(!float.TryParse(valProvider.GetValue("by")?.RawValue?.ToString(), out by)){ by = -1; }
            if (!float.TryParse(valProvider.GetValue("rx")?.RawValue?.ToString(), out rx)) { rx = -1; }
            if (!float.TryParse(valProvider.GetValue("ry")?.RawValue?.ToString(), out ry)) { ry = -1; }

            if (ax < 0 || ay < 0 || bx < 0 || by < 0 || rx < 0 || ry < 0)
            {
                bindingContext.ModelState.AddModelError(
                bindingContext.ModelName, "All vertex are not provided with positive value.");
                return false;
            }

            result.A = new VertexModel { X = ax, Y = ay } ;
            result.B = new VertexModel { X = bx, Y = by };
            result.R = new VertexModel { X = rx, Y = ry };

            bindingContext.Model = result;
            return true;


        }
    }
}

